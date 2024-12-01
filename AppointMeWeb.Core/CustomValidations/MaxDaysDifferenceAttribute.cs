using System.ComponentModel.DataAnnotations;

using static AppointMeWeb.Core.Constants.MessageConstants;

namespace AppointMeWeb.Core.CustomValidations
{
    public class MaxDaysDifferenceAttribute : ValidationAttribute
    {
        private readonly int maxDaysDifference;

        public MaxDaysDifferenceAttribute(int _maxDaysDifference)
        {
            maxDaysDifference = _maxDaysDifference;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateOnly endDate = (DateOnly)value;
            var startDateProperty = validationContext.ObjectType.GetProperty("StartDate");

            if (startDateProperty == null)
            {
                return new ValidationResult(StartDateNotFoundErrMsg);
            }

            DateOnly startDate = (DateOnly)startDateProperty.GetValue(validationContext.ObjectInstance, null);

            int daysDifference = endDate.DayNumber- startDate.DayNumber;

            if (daysDifference>0 && daysDifference > maxDaysDifference)
            {
                string errorMessage = string.Format(SchedulePeriodErrMsg, maxDaysDifference);
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
