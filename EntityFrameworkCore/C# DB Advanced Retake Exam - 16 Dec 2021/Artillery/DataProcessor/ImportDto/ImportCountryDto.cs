namespace Artillery.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;
    
    using Common;

    [XmlType("Country")]
    public class ImportCountryDto
    {
        [XmlElement("CountryName")]
        [MinLength(ValidationConstants.CountryNameMinLength)]
        [MaxLength(ValidationConstants.CountryNameMaxLength)]
        public string CountryName { get; set; } = null!;

        [XmlElement("ArmySize")]
        [Range(ValidationConstants.CountryArmySizeMin, ValidationConstants.CountryArmySizeMax)]
        public int ArmySize { get; set; }

    }
}
