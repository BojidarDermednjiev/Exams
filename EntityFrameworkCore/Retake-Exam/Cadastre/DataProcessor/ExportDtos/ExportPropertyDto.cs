namespace Cadastre.DataProcessor.ExportDtos
{
    using Newtonsoft.Json;
    public class ExportPropertyDto
    {
        [JsonProperty("PropertyIdentifier")]
        public string PropertyIdentifier { get; set; } = null!;

        [JsonProperty("Area")]
        public int Area { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; } = null!;

        [JsonProperty("DateOfAcquisition")]
        public string DateOfAcquisition { get; set; } = null!;

        [JsonProperty("Owners")]
        public IEnumerable<ExportCitizenDto> Owners { get; set; } = null!;
    }
}
