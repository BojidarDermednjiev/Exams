namespace Medicines.DataProcessor.ImportDtos
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;
    
    using Common;


    [XmlType("Medicine")]
    public class ImportMedicinesDto
    {
        [XmlAttribute("category")]
        [Range(ValidationConstants.MedicineCategoryMinRange, ValidationConstants.MedicineCategoryMaxRange)]
        public int Category { get; set; } 

        [XmlElement("Name")]
        [MinLength(ValidationConstants.MedicineNameMinLength)]
        [MaxLength(ValidationConstants.MedicineNameMaxLength)]
        public string Name { get; set; } = null!;

        [XmlElement("Price")]
        [Range(ValidationConstants.MedicinePriceMin, ValidationConstants.MedicinePriceMax)]
        public decimal Price { get; set; }

        [XmlElement("ProductionDate")]
        public string ProductionDate { get; set; } = null!;

        [XmlElement("ExpiryDate")]
        public string ExpiryDate { get; set; } = null!;

        [XmlElement("Producer")]
        [MinLength(ValidationConstants.MedicineProducerMin)]
        [MaxLength(ValidationConstants.MedicineProducerMax)]
        public string Producer { get; set; } = null!;
    }
}
