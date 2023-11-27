using Newtonsoft.Json;

namespace Invoices.DataProcessor
{
    using System.Text;
    using AutoMapper;
    using System.ComponentModel.DataAnnotations;

    using Data;
    using ImportDto;
    using Utilities;
    using Data.Models;
    using System.Xml.Serialization;
    using System.Globalization;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedClients
            = "Successfully imported client {0}.";

        private const string SuccessfullyImportedInvoices
            = "Successfully imported invoice with number {0}.";

        private const string SuccessfullyImportedProducts
            = "Successfully imported product - {0} with {1} clients.";

        private static XmlHelper xmlHelper;

        public static string ImportClients(InvoicesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            IMapper mapper = InitializeAutoMapper();
            xmlHelper = new XmlHelper();
            ImportClientDto[] clientDtos = xmlHelper.Deserialize<ImportClientDto[]>(xmlString, "Clients");
            ICollection<Client> clients = new HashSet<Client>();
            foreach (ImportClientDto importClientDto in clientDtos)
            {
                if (!IsValid(importClientDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Client client = mapper.Map<Client>(importClientDto);
                foreach (ImportAddressDto importAddressDto in importClientDto.Addresses)
                {
                    if (!IsValid(importAddressDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        client.Addresses = new HashSet<Address>();
                        continue;
                    }
                    Address address = mapper.Map<Address>(importAddressDto);
                    client.Addresses.Add(address);
                }
                clients.Add(client);
                sb.AppendLine(string.Format(SuccessfullyImportedClients, client.Name));
            }

            context.Clients.AddRange(clients);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }


        public static string ImportInvoices(InvoicesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            IMapper mapper = InitializeAutoMapper();
            ImportInvoiceDto[] invoiceDtos = JsonConvert.DeserializeObject<ImportInvoiceDto[]>(jsonString);
            ICollection<Invoice> invoices = new HashSet<Invoice>();
            foreach (var invoiceDto in invoiceDtos)
            {
                if (!IsValid(invoiceDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Invoice invoice = mapper.Map<Invoice>(invoiceDto);

                if (invoice.IssueDate > invoice.DueDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                invoices.Add(invoice);
                sb.AppendLine(string.Format(SuccessfullyImportedInvoices, invoice.Number));
            }
            context.Invoices.AddRange(invoices);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportProducts(InvoicesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            IMapper mapper = InitializeAutoMapper();
            ImportProductDto[] productDtos = JsonConvert.DeserializeObject<ImportProductDto[]>(jsonString);
            ICollection<Product> products = new HashSet<Product>();
            foreach (var importProductDto in productDtos)
            {
                if (!IsValid(importProductDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Product product = mapper.Map<Product>(importProductDto);
                foreach (var c in importProductDto.Clients.Distinct())
                {
                    Client client = context.Clients.Find(c)!;

                    // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract

                    if (client == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    product.ProductsClients.Add(new ProductClient()
                    {
                        Client = client
                    });
                }
                products.Add(product);
                sb.AppendLine(string.Format(SuccessfullyImportedProducts, product.Name, product.ProductsClients.Count));
            }
            context.Products.AddRange(products);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
        private static IMapper InitializeAutoMapper()
            => new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<InvoicesProfile>();
            }));
    }
}
