namespace Medicines.DataProcessor
{
    using AutoMapper;
    using System.Text;
    using Newtonsoft.Json;
    using System.Globalization;
    using System.ComponentModel.DataAnnotations;

    using Data;
    using Utilities;
    using ImportDtos;
    using Data.Models;
    using Data.Models.Enums;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data!";
        private const string SuccessfullyImportedPharmacy = "Successfully imported pharmacy - {0} with {1} medicines.";
        private const string SuccessfullyImportedPatient = "Successfully imported patient - {0} with {1} medicines.";

        private static XmlHelper xmlHelper;
        public static string ImportPatients(MedicinesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            IMapper mapper = InitializeAutoMapper();
            ImportPatientDto[] patientDtos = JsonConvert.DeserializeObject<ImportPatientDto[]>(jsonString);
            ICollection<Patient> patients = new HashSet<Patient>();
            foreach (var importPatientDto in patientDtos)
            {
                if (!IsValid(importPatientDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Patient patient = mapper.Map<Patient>(importPatientDto);
                foreach (var medicineId in importPatientDto.Medicines)
                {
                    if (patient.PatientsMedicines.Any(m => m.MedicineId == medicineId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    patient.PatientsMedicines.Add(new PatientMedicine()
                    {
                        Patient = patient,
                        MedicineId = medicineId,
                    });
                }
                patients.Add(patient);
                sb.AppendLine(string.Format(SuccessfullyImportedPatient, patient.FullName, patient.PatientsMedicines.Count));
            }
            context.Patients.AddRange(patients);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportPharmacies(MedicinesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            IMapper mapper = InitializeAutoMapper();
            xmlHelper = new XmlHelper();
            ImportPharmacyDto[] pharmacyDtos = xmlHelper.Deserialize<ImportPharmacyDto[]>(xmlString, "Pharmacies");
            ICollection<Pharmacy> pharmacies = new HashSet<Pharmacy>();
            foreach (var importPharmacyDto in pharmacyDtos)
            {
                if (!IsValid(importPharmacyDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Pharmacy pharmacy = new Pharmacy()
                {
                    Name = importPharmacyDto.Name,
                    IsNonStop = Convert.ToBoolean(importPharmacyDto.IsNonStop),
                    PhoneNumber = importPharmacyDto.PhoneNumber
                };

                foreach (var importMedicinesDto in importPharmacyDto.Medicines)
                {
                    DateTime ProductionDate;
                    DateTime ExpiryDate;

                    if (!IsValid(importMedicinesDto)
                                 || !DateTime.TryParseExact(importMedicinesDto.ProductionDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out ProductionDate)
                                 || !DateTime.TryParseExact(importMedicinesDto.ExpiryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out ExpiryDate)
                                 || ProductionDate >= ExpiryDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (pharmacy.Medicines.Any(x => x.Name == importMedicinesDto.Name && x.Producer == importMedicinesDto.Producer))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    Medicine medicine = new Medicine()
                    {
                        Category = (Category)importMedicinesDto.Category,
                        Name = importMedicinesDto.Name,
                        Price = importMedicinesDto.Price,
                        ProductionDate = DateTime.Parse(importMedicinesDto.ProductionDate),
                        ExpiryDate = DateTime.Parse(importMedicinesDto.ExpiryDate),
                        Producer = importMedicinesDto.Producer
                    };

                    pharmacy.Medicines.Add(medicine);
                }
                pharmacies.Add(pharmacy);
                sb.AppendLine(string.Format(SuccessfullyImportedPharmacy, pharmacy.Name, pharmacy.Medicines.Count));
            }
            context.Pharmacies.AddRange(pharmacies);
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
            => new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<MedicinesProfile>(); }));
    }
}
