using Mapster;
using System.ComponentModel.DataAnnotations;

namespace Host.Models
{
    public class LoginUserModel : IValidatableObject
    {
        public string LoginOrEmail { get; set; } = null!;

        public string Password { get; set; } = null!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            var asEmailRes = ValidateAsEmail.Validate(validationContext);

            var asLoginRes = ValidateAsLogin.Validate(validationContext);

            if(asEmailRes.Count() > 0 && asLoginRes.Count() > 0)
            {
                var validDistRes = asEmailRes.Union(asLoginRes);

                foreach(var item in validDistRes)
                    yield return item;
                
            }


        }
        UserObjectValidator ValidateAsLogin => new UserObjectValidator
        {
            Login = LoginOrEmail,
            Password = Password
        };

        UserObjectValidator ValidateAsEmail => new UserObjectValidator
        {
            Email = LoginOrEmail,
            Password = Password
        };

}

}
