namespace Footballers.DataProcessor.ImportDto
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class ImportTeamDto
    {
        [Required]
        [JsonProperty("Name")]
        [MinLength(ValidationConstants.TeamNameMinLength)]
        [MaxLength(ValidationConstants.TeamNameMaxLength)]
        [RegularExpression(ValidationConstants.RegexSpecificationsName)]
        public string Name { get; set; } = null!;

        [Required]
        [JsonProperty("Nationality")]
        [MinLength(ValidationConstants.TeamNationalityMinLength)]
        [MaxLength(ValidationConstants.TeamNationalityMaxLength)]
        public string Nationality { get; set; } = null!;

        [Required]
        [JsonProperty("Trophies")]
        public int Trophies { get; set; }

        [JsonProperty("Footballers")]
        public ICollection<int> Footballers { get; set; } = null!;

    }
}
