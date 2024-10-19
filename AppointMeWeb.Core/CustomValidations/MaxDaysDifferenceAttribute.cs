using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointMeWeb.Core.CustomValidations
{
    public class MaxDaysDifferenceAttribute : ValidationAttribute
    {
        private readonly int _maxDaysDifference;

        public MaxDaysDifferenceAttribute(int maxDaysDifference)
        {
            _maxDaysDifference = maxDaysDifference;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateOnly endDate = (DateOnly)value;
            var startDateProperty = validationContext.ObjectType.GetProperty("StartDate");

            if (startDateProperty == null)
            {
                return new ValidationResult("Start Date property not found.");
            }

            DateOnly startDate = (DateOnly)startDateProperty.GetValue(validationContext.ObjectInstance, null);

            int daysDifference = endDate.DayNumber- startDate.DayNumber;

            if (daysDifference>0 && daysDifference > _maxDaysDifference)
            {
                return new ValidationResult($"The difference between Start Date and End Date cannot exceed {_maxDaysDifference} days.");
            }

            return ValidationResult.Success;
        }
    }
}
