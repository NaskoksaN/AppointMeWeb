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



            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            if (value == null || !(value is DateOnly birthDate))
            {
                return new ValidationResult("Birth date is required.");
            }

            int age = today.Year - birthDate.Year;
            if (today < birthDate.AddYears(age))
            {
                age--;
            }
            if (age < minAge)
            {
                string errorMessage = string.Format(MinAgeErrorMsg, minAge);
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
