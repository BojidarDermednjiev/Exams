using AutoMapper.QueryableExtensions;
using Boardgames.DataProcessor.ExportDto;
using Microsoft.EntityFrameworkCore;

namespace Boardgames.DataProcessor
{
    using AutoMapper;
    using Newtonsoft.Json;

    using Data;
    using Utilities;
    using Data.Models.Enums;

    public class Serializer
    {
        public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper xmlHelper = new XmlHelper();
            ExportCreatorDto[] creatorDtos = context.Creators
                //.Where(c => c.Boardgames.Any())
                .AsNoTracking()
                .ProjectTo<ExportCreatorDto>(mapper.ConfigurationProvider)
                .OrderByDescending(c => c.BoardgamesCount)
                .ThenBy(c => c.Name)
                .ToArray();
            return xmlHelper.Serialize<ExportCreatorDto[]>(creatorDtos, "Creators");
        }

        public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
        {
            var sellers = context.Sellers
                .Where(s => s.BoardgamesSellers.Any(bs =>
                    bs.Boardgame.YearPublished >= year && bs.Boardgame.Rating <= rating))
                .Select(s => new
                {
                    Name = s.Name,
                    Website = s.Website,
                    Boardgames = s.BoardgamesSellers
                        .Where(bs => bs.Boardgame.YearPublished >= year && bs.Boardgame.Rating <= rating)
                        .OrderByDescending(bs => bs.Boardgame.Rating)
                        .ThenBy(bs => bs.Boardgame.Rating)
                        .Select(bs => new
                        {
                            Name = bs.Boardgame.Name,
                            Rating = bs.Boardgame.Rating,
                            Mechanics = bs.Boardgame.Mechanics,
                            Category = bs.Boardgame.CategoryType.ToString(),
                        })
                        .ToArray()
                })
                .OrderByDescending(s => s.Boardgames.Length)
                .ThenBy(s => s.Name)
                .Take(5)
                .ToArray();
            return JsonConvert.SerializeObject(sellers, Formatting.Indented);
        }

        private static IMapper InitializeAutoMapper()
            => new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<BoardgamesProfile>(); }));
    }
}