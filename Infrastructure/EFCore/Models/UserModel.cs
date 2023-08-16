using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Domain;

namespace EFCore.Models;


public class User
{
    public int Id { get; set; }

    [MaxLength(15)]
    public string Login {get; set; } = null!;

    [MaxLength(30)]
    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime RegisteredDate { get; set; }

    public DateTime LastAuthedDate { get; set; }

    public DateTime? LastUpdatedDate { get; set; }

    public UserRole Role { get; set; }

    
}
    