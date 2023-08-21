using Mapster;
using System.ComponentModel.DataAnnotations;

namespace Host.Models.Users
{
    public class LoginUserModel
    {
        public string LoginOrEmail { get; set; } = null!;

        public string Password { get; set; } = null!;



    }

}
