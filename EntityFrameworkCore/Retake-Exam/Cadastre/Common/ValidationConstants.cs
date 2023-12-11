namespace Cadastre.Common
{
    public static class ValidationConstants
    {
        // District
        public const int DistrictNameMinLength = 2;
        public const int DistrictNameMaxLength = 80;
        public const string DistrictPostalCodeRegex = @"^[A-Z]{2}-\d{5}$";
        public const int DistrictRegionMinValue = 0;
        public const int DistrictRegionMaxValue = 3;
        // Property
        public const int PropertyIdentifierMinLength = 16;
        public const int PropertyIdentifierMaxLength = 20;
        public const int PropertyDetailsMinLength = 5;
        public const int PropertyDetailsMaxLength = 500;
        public const int PropertyAddressMinLength = 5;
        public const int PropertyAddressMaxLength = 200;
        public const int PropertyAreaMinRange = 0;
        public const int PropertyAreaMaxRange = 2_147_483_647;
        // Citizen
        public const int CitizenFirstNameMinLength = 2;
        public const int CitizenFirstNameMaxLength = 30;
        public const int CitizenLastNameMinLength = 2;
        public const int CitizenLastNameMaxLength = 30;
        public const int CitizenMaritalStatusMinValue = 0;
        public const int CitizenMaritalStatusMaxValue = 3;
    }
}
