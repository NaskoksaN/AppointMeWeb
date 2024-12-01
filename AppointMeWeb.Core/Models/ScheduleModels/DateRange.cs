using AppointMeWeb.Core.CustomValidations;
using System.ComponentModel.DataAnnotations;
using static AppointMeWeb.Core.Constants.CoreConstants;
using static AppointMeWeb.Core.Constants.MessageConstants;

namespace AppointMeWeb.Core.Models.Schedule
{
    public class DateRange
    {
        [Required(ErrorMessage = RequiredMessage)]
        public DateOnly StartDate { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [EndDateAfterStartDate]
        [MaxDaysDifference(MaxDaySchedulePeriod, ErrorMessage = SchedulePeriodErrMsg)]
        public DateOnly EndDate { get; set; }   
    }
}
