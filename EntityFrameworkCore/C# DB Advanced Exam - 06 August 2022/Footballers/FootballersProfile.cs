using Footballers.DataProcessor.ExportDto;

namespace Footballers
{
    using AutoMapper;

    using Data.Models;
    using DataProcessor.ImportDto;
    using System.Globalization;

    // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE OR RENAME THIS CLASS
    public class FootballersProfile : Profile
    {
        public FootballersProfile()
        {
            // Coach
            this.CreateMap<Coach, ExportCoachDto>()
                .ForMember(d => d.FootballersCount, opt => opt.MapFrom(s=> s.Footballers.Count))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Footballers, opt => opt.MapFrom(s => s.Footballers.OrderBy(f => f.Name).ToArray()));
            // Footballer
            this.CreateMap<Footballer, ExportFootballerDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s=> s.Name))
                .ForMember(d => d.PositionType, opt => opt.MapFrom(s => s.PositionType.ToString()));
            // Team
            this.CreateMap<ImportTeamDto, Team>();
        }
    }
}
