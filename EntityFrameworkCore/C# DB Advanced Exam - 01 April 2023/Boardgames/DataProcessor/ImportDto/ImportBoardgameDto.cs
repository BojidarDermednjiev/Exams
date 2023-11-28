namespace Boardgames.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;
 
    using Common;
    using Data.Models.Enums;

    [XmlType("Boardgame")]
    public class ImportBoardgameDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(ValidationConstants.BoardGameMinLength)]
        [MaxLength(ValidationConstants.BoardGameMaxLength)]
        public string Name { get; set; } = null!;

        [XmlElement("Rating")]
        [Required]
        [Range(ValidationConstants.BoardGameRatingMin, ValidationConstants.BoardGameRatingMax)]
        public double Rating { get; set; }

        [XmlElement("YearPublished")]
        [Required]
        [Range(ValidationConstants.BoardGameYearPublishedMin, ValidationConstants.BoardGameYearPublishedMax)]
        public int YearPublished { get; set; }

        [XmlElement("CategoryType")]
        [Required]
        [Range(ValidationConstants.BoardGameCategoryTypeMin, ValidationConstants.BoardGameCategoryTypeMax)]
        public int CategoryType { get; set; }

        [XmlElement("Mechanics")]
        [Required]
        public string Mechanics { get; set; } = null!;

    }
}
