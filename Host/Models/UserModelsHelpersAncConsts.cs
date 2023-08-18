using Domain;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using static Host.Models.UserModelsHelpersAncConsts;

namespace Host.Models
{
    public static class UserModelsHelpersAncConsts
    {
        public const int MIN_LENGTH_LOGIN = 3;
        public const int MAX_LENGTH_LOGIN = 15;

        public const int MIN_LENGTH_PASSWORD = 8;
        public const int MAX_LENGTH_PASSWORD = 30;

        public const string LOGIN_REGEX = @"^[a-zA-Z0-9_\-]+$";

        public static Regex[] PasswordRegexes => new[] 
        {
            new Regex(@"[0-9]+"),
            new Regex(@"[A-Z]+")
        };

        public static bool IsMatch(this Regex[] regexes, string value) => regexes.All(x => x.IsMatch(value));

    }

    public class UserObjectValidator : IValidatableObject
    {
        public string? Login { get; set; }

        public string? Password { get; set; }
        
        public string? Password2 { get; set; }
        
        public string? Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password is not null && !ValidataePasswordSignature(Password))
                yield return new ValidationResult("Пароль должен быть сложным", new[] { nameof(Password) });

            if(Password is not null && !ValidateLength(Password, MIN_LENGTH_PASSWORD, MAX_LENGTH_PASSWORD))
                yield return new ValidationResult("Пароль слишком длинный или короткий", new[] { nameof(Password) });

            if(Password is not null && Password2 is not null && !ValidateEquals(Password, Password2))
                yield return new ValidationResult("Пароли должны совпадать", new[] { nameof(Password2) });

            if(Login is not null && !ValidateLoginSignature(Login))
                yield return new ValidationResult("Логин содержит забретные символы", new[] { nameof(Login) });

            if (Login is not null && !ValidateLength(Login, MIN_LENGTH_LOGIN, MAX_LENGTH_LOGIN))
                yield return new ValidationResult("Логин слишком длинный или короткий", new[] { nameof(Login) });


            if (Email is not null && !ValidateEmailSignature(Email))
                yield return new ValidationResult("Почта не валидна", new[] { nameof(Email) });

            

        }


        bool ValidateEmailSignature (string email) => new EmailAddressAttribute().IsValid(email);

        bool ValidateLoginSignature(string login) => Regex.IsMatch(login, LOGIN_REGEX);

        bool ValidataePasswordSignature(string password) => PasswordRegexes.IsMatch(password);


        bool ValidateLength(string value, int min, int max) => value.Length >= min && value.Length <= max;

        bool ValidateEquals(string value1, string value2) => value1 == value2;
        
        
        
    }

}
