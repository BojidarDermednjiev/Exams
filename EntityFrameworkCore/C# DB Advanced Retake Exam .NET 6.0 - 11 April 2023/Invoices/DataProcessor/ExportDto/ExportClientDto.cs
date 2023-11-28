namespace Invoices.DataProcessor.ExportDto
{
    using Newtonsoft.Json;
    public class ExportClientDto
    {
        [JsonProperty("Name")]
        public string Name { get; set; } = null!;

        [JsonProperty("NumberVat")]
        public string NumberVat { get; set; } = null!;
    }
}
