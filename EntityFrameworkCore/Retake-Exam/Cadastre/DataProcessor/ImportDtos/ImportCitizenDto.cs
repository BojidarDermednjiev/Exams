namespace Cadastre.DataProcessor.ImportDtos
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    
    using Common;

    public class ImportCitizenDto
    {
        [JsonProperty("FirstName")]
        [MinLength(ValidationConstants.CitizenFirstNameMinLength)]
        [MaxLength(ValidationConstants.CitizenFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [JsonProperty("LastName")]
        [MinLength(ValidationConstants.CitizenLastNameMinLength)]
        [MaxLength(ValidationConstants.CitizenLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [JsonProperty("BirthDate")]
        public string BirthDate { get; set; } = null!;

        [JsonProperty("MaritalStatus")]
        public string MaritalStatus { get; set; } = null!;

        [JsonProperty("Properties")]
        public int[] Properties { get; set; } = null!;
    }
}
