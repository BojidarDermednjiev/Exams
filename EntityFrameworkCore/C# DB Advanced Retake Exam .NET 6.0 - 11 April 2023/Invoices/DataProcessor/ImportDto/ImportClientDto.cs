namespace Invoices.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;
    
    using Common;

    [XmlType("Client")]
    public class ImportClientDto
    {
        [Required]
        [XmlElement("Name")]
        [MinLength(ValidationConstants.ClientNameMinLength)]
        [MaxLength(ValidationConstants.ClientNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [XmlElement("NumberVat")]
        [MinLength(ValidationConstants.ClientNumberVatMinLength)]
        [MaxLength(ValidationConstants.ClientNumberVatMaxLength)]
        public string NumberVat { get; set; } = null!;

        [XmlArray("Addresses")]
        public ImportAddressDto[] Addresses { get; set; } = null!;
    }
}
