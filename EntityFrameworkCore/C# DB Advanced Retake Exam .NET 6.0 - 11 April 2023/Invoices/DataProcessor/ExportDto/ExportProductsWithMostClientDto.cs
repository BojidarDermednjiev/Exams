namespace Invoices.DataProcessor.ExportDto
{
    using Newtonsoft.Json;

    using Data.Models.Enums;
    public class ExportProductsWithMostClientDto
    {
        [JsonProperty("Name")]
        public string Name { get; set; } = null!;

        [JsonProperty("Price")]
        public decimal Price { get; set; }

        [JsonProperty("Category")]
        public CategoryType CategoryType { get; set; }

        [JsonProperty("Clients")]
        public ExportClientDto[] Clients { get; set; } = null!;
        

    }
}
