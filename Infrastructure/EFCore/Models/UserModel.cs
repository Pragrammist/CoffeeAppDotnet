using Domain.Enums;

namespace EFCore.Models;


public class User : EntityBase
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime LastAuthedDate { get; set; }

    public UserRole Role { get; set; }
}
    