namespace Boardgames.DataProcessor.ImportDto
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class ImportSellerDto
    {
        [JsonProperty("Name")]
        [Required]
        [MinLength(ValidationConstants.SellerNameMinLength)]
        [MaxLength(ValidationConstants.SellerNameMaxLength)]
        public string Name { get; set; } = null!;
        
        [Required]
        [JsonProperty("Address")]
        [MinLength(ValidationConstants.SellerAddressNameMinLength)]
        [MaxLength(ValidationConstants.SellerAddressNameMaxLength)]
        public string Address { get; set; } = null!;
        
        [Required]
        [JsonProperty("Country")]
        public string Country { get; set; } = null!;
        
        [Required]
        [JsonProperty("Website")]
        [RegularExpression(ValidationConstants.SellerWebsiteRegex)]
        public string Website { get; set; } = null!;

        [JsonProperty("Boardgames")]
        public ICollection<int> Boardgames { get; set; } = null!;

    }
}
