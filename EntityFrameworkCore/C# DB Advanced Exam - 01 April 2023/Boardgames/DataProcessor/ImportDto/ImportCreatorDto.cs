namespace Boardgames.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using Common;

    [XmlType("Creator")]
    public class ImportCreatorDto
    {
        [XmlElement("FirstName")]
        [Required]
        [MinLength(ValidationConstants.CreatorFirstNameMinLength)]
        [MaxLength(ValidationConstants.CreatorFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [XmlElement("LastName")]
        [Required]
        [MinLength(ValidationConstants.CreatorSecondNameMinLength)]
        [MaxLength(ValidationConstants.CreatorSecondNameMaxLength)]
        public string LastName { get; set; } = null!;

        [XmlArray("Boardgames")]
        public List<ImportBoardgameDto> Boardgames { get; set; } = null!;
    }
}
