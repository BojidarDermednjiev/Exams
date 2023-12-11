using Cadastre.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Cadastre.DataProcessor
{
    using Newtonsoft.Json;

    using Data;
    using ExportDtos;

    public class Serializer
    {
        public static string ExportPropertiesWithOwners(CadastreContext dbContext)
        {
            string dateString = "01/01/2000";
            if (!DateTime.TryParse(dateString, out DateTime dateTime)) { }

            var properties = dbContext.Properties
                .Where(p => p.DateOfAcquisition >= dateTime)
                .OrderByDescending(p => p.DateOfAcquisition)
                .ThenBy(p => p.PropertyIdentifier)
                .Select(p => new ExportPropertyDto()
                {
                    PropertyIdentifier = p.PropertyIdentifier,
                    Area = p.Area,
                    Address = p.Address,
                    DateOfAcquisition = p.DateOfAcquisition.ToString("dd/MM/yyyy"),
                    Owners = p.PropertiesCitizens
                        .Where(pc => pc.Property.DateOfAcquisition >= dateTime)
                        .Select(pc => new ExportCitizenDto()
                        {
                            LastName = pc.Citizen.LastName,
                            MaritalStatus = pc.Citizen.MaritalStatus.ToString()
                        })
                        .OrderBy(c => c.LastName)
                        .ToArray()

                })
                .ToArray();

            return JsonConvert.SerializeObject(properties, Formatting.Indented);
        }

        public static string ExportFilteredPropertiesWithDistrict(CadastreContext dbContext)
        {
            XmlHelper xmlHelper = new XmlHelper();
            ExportDistrictDto[] district = dbContext.Properties
                .AsNoTracking()
                .Where(p => p.Area >= 100)
                .OrderByDescending(p => p.Area)
                .ThenBy(p => p.DateOfAcquisition)
                .Select(p => new ExportDistrictDto()
                {
                    PostalCode = p.District.PostalCode,
                    PropertyIdentifier = p.PropertyIdentifier,
                    Area = p.Area,
                    DateOfAcquisition = p.DateOfAcquisition.ToString("dd/MM/yyyy")
                })
                .ToArray();

            return xmlHelper.Serialize<ExportDistrictDto[]>(district, "Properties");
        }
    }
}
