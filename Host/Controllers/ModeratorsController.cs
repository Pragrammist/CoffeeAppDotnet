using EFCore.Models;
using Host.Models.Users;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using Services.Dtos.User;

namespace Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "ADMIN")]
    public class ModeratorsController : ControllerBase
    {
        IUserService _userService;
        public ModeratorsController(IUserService userService) 
        {
            _userService = userService;


        }

        [HttpPost]
        public async Task<IActionResult> CreateModerator([FromBody]CreateModeratorModel createModeratorModel)
        {
            var user = createModeratorModel.Adapt<CreateUserDto>();

            var createdUser = await _userService.CreateUser(user);

            return OkResultWithUser(createdUser);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteModerator(int userId)
        {
            await _userService.DeleteModerator(userId);

            return Ok();
        }

        [HttpPut("password")]
        public async Task<IActionResult> GeneratePaswsord(int userId)
        {
            var newPassword = await _userService.GenerateNewPasswordForModerator(userId);

            return Ok( new
                {
                    Password = newPassword
                }
            );
        }

        [HttpPut("login")]
        public async Task<IActionResult> GenerateLogin([FromBody]ChangeModeratorLogin data)
        {
            var user = await _userService.ChangeLoginForModerator(data.Login, data.UserId);

            return OkResultWithUser(user);
        }

        [HttpGet]
        public IAsyncEnumerable<UserDto> GetUsers()
        => _userService.GetModerators().AsAsyncEnumerable();

        IActionResult OkResultWithUser(UserDto user) => Ok(
            new
                {
                    User = user
                }
            );
    }
}