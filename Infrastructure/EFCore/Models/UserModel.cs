using Domain;

namespace EFCore.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Login {get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime RegisteredDate { get; set; }

        public DateTime LastAuthedDate { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public UserRole Role { get; set; }
        
    }
}

    