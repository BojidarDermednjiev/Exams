namespace Artillery.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;

    using Common;

    [XmlType("Shell")]
    public class ImportShellDto
    {
        [XmlElement("ShellWeight")]
        [Range(ValidationConstants.ShellMinWeight, ValidationConstants.ShellMaxWeight)]
        public double ShellWeight { get; set; }

        [XmlElement("Caliber")]
        [MinLength(ValidationConstants.ShellCaliberMin)]
        [MaxLength(ValidationConstants.ShellCaliberMax)]
        public string Caliber { get; set; } = null!;
    }
}
