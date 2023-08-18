using Mapster;
using System.ComponentModel.DataAnnotations;


namespace Host.Models
{

    public class RegisterUserModel : IValidatableObject
    {
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Password2 { get; set; } = null!;

        public string Email { get; set; } = null!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => this.Adapt<UserObjectValidator>().Validate(validationContext);

    }

}
