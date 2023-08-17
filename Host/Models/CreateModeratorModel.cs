using Domain;
using System.ComponentModel.DataAnnotations;
using static Host.Models.UserModelsHelpersAncConsts;

namespace Host.Models
{
    public class CreateModeratorModel
    {

        [StringLength(MAX_LENGTH_LOGIN, MinimumLength = MIN_LENGTH_LOGIN)]
        [RegularExpression(LOGIN_REGEX)]
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        [StringLength(MAX_LENGTH_PASSWORD, MinimumLength = MIN_LENGTH_PASSWORD)]
        [Compare(nameof(Password))]
        public string Password2 { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!ValidataePassword())
                yield return new ValidationResult("Пароль должен быть сложным", new[] { nameof(Password) });
        }

        bool ValidataePassword() => PasswordRegexes.IsMatch(Password);


        public UserRole Role { get; } = UserRole.MODERATOR;
    }

}
