using Boardgames.DataProcessor.ExportDto;

namespace Boardgames
{
    using AutoMapper;

    using Data.Models;
    using Data.Models.Enums;
    using DataProcessor.ImportDto;


    public class BoardgamesProfile : Profile
    {
        public BoardgamesProfile()
        {
            // Boardgame
            this.CreateMap<ImportBoardgameDto, Boardgame>()
                .ForMember(d => d.CategoryType, opt => opt.MapFrom(s => (CategoryType)s.CategoryType));
            CreateMap<Boardgame, ExportBoardgamesDto>()
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(s => s.Name))
                .ForMember(dest => dest.YearPublished, opt =>
                    opt.MapFrom(s => s.YearPublished));
            // Creator
            this.CreateMap<ImportCreatorDto, Creator>();
            
            CreateMap<Creator, ExportCreatorDto>()
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(dest => dest.BoardgamesCount, opt =>
                    opt.MapFrom(s => s.Boardgames.Count))
                .ForMember(dest => dest.Boardgames, opt =>
                    opt.MapFrom(s => s.Boardgames
                        .ToArray()
                        .OrderBy(b => b.Name)));
            ;
            // Seller 
            this.CreateMap<ImportSellerDto, Seller>();
        }
    }
}