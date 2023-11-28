using AutoMapper.QueryableExtensions;
using Invoices.DataProcessor.ExportDto;
using Invoices.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Invoices.DataProcessor
{
    using AutoMapper;
    
    using Data;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportClientsWithTheirInvoices(InvoicesContext context, DateTime date)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper xmlHelper = new XmlHelper();
            ExportClientWithTheirInvoiceDto[] clientsDto = context.Clients
                .Where(c => c.Invoices.Any(ci => ci.IssueDate >= date))
                .AsNoTracking()
                .OrderByDescending(c => c.Invoices.Count)
                .ThenBy(c => c.Name)
                .ProjectTo<ExportClientWithTheirInvoiceDto>(mapper.ConfigurationProvider)
                .ToArray();
            return xmlHelper.Serialize<ExportClientWithTheirInvoiceDto[]>(clientsDto, "Clients");
        }

        public static string ExportProductsWithMostClients(InvoicesContext context, int nameLength)
        {
            var products = context
                .Products
                .Where(p => p.ProductsClients.Any(pc => pc.Client.Name.Length >= nameLength))
                .ToArray()
                .Select(p => new
                {
                    p.Name,
                    p.Price,
                    Category = p.CategoryType.ToString(),
                    Clients = p.ProductsClients
                        .Where(pc => pc.Client.Name.Length >= nameLength)
                        .ToArray()
                        .OrderBy(pc => pc.Client.Name)
                        .Select(pc => new
                        {
                            Name = pc.Client.Name,
                            NumberVat = pc.Client.NumberVat,
                        })
                        .ToArray()
                })
                .OrderByDescending(p => p.Clients.Length)
                .ThenBy(p => p.Name)
                .Take(5)
                .ToArray();

            return JsonConvert.SerializeObject(products, Formatting.Indented);

        }
        private static IMapper InitializeAutoMapper()
            => new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<InvoicesProfile>();
            }));
    }
}