

using Services.Dtos.BaseDtos;

namespace Services.Dtos.Auth
{
    public class RefreshTokenDto : BaseOutputDto
    {
        public string Token { get; set; } = default!;
        public DateTimeOffset Expires { get; set; }
    }
}