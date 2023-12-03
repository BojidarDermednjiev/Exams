namespace Medicines.DataProcessor.ExportDtos
{
    public class ExportMedicinesFromDesiredCategoryInNonStopPharmaciesDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public ICollection<ExportPharmacyDto> Pharmacies { get; set; } = null!;
    }
}
