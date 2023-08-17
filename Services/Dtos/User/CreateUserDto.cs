using Domain;
using System.ComponentModel.DataAnnotations;

namespace Services.Dtos.User
{
    public class CreateUserDto
    {
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
        
        public string Email { get; set; } = null!;

        public UserRole Role { get; set; }
    }

}