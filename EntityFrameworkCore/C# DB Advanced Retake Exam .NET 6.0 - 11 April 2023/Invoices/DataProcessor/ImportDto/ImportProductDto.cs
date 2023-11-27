namespace Invoices.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    using Common;
    using Data.Models.Enums;

    public class ImportProductDto
    {

        [Required]
        [MinLength(ValidationConstants.ProductNameMinLength)]
        [MaxLength(ValidationConstants.ProductNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Range((double)ValidationConstants.ProductPriceMinRange, (double)ValidationConstants.ProductPriceMaxRange)]
        public decimal Price { get; set; }

        [Required]
        [Range(ValidationConstants.ProductCategoryMinTypeRange, ValidationConstants.ProductCategoryMaxTypeRange)]
        public CategoryType CategoryType { get; set; }

        public ICollection<int> Clients { get; set; } = null!;
    }
}
