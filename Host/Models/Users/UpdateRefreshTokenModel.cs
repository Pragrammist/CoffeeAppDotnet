using Host.Infrastructure.ValidationAttributes;

namespace Host.Models.Users
{
    public class UpdateRefreshTokenModel
    {
        public string RefreshToken { get; set; } = null!;


        [Id]
        public int UserId { get; set; }
    }
}
