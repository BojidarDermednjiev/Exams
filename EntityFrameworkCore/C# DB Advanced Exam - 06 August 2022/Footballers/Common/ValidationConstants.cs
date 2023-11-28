namespace Footballers.Common
{
    public static class ValidationConstants
    {
        // Coach
        public const int CoachNameMinLength = 2;
        public const int CoachNameMaxLength = 40;
        // Footballer
        public const int FootballerNameMinLength = 2;
        public const int FootballerNameMaxLength = 40;
        public const int FootballerPositionMinRange = 0;
        public const int FootballerPositionMaxRange = 3;
        public const int FootballerBestSkillMinRange = 0;
        public const int FootballerBestSkillMaxRange = 4;
        // Team
        public const int TeamNameMinLength = 3;
        public const int TeamNameMaxLength = 40;
        public const int TeamNationalityMinLength = 2;
        public const int TeamNationalityMaxLength = 40;
        public const string RegexSpecificationsName = @"^[A-Za-z0-9\s\.\-]{3,}$";
    }
}
