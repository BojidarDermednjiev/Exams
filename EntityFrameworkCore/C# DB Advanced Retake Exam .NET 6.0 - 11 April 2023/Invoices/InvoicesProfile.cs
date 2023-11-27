namespace Invoices
{
    using AutoMapper;
    using System.Globalization;

    using Data.Models;
    using DataProcessor.ImportDto;
    using Data.Models.Enums;
    public class InvoicesProfile : Profile
    {
        public InvoicesProfile()
        {
            this.CreateMap<ImportAddressDto, Address>();
            this.CreateMap<ImportClientDto, Client>();
            this.CreateMap<ImportInvoiceDto, Invoice>();
            this.CreateMap<ImportProductDto, Product>();
        }
    }
}
