using Host.Infrastructure.Helpers.Validation;
using System.ComponentModel.DataAnnotations;
using static Host.Infrastructure.Consts.UserModelsHelpersAncConsts;

namespace Host.Infrastructure.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PasswordRegexAttribute : ValidationAttribute
    {

        public PasswordRegexAttribute() 
        {
            
        }

        public override bool IsValid(object? value)
        {
            if(value is null)
                return false;

            

            var str = value.ToString();

            if(str is null)
                return false;

            return PasswordRegexes.IsMatch(str);
        }

    }
}
