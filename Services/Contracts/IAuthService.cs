using Services.Dtos.Auth;

namespace Services.Contracts
{
    public interface IAuthService
    {
        Task<RefreshTokenDto> GenerateRefreshToken(int userId);

        Task<RefreshTokenDto> UpdateRefreshToken(int userId, string oldToken);
    }
}