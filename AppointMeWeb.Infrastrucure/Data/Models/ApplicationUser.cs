using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointMeWeb.Infrastrucure.Data.Models
{
    [Comment("Application user data")]
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(30)]
        [Comment("Application first name")]
        public string FirstName {  get; set; }=string.Empty;

        [Required]
        [MaxLength(30)]
        [Comment("Application last name")]
        public string LastName { get; set; }= string.Empty;

        [Required]
        [MaxLength(30)]
        [Comment("Application phone")]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [Comment("User date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Comment("User appointments")]
        public virtual ICollection<Appointment> Appointments { get; set; } = [];

        [ForeignKey(nameof(BusinessServiceProvider))]
        [Comment("Associated business service provider")]
        public int? BusinessServiceProviderId { get; set; }

        [ForeignKey(nameof(BusinessServiceProviderId))]
        public BusinessServiceProvider? BusinessServiceProvider { get; set; }


        [NotMapped] 
        public int Age => DateTime.Now.Year - DateOfBirth.Year - (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);
    }
}
