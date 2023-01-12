using System.ComponentModel.DataAnnotations;

namespace Vahid_C_Web_Proj.CustomValidation
{
    public class UitgifteDatum: ValidationAttribute
    {
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var date = (DateTime)value;
                if (date < new DateTime(1980, 1, 1) || date > DateTime.Now)
                {
                    return new ValidationResult("Date of issue not possible before 1/1/1980 and not after 1/1/CurrentYear");
                }
            }
            return ValidationResult.Success;
        }
    }
}
