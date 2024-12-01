using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static AppointMeWeb.Infrastrucure.Constants.DataConstants;

namespace AppointMeWeb.Infrastrucure.Data.Models
{
    [Comment("User business rating")]
    public class Rating
    {
        [Key]
        [Comment("Rating Id")]
        public int Id { get; set; }

        [Required]
        [Comment("User evaluation for service")]
        public int Evaluation { get; set; }
        [Required]
        [Comment("User comment")]
        [MaxLength(AppointmentCommentMaxLength)]
        public string AppointmentComment {  get; set; }=string.Empty;

        [Required]
        public string ApplicationUserId { get; set; } = string.Empty;
        [Required]
        [Comment("Associated user for this rating")]
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        [Required]
        public int AppointmentId {  get; set; }
        [Required]
        [Comment("Associated appointment for this rating")]
        [ForeignKey(nameof(AppointmentId))]
        public Appointment Appointment { get; set; } = null!;
    }
}
