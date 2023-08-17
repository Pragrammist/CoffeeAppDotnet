using Domain;
using Domain.Exceptions;
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
        IHasherService _hasher;
        public AuthService(IRepository repository, IOptions<RefreshTokenOptions> authOptions, IHasherService hasher) : base(repository) 
        {
            _authOptions = authOptions.Value;
            _hasher = hasher;
        }
        


        public async Task<RefreshTokenDto> GenerateRefreshToken(int userId)
        {
            var existingToken = _dbRepository.GetItems<RefreshToken>().FirstOrDefault(u => u.UserId == userId);

            if(existingToken is not null)
            {
                UpdateValues(existingToken);

                return await base.Edit(existingToken, true);
            }


            var user = await _dbRepository.GetByIdAsync<User>(userId);

            var refreshToken = GetRefreshToken(user);
            
            return await base.Create(refreshToken, true);
        }

        

        string GenerateToken() => _hasher.GetHashForToken(_authOptions.GetRandomValue());

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
            var existingToken = _dbRepository.GetItems<RefreshToken>().FirstOrDefault(u => 
                u.UserId == userId && 
                u.Token == oldToken && 
                u.Expires >= DateTimeOffset.Now
            ) ?? throw new UserDataNotValidException("refreshtoken is invalid"); ;



            UpdateValues(existingToken);

            return await base.Edit(existingToken, true);
        }
    }

}