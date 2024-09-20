using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppointMeWeb.Infrastrucure.Data.Models
{
    [Comment("Working schedule for each service provider")]
    public class WorkingSchedule
    {
        [Key]
        [Comment("Schedule identifier")]
        public int Id { get; set; }

        [Required]
        [Comment("Foreign key to BusinessServiceProvider")]
        public int BusinessServiceProviderId { get; set; }

        [Required]
        [Comment("Day of the week")]
        public DayOfWeek Day { get; set; }

        
        [Comment("Start time of work")]
        public TimeSpan? StartTime { get; set; }

      
        [Comment("End time of work")]
        public TimeSpan? EndTime { get; set; }

        [Required]
        [Comment("Indicates whether the day is a day off for the service provider.")]
        public bool IsDayOff { get; set; }

        [ForeignKey(nameof(BusinessServiceProviderId))]
        public  BusinessServiceProvider BusinessServiceProvider { get; set; } = null!;
    }
}