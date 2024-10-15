using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using static AppointMeWeb.Core.Constants.MessageConstants;

namespace AppointMeWeb.Core.CustomValidations
{
    public class MinAgeAttribute : ValidationAttribute
    {
        private readonly int minAge;

        public MinAgeAttribute(int _minAge)
        {
            minAge = _minAge;
            
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            ErrorMessage = string.Format(MinAgeErrorMsg, minAge);

            if (value is DateOnly birthDate)
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                var age = today.Year - birthDate.Year;
                Debugger.Break();
                if (birthDate > today.AddYears(-age))
                {
                    age--;
                }

                if (age < minAge)
                {
                    return new ValidationResult(ErrorMessage);
                }
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage);

        }
    }
}
