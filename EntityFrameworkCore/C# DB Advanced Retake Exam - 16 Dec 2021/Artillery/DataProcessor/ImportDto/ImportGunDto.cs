namespace Artillery.DataProcessor.ImportDto
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class ImportGunDto
    {
        [JsonProperty("ManufacturerId")]
        public int ManufacturerId { get; set; }

        [Range(ValidationConstants.GunMinWeight, ValidationConstants.GunMaxWeight)]
        public int GunWeight { get; set; }

        [Range(ValidationConstants.GunBarrelMinLength, ValidationConstants.GunBarrelMaxLength)]
        public double BarrelLength { get; set; }

        public int? NumberBuild { get; set; }

        [Range(ValidationConstants.GunMinRange, ValidationConstants.GunMaxRange)]
        public int Range { get; set; }

        [Required]
        public string GunType { get; set; } = null!;

        public int ShellId { get; set; }

        public ImportCountryGunDto[] Countries { get; set; } = null!;
    }
}
