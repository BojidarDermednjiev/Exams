using AutoMapper.QueryableExtensions;
using Footballers.DataProcessor.ExportDto;
using Microsoft.EntityFrameworkCore;

namespace Footballers.DataProcessor
{
    using AutoMapper;

    using Data;
    using Footballers.Data.Models.Enums;
    using Newtonsoft.Json;
    using System.Globalization;
    using Utilities;


    public class Serializer
    {
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {
            XmlHelper xmlHelper = new XmlHelper();
            IMapper mapper = InitializationAutoMapper();
            ExportCoachDto[] coachDtos = context.Coaches
                .Where(c => c.Footballers.Any())
                .AsNoTracking()
                .ProjectTo<ExportCoachDto>(mapper.ConfigurationProvider)
                .OrderByDescending(c => c.Footballers.Length)
                .ThenBy(c => c.Name)
                .ToArray();
            return xmlHelper.Serialize<ExportCoachDto[]>(coachDtos, "Coaches");
        }

        public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
        {
           var teams = context.Teams
                .Where(t => t.TeamsFootballers.Any(tf => tf.Footballer.ContractStartDate >= date))
                .ToArray()
                .Select(t => new
                {
                    Name = t.Name,
                    Footballers = t.TeamsFootballers
                    .Where(tf => tf.Footballer.ContractStartDate >= date)
                    .OrderByDescending(tf => tf.Footballer.ContractEndDate)
                    .ThenBy(tf => tf.Footballer.Name)
                    .Select(tf => new
                    {
                        FootballerName = tf.Footballer.Name,
                        ContractStartDate = tf.Footballer.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
                        ContractEndDate = tf.Footballer.ContractEndDate.ToString("d", CultureInfo.InvariantCulture),
                        BestSkillType = tf.Footballer.BestSkillType.ToString(),
                        PositionType = tf.Footballer.PositionType.ToString()
                    })
                    .ToArray()
                })
                .OrderByDescending(t => t.Footballers.Length)
                .ThenBy(t => t.Name)
                .Take(5)
                .ToArray();

            return JsonConvert.SerializeObject(teams, Formatting.Indented);
        }
        private static IMapper InitializationAutoMapper()
            => new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FootballersProfile>();
            }));
    }
}
