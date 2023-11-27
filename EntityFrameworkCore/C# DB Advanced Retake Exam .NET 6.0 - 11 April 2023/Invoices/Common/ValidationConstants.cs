namespace Invoices.Common
{
    public static class ValidationConstants
    {
        // Product
        public const int ProductNameMinLength = 9;
        public const int ProductNameMaxLength = 30;
        public const decimal ProductPriceMinRange = 5m;
        public const decimal ProductPriceMaxRange = 1000m;
        public const int ProductCategoryMinTypeRange = 0;
        public const int ProductCategoryMaxTypeRange = 4;
        // Address
        public const int AddressStreetNameMinLength = 10;
        public const int AddressStreetNameMaxLength = 20;
        public const int AddressCityNameMinLength = 5;
        public const int AddressCityNameMaxLength = 15;
        public const int AddressCountryMinName = 5;
        public const int AddressCountryMaxName = 15;
        // Invoice
        public const int InvoiceNumberMinRange = 1_000_000_000;
        public const int InvoiceNumberMaxRange = 1_500_000_000;
        public const int InvoiceCurrencyTypeMinRange = 0;
        public const int InvoiceCurrencyTypeMaxRange = 2;
        // Client
        public const int ClientNameMinLength = 10;
        public const int ClientNameMaxLength = 25;
        public const int ClientNumberVatMinLength = 10;
        public const int ClientNumberVatMaxLength = 15;
    }
}
