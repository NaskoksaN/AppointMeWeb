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

        [Required]
        [Comment("Start time of work")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Comment("End time of work")]
        public TimeSpan EndTime { get; set; }

        [Required]
        [Comment("Duration of each appointment")]
        public TimeSpan AppointmentDuration { get; set; } = TimeSpan.FromMinutes(30);

        [ForeignKey(nameof(BusinessServiceProviderId))]
        public  BusinessServiceProvider BusinessServiceProvider { get; set; } = null!;
    }
}