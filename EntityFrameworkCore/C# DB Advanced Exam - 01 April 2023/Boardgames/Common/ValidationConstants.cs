namespace Boardgames.Common
{
    public static class ValidationConstants
    {
        // Boardgame
        public const int BoardGameMinLength = 10;
        public const int BoardGameMaxLength = 20;
        public const double BoardGameRatingMin = 1;
        public const double BoardGameRatingMax = 10;
        public const int BoardGameYearPublishedMin = 2018;
        public const int BoardGameYearPublishedMax = 2023;
        public const int BoardGameCategoryTypeMin = 0;
        public const int BoardGameCategoryTypeMax = 4;
        // Seller
        public const int SellerNameMinLength = 5;
        public const int SellerNameMaxLength = 20;
        public const int SellerAddressNameMinLength = 2;
        public const int SellerAddressNameMaxLength = 30;
        public const string SellerWebsiteRegex = @"(www\.[a-zA-Z0-9\-]{2,256}\.com)";
        // Creator
        public const int CreatorFirstNameMinLength = 2;
        public const int CreatorFirstNameMaxLength = 7;
        public const int CreatorSecondNameMinLength = 2;
        public const int CreatorSecondNameMaxLength = 7;
        
    }
}
