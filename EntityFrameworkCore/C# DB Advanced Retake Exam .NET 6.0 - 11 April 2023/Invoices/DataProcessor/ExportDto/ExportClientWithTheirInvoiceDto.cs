namespace Invoices.DataProcessor.ExportDto
{
    using System.Xml.Serialization;

    [XmlType("Client")]
    public class ExportClientWithTheirInvoiceDto
    {
        [XmlAttribute("InvoicesCount")]
        public int InvoicesCount { get; set; }

        [XmlElement("ClientName")]
        public string ClientName { get; set; } = null!;

        [XmlElement("VatNumber")]
        public string VatNumber { get; set; } = null!;
        
        [XmlArray("Invoices")]
        public ExportInvoiceDto[] Invoices { get; set; } = null!;
    }
}
