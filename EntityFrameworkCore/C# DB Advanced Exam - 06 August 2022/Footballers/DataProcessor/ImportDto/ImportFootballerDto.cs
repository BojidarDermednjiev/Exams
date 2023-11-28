namespace Footballers.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;

    using Common;


    [XmlType("Footballer")]
    public class ImportFootballerDto
    {
        [Required]
        [XmlElement("Name")]
        [MinLength(ValidationConstants.FootballerNameMinLength)]
        [MaxLength(ValidationConstants.FootballerNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [XmlElement("ContractStartDate")]
        public string ContractStartDate { get; set; } = null!;

        [Required]
        [XmlElement("ContractEndDate")]
        public string ContractEndDate { get; set; } = null!;

        [Required]
        [XmlElement("BestSkillType")]
        [Range(ValidationConstants.FootballerBestSkillMinRange, ValidationConstants.FootballerBestSkillMaxRange)]
        public int BestSkillType { get; set; }

        [Required]
        [XmlElement("PositionType")]
        [Range(ValidationConstants.FootballerPositionMinRange, ValidationConstants.FootballerPositionMaxRange)]
        public int PositionType { get; set; }
    }
}
