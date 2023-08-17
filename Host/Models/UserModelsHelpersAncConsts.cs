using System.Text.RegularExpressions;

namespace Host.Models
{
    public static class UserModelsHelpersAncConsts
    {
        public const int MIN_LENGTH_LOGIN = 3;
        public const int MAX_LENGTH_LOGIN = 15;

        public const int MIN_LENGTH_PASSWORD = 8;
        public const int MAX_LENGTH_PASSWORD = 15;

        public const string LOGIN_REGEX = "^[a-zA-Z0-9]+$";

        public static Regex[] PasswordRegexes => new[] 
        {
            new Regex(@"[0-9]+"),
            new Regex(@"[A-Z]+")
        };

        public static bool IsMatch(this Regex[] regexes, string value) => regexes.All(x => x.IsMatch(value));

    }

}
