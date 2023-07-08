namespace SportWave.Common
{
    public static class EntityValidationConstants
    {
        public static class Address
        {
            public const int TownNameMinLength = 2;
            public const int TownNameMaxLength = 60;

            public const int StreetNameMinLength = 2;
            public const int StreetNameMaxLength = 100;

            public const int AdditionalInfoMaxLength = 200;

            public const int CountryNameMaxLength = 56;
            public const int CountryNameMinLength = 4;
        }

        public static class UserReviews
        {
            public const int RatingMaxValue = 10;
            public const int RatingMinValue = 0;

            public const int CommentMaxLength = 100;
        }

        public static class Product
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 5;

            public const int DescriptionMaxLength = 200;
            public const int DescriptionMinLength = 50;

            public const int ColorMaxLength = 30;
            public const int ColorMinLength = 3;

            public const int GenderMaxLength = 6;
        }

        public static class ProductCategory
        {
            public const int CategoryNameMaxLength = 20;
            public const int CategoryNameMinLength = 4;
        }

        public static class PromoCode
        {
            public const int CodeMaxLength = 15;
            public const int CodeMinLength = 10;
        }

        public static class Category
        {
            public const int NameMaxLength = 20;
            public const int NameMinLength = 3;
        }

        public static class PaymentMethod
        {
            public const int CardNumberMaxLength = 16;
            public const int CardNumberMinLength = 16;

            public const int SecurityNumberMaxLength = 4;
            public const int SecurityNumberMinLength = 3;

            public const int ExpiryDateMaxLength = 5;
            public const int ExpiryDateMinLength = 5;
        }
    }
}
