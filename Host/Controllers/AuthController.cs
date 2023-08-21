using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Dtos.User;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Domain;
using EFCore.Models;
using Domain.Exceptions;
using Host.Infrastructure.Consts;
using Host.Models.Users;

namespace Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase 
    {
        IUserService _userService;
        IAuthService _authService;
        BearerAccessTokenOptions _bearerOptions;
        public AuthController(IUserService userService, IOptions<BearerAccessTokenOptions> bearerOptions, IAuthService authService)
        {
            _authService = authService;
            _userService = userService;
            _bearerOptions = bearerOptions.Value;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserModel userData)
        {
            var createdUser = await _userService.CreateUser(userData.Adapt<CreateUserDto>());

            return await GetUserResult(createdUser);
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody]UpdateRefreshTokenModel refreshTokenModel)
        {
            try
            {
                var refreshToken = await _authService.UpdateRefreshToken(refreshTokenModel.UserId, refreshTokenModel.RefreshToken);

                return Ok(refreshToken);
            }
            catch(UserDataNotValidException)
            {
                return Unauthorized();
            }
            catch
            {
                throw;
            }
            
        }

        async Task<IActionResult> GetUserResult(UserDto user)
        {
            var accessToken = AccessToken(user);
            var token = await _authService.GenerateRefreshToken(user.Id);
            return Ok(new
            {
                User = user,
                AccessToken = accessToken,
                RefreshToken = token
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUserModel loginUserModel)
        {
            var user = await _userService.LoginUser(loginUserModel.Adapt<LoginUserDto>());
            return await GetUserResult(user);
        }
        public string AccessToken(UserDto user)
        {
            var claims = GetClaims(user);


            var encodedJwt = _bearerOptions.GetBearerToken(claims);

            return encodedJwt;
        }
        private List<Claim> GetClaims(UserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsConst.LOGIN, user.Login),
                new Claim(ClaimsConst.ROLE, user.Role.ToString()),
                new Claim(ClaimsConst.EMAIL, user.Email),
                new Claim(ClaimsConst.ID, user.Id.ToString())
            };
            return claims;

        }


    }
}