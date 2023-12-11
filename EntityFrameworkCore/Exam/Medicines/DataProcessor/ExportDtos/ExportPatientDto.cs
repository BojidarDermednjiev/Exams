namespace Medicines.DataProcessor.ExportDtos
{
    using System.Xml.Serialization;

    [XmlType("Patient ")]
    public class ExportPatientDto
    {
        public ExportPatientDto()
        {
            this.Medicines = new HashSet<ExportMedicineDto>();
        }

        [XmlAttribute("Gender")]
        public string Gender { get; set; } = null!;

        [XmlElement("Name")]
        public string Name { get; set; } = null!;

        [XmlElement("AgeGroup")]
        public string AgeGroup { get; set; } = null!;

        [XmlArray("Medicines")]
        public ICollection<ExportMedicineDto> Medicines { get; set; }
    }
}
