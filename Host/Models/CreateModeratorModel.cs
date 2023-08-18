using Domain;
using Mapster;
using System.ComponentModel.DataAnnotations;

namespace Host.Models
{
    public class CreateModeratorModel
    {
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Password2 { get; set; } = null!;

        public string Email { get; set; } = null!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var valRes = this.Adapt<UserObjectValidator>().Validate(validationContext);

            foreach (var item in valRes) 
                yield return item;

            if (Role != UserRole.MODERATOR)
                yield return new ValidationResult("Входящая роль может быть только модераторская", new[] {nameof(Role)});
        }





        public UserRole Role { get; } = UserRole.MODERATOR;
    }

}
