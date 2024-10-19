using System.ComponentModel.DataAnnotations;

using static AppointMeWeb.Core.Constants.MessageConstants;

namespace AppointMeWeb.Core.CustomValidations
{
    public class EndDateAfterStartDateAttribute : ValidationAttribute
    {
        private readonly int _maxDaysDifference;

        public EndDateAfterStartDateAttribute(int maxDaysDifference)
        {
            _maxDaysDifference = maxDaysDifference;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var endDate = (DateOnly)value;
            var startDateProperty = validationContext.ObjectType.GetProperty("StartDate");

            if (startDateProperty == null)
            {
                return new ValidationResult(StartDateNotFoundErrMsg);
            }

            var startDate = (DateOnly)startDateProperty.GetValue(validationContext.ObjectInstance, null);
            int daysDifference = endDate.DayNumber - startDate.DayNumber;
            if (daysDifference < 0)
            {
                return new ValidationResult(EndDateErrMsg);
            }

            return ValidationResult.Success;
        }
    }
}
