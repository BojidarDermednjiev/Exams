using Artillery.DataProcessor.ExportDto;

namespace Artillery
{
    using AutoMapper;

    using Data.Models;
    using DataProcessor.ImportDto;
    public class ArtilleryProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public ArtilleryProfile()
        {
            // Country
            this.CreateMap<ImportCountryDto, Country>();
            this.CreateMap<Country, ExportCountryDto>()
                .ForMember(d => d.Country, opt => opt.MapFrom(s => s.CountryName))
                .ForMember(d => d.ArmySize, opt => opt.MapFrom(s => s.ArmySize));
            // Gun
            this.CreateMap<ImportGunDto, Gun>()
                .ForMember(d => d.GunType, opt => opt.MapFrom(s => s.GunType.ToString()));
            this.CreateMap<Gun, ExportGunDto>()
                .ForMember(d => d.Manufacturer, opt => opt.MapFrom(s => s.Manufacturer.ManufacturerName))
                .ForMember(d => d.GunWeight, opt => opt.MapFrom(s => s.GunWeight))
                .ForMember(d => d.BarrelLength, opt => opt.MapFrom(s => s.BarrelLength))
                .ForMember(d => d.GunType, opt => opt.MapFrom(s => s.GunType.ToString()))
                .ForMember(d => d.Range, opt => opt.MapFrom(s => s.Range))
                .ForMember(d => d.Countries, opt => opt.MapFrom(s => s.CountriesGuns
                    .Where(g => g.Country.ArmySize > 450_000)
                    .Select(y => new ExportCountryDto { Country = y.Country.CountryName, ArmySize = y.Country.ArmySize })
                    .OrderBy(g => g.ArmySize)
                    .ToArray()));
            // Shell
            this.CreateMap<ImportShellDto, Shell>();
            // Manufacturer
            this.CreateMap<ImportManufacturerDto, Manufacturer>();
            // CountryGun
            this.CreateMap<ImportCountryGunDto, CountryGun>();

        }
    }
}