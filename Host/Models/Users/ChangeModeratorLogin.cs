using static Host.Infrastructure.Consts.UserModelsHelpersAncConsts;
using System.ComponentModel.DataAnnotations;
using Host.Infrastructure.ValidationAttributes;

namespace Host.Models.Users
{
    public class ChangeModeratorLogin
    {

        [StringLength(MAX_LENGTH_LOGIN, MinimumLength = MIN_LENGTH_LOGIN, ErrorMessage = MESSAGE_LENGTH_FOR_INVALID_LOGIN)]
        [RegularExpression(LOGIN_REGEX, ErrorMessage = MESSAGE_INVALID_LOGIN)]
        public string Login { get; set; } = null!;

        [Id]
        public int UserId { get; set; }

    }
}
