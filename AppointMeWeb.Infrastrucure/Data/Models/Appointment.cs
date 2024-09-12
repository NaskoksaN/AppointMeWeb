using AppointMeWeb.Infrastrucure.Data.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointMeWeb.Infrastrucure.Data.Models
{
    [Comment("Appoinment")]
    public class Appointment
    {
        [Key]
        [Comment("Appoinment identifier")]
        public int Id {  get; set; }

       
        [StringLength(50)]
        [Comment("Message to BusinessServiceProvider")]
        public string? Message { get; set; }

        [Required]
        [Comment("Appoinment confirmation")]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Confirmed;

        [Required]
        [Comment("User Identifier")]
        public string UserId {  get; set; }=string.Empty;

        [Required]
        [Comment("BusinessServiceProvider Identifier")]
        public int BusinessServiceProviderId {  get; set; }

        [Required]
        [Comment("Day of the week")]
        public DayOfWeek Day { get; set; }

        [Required]
        [Comment("Start of appointment")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Comment("End of appointment")]
        public TimeSpan EndTime { get; set; }


        [ForeignKey(nameof(BusinessServiceProviderId))]
        public BusinessServiceProvider BusinessServiceProvider { get; set; } = null!;
    }
}
