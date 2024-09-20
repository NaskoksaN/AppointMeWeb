using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AppointMeWeb.Core.Models.BusinessProvider
{
    public  class DailyScheduleViewModel
    {
        [Required]
        public DayOfWeek Day { get; set; }

        
        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }
        [Required]
        public bool IsDayOff {  get; set; }=false;
    }
}
