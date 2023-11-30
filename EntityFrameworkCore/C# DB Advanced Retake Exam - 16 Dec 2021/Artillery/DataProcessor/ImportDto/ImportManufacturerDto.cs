namespace Artillery.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;
    
    using Common;

    [XmlType("Manufacturer")]
    public class ImportManufacturerDto
    {
        [XmlElement("ManufacturerName")]
        [MinLength(ValidationConstants.ManufacturerNameMinLength)]
        [MaxLength(ValidationConstants.ManufacturerNameMaxLength)]
        public string ManufacturerName { get; set; } = null!;


        [XmlElement("Founded")]
        [MinLength(ValidationConstants.ManufacturerFoundedMinLength)]
        [MaxLength(ValidationConstants.ManufacturerFoundedMaxLength)]
        public string Founded { get; set; } = null!;
    }
}
