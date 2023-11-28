namespace Boardgames
{
    using AutoMapper;

    using Data.Models;
    using DataProcessor.ImportDto;
    using Data.Models.Enums;


    public class BoardgamesProfile : Profile
    {
        public BoardgamesProfile()
        {
            // Boardgame
            this.CreateMap<ImportBoardgameDto, Boardgame>()
                .ForMember(d => d.CategoryType, opt => opt.MapFrom(s => (CategoryType)s.CategoryType));
            // Creator
            this.CreateMap<ImportCreatorDto, Creator>()
                .ForMember(d => d.Boardgames, opt => opt.MapFrom(s => s.Boardgames));
            // Seller 
            this.CreateMap<ImportSellerDto, Seller>();
        }
    }
}