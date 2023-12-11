namespace Cadastre.DataProcessor
{
    using AutoMapper;
    using System.Text;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    
    using Data;
    using Utilities;
    using ImportDtos;
    using Data.Models;
    using Data.Enumerations;
    using System.Globalization;

    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid Data!";
        private const string SuccessfullyImportedDistrict =
            "Successfully imported district - {0} with {1} properties.";
        private const string SuccessfullyImportedCitizen =
            "Succefully imported citizen - {0} {1} with {2} properties.";

        private static XmlHelper xmlHelper;
        public static string ImportDistricts(CadastreContext dbContext, string xmlDocument)
        {
            StringBuilder sb = new StringBuilder();
            IMapper mapper = InitializeAutoMapper();
            xmlHelper = new XmlHelper();
            ImportDistrictDto[] districtDto = xmlHelper.Deserialize<ImportDistrictDto[]>(xmlDocument, "Districts");
            ICollection<District> districts = new HashSet<District>();
            foreach (var importDistrictDto in districtDto)
            {
                if (!IsValid(importDistrictDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                District district = new District()
                {
                    Region = importDistrictDto.Region,
                    Name = importDistrictDto.Name,
                    PostalCode = importDistrictDto.PostalCode
                };

                if (dbContext.Districts.Any(d => d.Name == importDistrictDto.Name))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                foreach (var importPropertyDto in importDistrictDto.Properties)
                {
                    DateTime DateOfAcquisition;

                    if (!IsValid(importPropertyDto) || !DateTime.TryParseExact(importPropertyDto.DateOfAcquisition, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOfAcquisition))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (district.Properties.Any(p => p.PropertyIdentifier == importPropertyDto.PropertyIdentifier) || district.Properties.Any(p => p.Address == importPropertyDto.Address))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Property property = new Property()
                    {
                        PropertyIdentifier = importPropertyDto.PropertyIdentifier,
                        Area = importPropertyDto.Area,
                        Details = importPropertyDto.Details,
                        Address = importPropertyDto.Address,
                        DateOfAcquisition = DateOfAcquisition
                    };
                    district.Properties.Add(property);
                }
                districts.Add(district);
                sb.AppendLine(string.Format(SuccessfullyImportedDistrict, district.Name, district.Properties.Count));
            }
            dbContext.Districts.AddRange(districts);
            dbContext.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportCitizens(CadastreContext dbContext, string jsonDocument)
        {

            StringBuilder sb = new StringBuilder();
            ImportCitizenDto[] citizenDtos = JsonConvert.DeserializeObject<ImportCitizenDto[]>(jsonDocument)!;
            ICollection<Citizen> citizens = new HashSet<Citizen>();
            foreach (var importCitizenDto in citizenDtos)
            {
                DateTime BirthDate;
                if (!IsValid(importCitizenDto) || !DateTime.TryParseExact(importCitizenDto.BirthDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out BirthDate) || !Enum.TryParse(importCitizenDto.MaritalStatus, out MaritalStatus maritalStatus))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Citizen citizen = new Citizen()
                {
                    FirstName = importCitizenDto.FirstName,
                    LastName = importCitizenDto.LastName,
                    BirthDate = BirthDate,
                    MaritalStatus = maritalStatus,
                };
                foreach (var propertyId in importCitizenDto.Properties.Distinct())
                {
                    PropertyCitizen propertyCitizen = new PropertyCitizen()
                    {
                        Citizen = citizen,
                        PropertyId = propertyId,
                    };
                    citizen.PropertiesCitizens.Add(propertyCitizen);
                }
                citizens.Add(citizen);
                sb.AppendLine(string.Format(SuccessfullyImportedCitizen, citizen.FirstName, citizen.LastName, citizen.PropertiesCitizens.Count));
            }
            dbContext.Citizens.AddRange(citizens);
            dbContext.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }

        private static IMapper InitializeAutoMapper()
            => new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<CadastreProfile>(); }));
    }
}
