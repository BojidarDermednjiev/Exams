namespace Invoices
{
    using AutoMapper;

    using Data.Models;
    using DataProcessor.ImportDto;
    using DataProcessor.ExportDto;
    using System.Globalization;

    public class InvoicesProfile : Profile
    {
        public InvoicesProfile()
        {
            // Address
            this.CreateMap<ImportAddressDto, Address>();
            // Client
            this.CreateMap<ImportClientDto, Client>();
            this.CreateMap<Client, ExportClientDto>();
            this.CreateMap<Client, ExportClientWithTheirInvoiceDto>()
                .ForMember(d => d.ClientName, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.VatNumber, opt => opt.MapFrom(s => s.NumberVat))
                .ForMember(d => d.Invoices, opt => opt.MapFrom(s => s.Invoices.ToArray().OrderBy(i => i.IssueDate).ThenByDescending(i => i.DueDate)));
            // Invoice
            this.CreateMap<ImportInvoiceDto, Invoice>();
            this.CreateMap<Invoice, ExportInvoiceDto>()
                .ForMember(d => d.InvoiceNumber, opt => opt.MapFrom(s => s.Number))
                .ForMember(d => d.InvoiceAmount, opt => opt.MapFrom(s => s.Amount))
                .ForMember(d => d.DueDate, opt => opt.MapFrom(s => s.DueDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.CurrencyType.ToString()));
            // Product
            this.CreateMap<ImportProductDto, Product>();
            this.CreateMap<Product, ExportProductsWithMostClientDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price))
                .ForMember(d => d.CategoryType, opt => opt.MapFrom(s => s.CategoryType))
                .ForMember(d => d.Clients, opt => opt.MapFrom(s => s.ProductsClients
                    .Where(pc => pc.Client.Name.Length >= 11)
                    .Select(pc => pc.Client)
                    .OrderBy(c => c.Name)
                    .ToArray()));
        }
    }
}
