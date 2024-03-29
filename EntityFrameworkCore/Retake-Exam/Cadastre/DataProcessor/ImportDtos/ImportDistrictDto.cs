﻿namespace Cadastre.DataProcessor.ImportDtos
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;
    
    using Common;
    using Data.Enumerations;

    [XmlType("District")]
    public class ImportDistrictDto
    {
        [XmlAttribute("Region")]
        public Region Region { get; set; }

        [XmlElement("Name")]
        [MinLength(ValidationConstants.DistrictNameMinLength)]
        [MaxLength(ValidationConstants.DistrictNameMaxLength)]
        public string Name { get; set; } = null!;

        [XmlElement("PostalCode")]
        [RegularExpression(ValidationConstants.DistrictPostalCodeRegex)]
        public string PostalCode { get; set; } = null!;

        [XmlArray("Properties")]
        public ImportPropertyDto[] Properties { get; set; } = null!;
    }
}
