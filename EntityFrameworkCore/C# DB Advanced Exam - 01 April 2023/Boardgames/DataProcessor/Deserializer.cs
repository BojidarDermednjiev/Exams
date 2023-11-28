using Boardgames.Data.Models;
using Boardgames.Utilities;
using Newtonsoft.Json;

namespace Boardgames.DataProcessor
{
    using AutoMapper;
    using System.Text;
    using System.ComponentModel.DataAnnotations;

    using Data;
    using ImportDto;


    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCreator
            = "Successfully imported creator – {0} {1} with {2} boardgames.";

        private const string SuccessfullyImportedSeller
            = "Successfully imported seller - {0} with {1} boardgames.";

        private static XmlHelper xmlHelper;

        public static string ImportCreators(BoardgamesContext context, string xmlString)
        {
            IMapper mapper = InitializeAutoMapper();
            StringBuilder sb = new StringBuilder();
            xmlHelper = new XmlHelper();
            ImportCreatorDto[] creatorDtos = xmlHelper.Deserialize<ImportCreatorDto[]>(xmlString, "Creators");
            ICollection<Creator> creators = new HashSet<Creator>();
            foreach (ImportCreatorDto importCreatorDto in creatorDtos)
            {
                if (!IsValid(importCreatorDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Creator creator = mapper.Map<Creator>(importCreatorDto);
                foreach (var importBoardgameDto in importCreatorDto.Boardgames)
                {
                    if (!IsValid(importBoardgameDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        creator.Boardgames = new HashSet<Boardgame>();
                        continue;
                    }

                    Boardgame boardgame = mapper.Map<Boardgame>(importBoardgameDto);
                    creator.Boardgames.Add(boardgame);
                }

                creators.Add(creator);
                sb.AppendLine(string.Format(SuccessfullyImportedCreator, creator.FirstName, creator.LastName,
                    creator.Boardgames.Count));
            }

            context.Creators.AddRange(creators);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            IMapper mapper = InitializeAutoMapper();
            var sb = new StringBuilder();
            ImportSellerDto[] sellerDtos = JsonConvert.DeserializeObject<ImportSellerDto[]>(jsonString)!;
            ICollection<Seller> sellers = new HashSet<Seller>();
            foreach (ImportSellerDto importSellerDto in sellerDtos)
            {
                if (!IsValid(importSellerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Seller seller = mapper.Map<Seller>(importSellerDto);
                foreach (var currentBoardgameId in importSellerDto.Boardgames.Distinct())
                {
                    Boardgame boardgame = context.Boardgames.Find(currentBoardgameId);
                    if (boardgame == default)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    seller.BoardgamesSellers.Add(new BoardgameSeller()
                    {
                        Boardgame = boardgame
                    });

                    sellers.Add(seller);
                    sb.AppendLine(string.Format(SuccessfullyImportedSeller, seller.Name, seller.BoardgamesSellers.Count));
                }
            }

            context.Sellers.AddRange(sellers);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }

        private static IMapper InitializeAutoMapper()
            => new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<BoardgamesProfile>(); }));
    }
}
