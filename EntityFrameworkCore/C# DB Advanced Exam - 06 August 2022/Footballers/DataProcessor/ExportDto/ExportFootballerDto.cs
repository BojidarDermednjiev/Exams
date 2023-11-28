namespace Footballers.DataProcessor.ExportDto
{
    using System.Xml.Serialization;
    
    using Data.Models.Enums;
    [XmlType("Footballer")]
    public class ExportFootballerDto
    {
        [XmlElement("Name")]
        public string Name { get; set; } = null!;

        [XmlElement("Position")]
        public PositionType PositionType { get; set; }
    }
}
