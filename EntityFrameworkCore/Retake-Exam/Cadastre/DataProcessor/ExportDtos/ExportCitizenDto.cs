namespace Cadastre.DataProcessor.ExportDtos
{
    using Newtonsoft.Json;

    public class ExportCitizenDto
    {
        [JsonProperty("LastName")]
        public string LastName { get; set; } = null!;

        [JsonProperty("MaritalStatus")]
        public string MaritalStatus { get; set; } = null!;
    }
}
