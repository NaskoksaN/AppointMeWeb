using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AppointMeWeb.Core.Models.BusinessProvider
{
    public  class DailyScheduleViewModel
    {
        [Required]
        public DayOfWeek Day { get; set; }

        
        [Comment("Start time of work")]
        public TimeSpan? StartTime { get; set; }

        
        [Comment("End time of work")]
        public TimeSpan? EndTime { get; set; }
        [Required]
        public bool IsDayOff {  get; set; }=false;
    }
}
