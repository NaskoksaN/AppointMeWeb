
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static AppointMeWeb.Infrastrucure.Constants.DataConstants;
using AppointMeWeb.Infrastrucure.Data.Enum;

namespace AppointMeWeb.Infrastrucure.Data.Models
{
    [Comment("Appoinment")]
    public class Appointment
    {
        [Key]
        [Comment("Appoinment identifier")]
        public int Id {  get; set; }

       
        [MaxLength(AppointmentMessageMaxLength)]
        [Comment("Message to BusinessServiceProvider")]
        public string? Message { get; set; }

        [Required]
        [Comment("Appoinment confirmation")]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Confirmed;

        [Required]
        [Comment("Day of the week")]
        public DayOfWeek Day { get; set; }

        [Required]
        [Comment("Start of appointment")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Comment("End of appointment")]
        public TimeSpan EndTime { get; set; }

        [Required]
        [Comment("BusinessServiceProvider Identifier")]
        public int BusinessServiceProviderId { get; set; }
        [Required]
        [Comment("User Identifier")]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; }=null!;

        [Required]
        [ForeignKey(nameof(BusinessServiceProviderId))]
        public BusinessServiceProvider BusinessServiceProvider { get; set; } = null!;
    }
}
