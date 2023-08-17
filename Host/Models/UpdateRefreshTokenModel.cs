namespace Host.Models
{
    public class UpdateRefreshTokenModel
    {
        public string RefreshToken { get; set; } = null!;

        public int UserId { get; set; }
    }
}
