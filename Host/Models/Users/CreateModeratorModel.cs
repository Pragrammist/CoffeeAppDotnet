using static Host.Infrastructure.Consts.UserModelsHelpersAncConsts;
using Host.Infrastructure.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Host.Models.Users
{
    public class CreateModeratorModel
    {
        [StringLength(MAX_LENGTH_LOGIN, MinimumLength = MIN_LENGTH_LOGIN, ErrorMessage = MESSAGE_LENGTH_FOR_INVALID_LOGIN)]
        [RegularExpression(LOGIN_REGEX, ErrorMessage = MESSAGE_INVALID_LOGIN)]
        public string Login { get; set; } = null!;


        [StringLength(MAX_LENGTH_PASSWORD, MinimumLength = MIN_LENGTH_PASSWORD, ErrorMessage = MESSAGE_LENGTH_FOR_INVALID_PASSWORD)]
        [PasswordRegex(ErrorMessage = MESSAGE_INVALID_PASSWORD)]
        public string Password { get; set; } = null!;


        [Compare(nameof(Password))]
        public string Password2 { get; set; } = null!;


        [EmailAddress(ErrorMessage = MESSAGE_INVALID_EMAIL)]
        public string Email { get; set; } = null!;






        public UserRole Role { get; } = UserRole.MODERATOR;
    }

}
