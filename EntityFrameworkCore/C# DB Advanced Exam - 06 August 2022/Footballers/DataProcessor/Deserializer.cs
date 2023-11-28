namespace Footballers.DataProcessor
{
    using AutoMapper;
    using System.Text;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    
    using Data;
    using ImportDto;
    using Utilities;
    using Data.Models;
    using Data.Models.Enums;


    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        private static XmlHelper xmlHelper;
        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            xmlHelper = new XmlHelper();
            IMapper mapper = InitializationAutoMapper();
            ImportCoachDto[] coachDtos = xmlHelper.Deserialize<ImportCoachDto[]>(xmlString, "Coaches");
            ICollection<Coach> coaches = new HashSet<Coach>();
            foreach (ImportCoachDto importCoachDto in coachDtos)
            {
                if (!IsValid(importCoachDto) || string.IsNullOrEmpty(importCoachDto.Name) || string.IsNullOrEmpty(importCoachDto.Nationality))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Coach coach = new Coach()
                {
                    Name = importCoachDto.Name,
                    Nationality = importCoachDto.Nationality
                };
                ;
                foreach (ImportFootballerDto importFootballerDto in importCoachDto.Footballers)
                {
                    DateTime contractStartDate;
                    DateTime contractEndDate;

                    if (!IsValid(importFootballerDto) 
                        || !DateTime.TryParseExact(importFootballerDto.ContractStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out contractStartDate)
                                                      
                        || !DateTime.TryParseExact(importFootballerDto.ContractEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out contractEndDate)
                        || contractStartDate > contractEndDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Footballer footballer = new Footballer()
                    {
                        Name = importFootballerDto.Name,
                        ContractStartDate = contractStartDate,
                        ContractEndDate = contractEndDate,
                        BestSkillType = (BestSkillType)importFootballerDto.BestSkillType,
                        PositionType = (PositionType)importFootballerDto.PositionType
                    };
                    coach.Footballers.Add(footballer);
                }
                coaches.Add(coach);
                sb.AppendLine(string.Format(SuccessfullyImportedCoach, coach.Name, coach.Footballers.Count));
            }
            context.Coaches.AddRange(coaches);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            IMapper mapper = InitializationAutoMapper();
            ImportTeamDto[] teamDtos = JsonConvert.DeserializeObject<ImportTeamDto[]>(jsonString);
            ICollection<Team> teams = new HashSet<Team>();
            foreach (var importTeamDto in teamDtos)
            {
                if (!IsValid(importTeamDto) || importTeamDto.Trophies == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Team team = mapper.Map<Team>(importTeamDto);
                foreach (var footballerId in importTeamDto.Footballers.Distinct())
                {
                    Footballer footballer = context.Footballers.Find(footballerId);
                    if (footballer == default)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    team.TeamsFootballers.Add(new TeamFootballer()
                    {
                        Footballer = footballer
                    });
                }
                teams.Add(team);
                sb.AppendLine(string.Format(SuccessfullyImportedTeam, team.Name, team.TeamsFootballers.Count));
            }
            context.Teams.AddRange(teams);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
        private static IMapper InitializationAutoMapper()
            => new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FootballersProfile>();
            }));
    }
}
