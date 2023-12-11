namespace Cadastre.DataProcessor.ImportDtos
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;
    
    using Common;

    [XmlType("Property")]
    public class ImportPropertyDto
    {
        [XmlElement("PropertyIdentifier")]
        [MinLength(ValidationConstants.PropertyIdentifierMinLength)]
        [MaxLength(ValidationConstants.PropertyIdentifierMaxLength)]
        public string PropertyIdentifier { get; set; } = null!;

        [XmlElement("Area")]
        [Range(ValidationConstants.PropertyAreaMinRange, ValidationConstants.PropertyAreaMaxRange)]
        public int Area { get; set; }

        [XmlElement("Details")]
        [MinLength(ValidationConstants.PropertyDetailsMinLength)]
        [MaxLength(ValidationConstants.PropertyDetailsMaxLength)]
        public string Details { get; set; } = null!;

        [XmlElement("Address")]
        [MinLength(ValidationConstants.PropertyAddressMinLength)]
        [MaxLength(ValidationConstants.PropertyAddressMaxLength)]
        public string Address { get; set; } = null!;

        [XmlElement("DateOfAcquisition")]
        public string DateOfAcquisition { get; set; } = null!;
    }
}
