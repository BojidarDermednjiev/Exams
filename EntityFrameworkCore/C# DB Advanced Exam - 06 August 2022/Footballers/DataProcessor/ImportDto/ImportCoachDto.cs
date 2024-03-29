﻿namespace Footballers.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;

    using Common;


    [XmlType("Coach")]
    public class ImportCoachDto
    {
        [Required]
        [XmlElement("Name")]
        [MinLength(ValidationConstants.CoachNameMinLength)]
        [MaxLength(ValidationConstants.CoachNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [XmlElement("Nationality")]
        public string Nationality { get; set; } = null!;

        [XmlArray("Footballers")]
        public ImportFootballerDto[] Footballers { get; set; } = null!;

    }
}
