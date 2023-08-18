using Mapster;
using System.ComponentModel.DataAnnotations;

namespace Host.Models
{
    public class ChangeModeratorLogin : IValidatableObject
    {
        public string Login { get; set; } = null!; 
        public int UserId { get; set; } 
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var varlRes = this.Adapt<UserObjectValidator>().Validate(validationContext);

            foreach (var item in varlRes) 
                yield return item;

            if (UserId < 1)
                yield return new ValidationResult("Айди пользователя не может быть меньше 1");
        }
    }
}
