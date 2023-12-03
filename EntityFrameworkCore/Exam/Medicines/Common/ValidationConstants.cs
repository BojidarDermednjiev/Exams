namespace Medicines.Common
{
    public static class ValidationConstants
    {
        // Pharmacy
        public const int PharmacyNameMinLength = 2;
        public const int PharmacyNameMaxLength = 50;
        public const string PharmacyRegexPhoneNumber = @"^\(\d{3}\) \d{3}-\d{4}$";
        // Medicine 
        public const int MedicineNameMinLength = 3;
        public const int MedicineNameMaxLength = 150;
        public const double MedicinePriceMin = 0.01;
        public const double MedicinePriceMax = 1000.00;
        public const int MedicineCategoryMinRange = 0;
        public const int MedicineCategoryMaxRange = 4;
        public const int MedicineProducerMin = 3;
        public const int MedicineProducerMax = 100;
        // Patient 
        public const int PatientFullNameMin = 5;
        public const int PatientFullNameMax = 100;
        public const int PatientAgeGroupMinRange = 0;
        public const int PatientAgeGroupMaxRange = 2;
        public const int PatientGenderMinRange = 0;
        public const int PatientGenderMaxRange = 1;
    }
}
