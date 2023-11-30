x`namespace Artillery.Common
{
    public static class ValidationConstants
    {
        // Country
        public const int CountryNameMinLength = 4;
        public const int CountryNameMaxLength = 60;
        public const int CountryArmySizeMin = 50_000;
        public const int CountryArmySizeMax = 10_000_000;
        // Manufacturer
        public const int ManufacturerNameMinLength = 4;
        public const int ManufacturerNameMaxLength = 40;
        public const int ManufacturerFoundedMinLength = 10;
        public const int ManufacturerFoundedMaxLength = 100;
        // Shell
        public const double ShellMinWeight = 2;
        public const double ShellMaxWeight = 1680;
        public const int ShellCaliberMin = 4;
        public const int ShellCaliberMax = 30;
        // Gun
        public const int GunMinWeight = 100;
        public const int GunMaxWeight = 1_350_000;
        public const double GunBarrelMinLength = 2.00;
        public const double GunBarrelMaxLength = 35.00;
        public const int GunMinRange = 1;
        public const int GunMaxRange = 1_00_000;
    }
}
