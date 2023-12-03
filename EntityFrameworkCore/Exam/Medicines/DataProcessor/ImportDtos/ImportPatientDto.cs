namespace Medicines.DataProcessor.ImportDtos
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    
    using Common;

    public class ImportPatientDto
    {
        [JsonProperty("FullName")]
        [MinLength(ValidationConstants.PatientFullNameMin)]
        [MaxLength(ValidationConstants.PatientFullNameMax)]
        public string FullName { get; set; } = null!;

        [JsonProperty("AgeGroup")]
        [Range(ValidationConstants.PatientAgeGroupMinRange, ValidationConstants.PatientAgeGroupMaxRange)]
        public int AgeGroup { get; set; }

        [JsonProperty("Gender")]
        [Range(ValidationConstants.PatientGenderMinRange, ValidationConstants.PatientGenderMaxRange)]
        public int Gender { get; set; }

        [JsonProperty("Medicines")]
        public ICollection<int> Medicines { get; set; } = null!;
    }
}
