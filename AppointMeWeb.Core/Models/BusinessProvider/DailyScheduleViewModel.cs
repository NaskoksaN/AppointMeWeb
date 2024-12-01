using System.ComponentModel.DataAnnotations;

using AppointMeWeb.Core.CustomValidations;
using static AppointMeWeb.Core.Constants.MessageConstants;

namespace AppointMeWeb.Core.Models.BusinessProvider
{
    public  class DailyScheduleViewModel
    {
        [Required]
        public DayOfWeek Day { get; set; }

        
        public TimeSpan? StartTime { get; set; }

        [EndTimeAfterStartTime(ErrorMessage = EndTimeBeforeStartTimeErrMsg)]
        public TimeSpan? EndTime { get; set; }
        [Required]
        public bool IsDayOff {  get; set; }=false;
    }
}
