using Host.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Dtos.User;

namespace Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "ADMIN")]
    public class UserController : ControllerBase
    {
        IUserService _userService;
        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        public async Task<IActionResult> CreateModerator([FromBody]CreateModeratorModel createModeratorModel)
        {
            var user = createModeratorModel.Adapt<CreateUserDto>();

            var createdUser = await _userService.CreateUser(user);

            return Ok(new
            {
                User = createdUser,
            });
        }
    }
}