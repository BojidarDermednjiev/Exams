namespace Medicines.DataProcessor.ExportDtos
{
    using Newtonsoft.Json;

    public class ExportPharmacyDto
    {
        [JsonProperty("Name")]
        public string Name { get; set; } = null!;

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; } = null!;
    }
}
