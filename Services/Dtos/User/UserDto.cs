using Domain;
using Services.Dtos.BaseDtos;

namespace Services.Dtos.User
{
    public class UserDto : BaseOutputDto
    {
        public string Login { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime LastAuthedDate { get; set; }

        public UserRole Role { get; set; }
    }
    
}