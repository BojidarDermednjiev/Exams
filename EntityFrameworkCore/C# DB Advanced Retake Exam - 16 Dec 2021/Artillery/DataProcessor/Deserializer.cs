namespace Artillery.DataProcessor
{
    
    using AutoMapper;
    using System.Text;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    using Data;
    using ImportDto;
    using Utilities;
    using Data.Models;
    using Data.Models.Enums;



    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid data.";
        private const string SuccessfulImportCountry =
            "Successfully import {0} with {1} army personnel.";
        private const string SuccessfulImportManufacturer =
            "Successfully import manufacturer {0} founded in {1}.";
        private const string SuccessfulImportShell =
            "Successfully import shell caliber #{0} weight {1} kg.";
        private const string SuccessfulImportGun =
            "Successfully import gun {0} with a total weight of {1} kg. and barrel length of {2} m.";

        private static XmlHelper xmlHelper;
        public static string ImportCountries(ArtilleryContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            IMapper mapper = InitializeAutoMapper();
            xmlHelper = new XmlHelper();
            ImportCountryDto[] countryDtos = xmlHelper.Deserialize<ImportCountryDto[]>(xmlString, "Countries");
            ICollection<Country> countries = new HashSet<Country>();
            foreach (var importCountryDto in countryDtos)
            {
                if (!IsValid(importCountryDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Country country = mapper.Map<Country>(importCountryDto);
                countries.Add(country);
                sb.AppendLine(string.Format(SuccessfulImportCountry, country.CountryName, country.ArmySize));
            }
            context.Countries.AddRange(countries);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportManufacturers(ArtilleryContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            IMapper mapper = InitializeAutoMapper();
            xmlHelper = new XmlHelper();
            ImportManufacturerDto[] manufacturerDtos = xmlHelper.Deserialize<ImportManufacturerDto[]>(xmlString, "Manufacturers");
            ICollection<Manufacturer> manufacturers = new HashSet<Manufacturer>();
            foreach (var importManufacturerDto in manufacturerDtos)
            {
                var uniqueManufacturer = manufacturers.FirstOrDefault(x => x.ManufacturerName == importManufacturerDto.ManufacturerName);
                if (!IsValid(importManufacturerDto) || uniqueManufacturer != null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Manufacturer manufacturer = mapper.Map<Manufacturer>(importManufacturerDto);
                manufacturers.Add(manufacturer);
                var manufacturerCountry = manufacturer.Founded.Split(", ").ToArray();
                var countryWithCities = manufacturerCountry.Skip(Math.Max(0, manufacturerCountry.Count() - 2)).ToArray();
                sb.AppendLine(string.Format(SuccessfulImportManufacturer, manufacturer.ManufacturerName, string.Join(", ", countryWithCities)));
            }
            context.Manufacturers.AddRange(manufacturers);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportShells(ArtilleryContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            IMapper mapper = InitializeAutoMapper();
            xmlHelper = new XmlHelper();
            ImportShellDto[] shellDtos = xmlHelper.Deserialize<ImportShellDto[]>(xmlString, "Shells");
            ICollection<Shell> shells = new HashSet<Shell>();
            foreach (var importShellDto in shellDtos)
            {
                if (!IsValid(importShellDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Shell shell = mapper.Map<Shell>(importShellDto);
                shells.Add(shell);
                sb.AppendLine(string.Format(SuccessfulImportShell, shell.Caliber, shell.ShellWeight));
            }
            context.Shells.AddRange(shells);
            context.SaveChanges();
            return sb.ToString().TrimEnd();

        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {
           StringBuilder sb = new StringBuilder();
           IMapper mapper = InitializeAutoMapper();
           ImportGunDto[] gunsDtos = JsonConvert.DeserializeObject<ImportGunDto[]>(jsonString);
           ;
           ICollection<Gun> guns = new HashSet<Gun>();
           foreach (ImportGunDto importGunDto in gunsDtos)
           {
               if (!IsValid(importGunDto) || !Enum.TryParse(importGunDto.GunType, out GunType gunType))
               {
                   sb.Append(ErrorMessage); 
                   continue;
               }

               Gun gun = mapper.Map<Gun>(importGunDto);

               foreach (var importCountryGunDto in importGunDto.Countries.DistinctBy(c => c.Id))
               {
                  gun.CountriesGuns.Add(new CountryGun()
                  {
                      Gun = gun,
                      CountryId = importCountryGunDto.Id
                  });
               }
               guns.Add(gun);
               sb.AppendLine(string.Format(SuccessfulImportGun, gun.GunType.ToString(), gun.GunWeight, gun.BarrelLength));
           }
           context.Guns.AddRange(guns);
           context.SaveChanges();
           return sb.ToString().TrimEnd();
        }
        private static bool IsValid(object obj)
        {
            var validator = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
        private static IMapper InitializeAutoMapper()
            => new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<ArtilleryProfile>(); }));

    }
}