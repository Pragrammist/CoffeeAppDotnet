using Domain.Exceptions;
using Domain.Helpers.Hashing;
using Domain.Options;
using EFCore;
using EFCore.Models;
using Microsoft.Extensions.Options;
using Services.Common;
using Services.Contracts;
using Services.Dtos.Auth;

namespace Services
{
    public class AuthService : DbServiceBase<RefreshTokenDto, RefreshToken>, IAuthService
    {
        RefreshTokenOptions _authOptions;
        public AuthService(IRepository repository, IOptions<RefreshTokenOptions> authOptions) : base(repository) 
        {
            _authOptions = authOptions.Value;
            
        }
        


        public async Task<RefreshTokenDto> GenerateRefreshToken(int userId)
        {
            var existingToken = _dbRepository.Set<RefreshToken>().FirstOrDefault(u => u.UserId == userId);

            if(existingToken is not null)
            {
                UpdateValues(existingToken);

                return await base.Edit(existingToken, true);
            }


            var user = await _dbRepository.GetByIdAsync<User>(userId);

            var refreshToken = GetRefreshToken(user);
            
            return await base.Create(refreshToken, true);
        }

        

        string GenerateToken() => _authOptions.GetRandomValue().GetRandomHash();

        RefreshToken GetRefreshToken(User user) => new RefreshToken
        {
            User = user,
            Expires = _authOptions.GetDateTimeForRefreshTokenExpiredTime(),
            Token = GenerateToken()
        };

        void UpdateValues(RefreshToken refreshToken)
        {
            refreshToken.Token = GenerateToken();
            refreshToken.Expires = _authOptions.GetDateTimeForRefreshTokenExpiredTime();
            
        }

        public async Task<RefreshTokenDto> UpdateRefreshToken(int userId, string oldToken)
        {
            var existingToken = _dbRepository.Set<RefreshToken>().FirstOrDefault(u => 
                u.UserId == userId && 
                u.Token == oldToken && 
                u.Expires >= DateTimeOffset.Now
            ) ?? throw new UserDataNotValidException("refreshtoken is invalid", 10); ;



            UpdateValues(existingToken);

            return await base.Edit(existingToken, true);
        }
    }

}