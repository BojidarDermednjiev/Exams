namespace Invoices.DataProcessor.ImportDto
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    using Common;
    using Data.Models.Enums;


    public class ImportInvoiceDto
    {
        [JsonProperty("Number")]
        [Range(ValidationConstants.InvoiceNumberMinRange, ValidationConstants.InvoiceNumberMaxRange)]
        public int Number { get; set; }

        [Required]
        [JsonProperty("IssueDate")] 
        public DateTime IssueDate { get; set; } 

        [Required] 
        [JsonProperty("DueDate")]
        public DateTime DueDate { get; set; } 

        [JsonProperty("Amount")]
        public decimal Amount { get; set; }

        [Required]
        [JsonProperty("CurrencyType")]
        [Range(ValidationConstants.InvoiceCurrencyTypeMinRange, ValidationConstants.InvoiceCurrencyTypeMaxRange)]
        public CurrencyType CurrencyType { get; set; }

        [JsonProperty("ClientId")]
        public int ClientId { get; set; }
    }
}
