﻿namespace Medicines.DataProcessor.ExportDtos
{
    using System.Xml.Serialization;

    [XmlType("Medicine")]
    public class ExportMedicineDto
    {
        [XmlAttribute("Category")]
        public string Category { get; set; } = null!;

        [XmlElement("Name")]
        public string Name { get; set; } = null!;

        [XmlElement("Price")]
        public double Price { get; set; }

        [XmlElement("Producer")]
        public string Producer { get; set; } = null!;

        [XmlElement("BestBefore")]
        public string ExpiryDate { get; set; } = null!;
    }
}
