using System.ComponentModel.DataAnnotations;

namespace Host.Infrastructure.ValidationAttributes
{
    public class IdAttribute : ValidationAttribute
    {
        public IdAttribute() 
        {
            ErrorMessage = "Проверьте введеный айди";
        }

        public override bool IsValid(object? value)
        {
            if (value is null || value is not int)
                return false;

            var id = (int)value;

            return id > 0;
        }
    }
}
