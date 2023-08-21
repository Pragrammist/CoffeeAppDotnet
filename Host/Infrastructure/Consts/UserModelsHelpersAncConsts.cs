using System.Text.RegularExpressions;


namespace Host.Infrastructure.Consts
{
    public static class UserModelsHelpersAncConsts
    {
        public const int MIN_LENGTH_LOGIN = 3;
        public const int MAX_LENGTH_LOGIN = 15;

        public const int MIN_LENGTH_PASSWORD = 8;
        public const int MAX_LENGTH_PASSWORD = 30;

        

        public const string MESSAGE_LENGTH_FOR_INVALID_LOGIN = "Логин сликшом короткий или слишком длинный";
        public const string MESSAGE_LENGTH_FOR_INVALID_PASSWORD = "Пароль сликшом короткий или слишком длинный";

        public const string MESSAGE_INVALID_LOGIN = "Проверьте логин";
        public const string MESSAGE_INVALID_PASSWORD = "Пароль должен быть сложным";
        public const string MESSAGE_INVALID_EMAIL = "Проверьте почту";

        public const string LOGIN_REGEX = @"^[a-zA-Z0-9_\-]+$";

        public static Regex[] PasswordRegexes => new[]
        {
            new Regex(@"[0-9]+"),
            new Regex(@"[A-Z]+")
        };
    }

}
