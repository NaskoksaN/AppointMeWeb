using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return new ValidationResult("Start Date property not found.");
            }

            var startDate = (DateOnly)startDateProperty.GetValue(validationContext.ObjectInstance, null);
            int daysDifference = endDate.DayNumber - startDate.DayNumber;
            if (daysDifference < 0)
            {
                return new ValidationResult("End Date must be greater than or equal to Start Date.");
            }

            return ValidationResult.Success;
        }
    }
}
