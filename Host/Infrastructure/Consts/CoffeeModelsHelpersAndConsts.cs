namespace Host.Infrastructure.Consts
{
    public static class CoffeeModelsHelpersAndConsts
    {
        public const int MAX_LENGTH_NAME_CONST = 100;
        public const int MIN_LENGTH_NAME_CONST = 3;

        public const string MESSAGE_NAME_NOT_VALID_LENGTH = "Длина названия кофе не валидна";


        public const int MAX_LENGTH_NOTE_CONST = 1000;
        public const int MIN_LENGTH_NOTE_CONST = 10;

        public const string MESSAGE_NOTE_NOT_VALID_LENGTH = "Длина примичания не валидна";

        public const int WIDTH_IMAGE_SIZE = 500;
        public const int HEIGHT_IMAGE_SIZE = 500;

        public const int MAX_LENGTH_PHOTO = 1024 * 1024; //1мб

        public static readonly int MAX_LENGTH_PHOTO_MB = MAX_LENGTH_PHOTO / 1024 / 1024;

        public const int MIN_ID_VALUE = 1;
    }
}
