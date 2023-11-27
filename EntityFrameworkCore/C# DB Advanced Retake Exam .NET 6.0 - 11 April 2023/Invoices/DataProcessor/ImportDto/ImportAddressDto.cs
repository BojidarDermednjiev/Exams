namespace Invoices.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;

    using Common;

    [XmlType("Address")]
    public class ImportAddressDto
    {
        [Required]
        [XmlElement("StreetName")]
        [MinLength(ValidationConstants.AddressStreetNameMinLength)]
        [MaxLength(ValidationConstants.AddressStreetNameMaxLength)]
        public string StreetName { get; set; } = null!;

        [Required]
        [XmlElement("StreetNumber")]
        public int StreetNumber { get; set; }

        [Required]
        [XmlElement("PostCode")] 
        public string PostCode { get; set; } = null!;

        [Required]
        [XmlElement("City")]
        [MinLength(ValidationConstants.AddressCityNameMinLength)]
        [MaxLength(ValidationConstants.AddressCityNameMaxLength)]
        public string City { get; set; } = null!;

        [Required]
        [XmlElement("Country")]
        [MinLength(ValidationConstants.AddressCountryMinName)]
        [MaxLength(ValidationConstants.AddressCountryMaxName)]
        public string Country { get; set; } = null!;
    }
}
