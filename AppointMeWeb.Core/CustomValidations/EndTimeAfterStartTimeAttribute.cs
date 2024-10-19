using System.ComponentModel.DataAnnotations;

using static AppointMeWeb.Core.Constants.MessageConstants;

namespace AppointMeWeb.Core.CustomValidations
{
    public class EndTimeAfterStartTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                TimeSpan endTime = (TimeSpan)value;
                var startTimeProperty = validationContext.ObjectType.GetProperty("StartTime");

                if (startTimeProperty == null)
                {
                    return new ValidationResult(StartDateNotFoundErrMsg);
                }

                TimeSpan startTime = (TimeSpan)startTimeProperty.GetValue(validationContext.ObjectInstance, null);

                //var durationProperty = validationContext.ObjectType.GetProperty("AppointmentDuration");

                //if (durationProperty == null)
                //{
                //    return new ValidationResult(StartDateNotFoundErrMsg);
                //}

                //TimeSpan durationTime = (TimeSpan)durationProperty.GetValue(validationContext.ObjectInstance, null);

                if (endTime  <= startTime)
                {
                    return new ValidationResult(EndTimeBeforeStartTimeErrMsg);
                }
            }
                return ValidationResult.Success;
        }
    }
}
