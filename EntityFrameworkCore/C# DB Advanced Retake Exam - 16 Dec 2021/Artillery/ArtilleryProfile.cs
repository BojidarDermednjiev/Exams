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
            // Gun
            this.CreateMap<ImportGunDto, Gun>();
            // Shell
            this.CreateMap<ImportShellDto, Shell>();
            // Manufacturer
            this.CreateMap<ImportManufacturerDto, Manufacturer>();
            // CountryGun
            this.CreateMap<ImportCountryGunDto, CountryGun>();
        }
    }
}