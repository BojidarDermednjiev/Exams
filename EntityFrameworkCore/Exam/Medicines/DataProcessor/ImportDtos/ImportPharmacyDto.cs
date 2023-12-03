namespace Medicines.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    
    using Common;

    [XmlType("Pharmacy")]
    public class ImportPharmacyDto
    {
        [XmlAttribute("non-stop")]
        [MinLength(4)]
        [MaxLength(5)]
        public string IsNonStop { get; set; } = null!;

        [XmlElement("Name")]
        [MinLength(ValidationConstants.PharmacyNameMinLength)]
        [MaxLength(ValidationConstants.PharmacyNameMaxLength)]
        public string Name { get; set; } = null!;

        [XmlElement("PhoneNumber")]
        [RegularExpression(ValidationConstants.PharmacyRegexPhoneNumber)]
        public string PhoneNumber { get; set; } = null!;

        [XmlArray("Medicines")]
        public ImportMedicinesDto[] Medicines { get; set; } = null!;
    }
}
