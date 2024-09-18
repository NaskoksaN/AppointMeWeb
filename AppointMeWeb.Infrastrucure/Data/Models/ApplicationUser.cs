using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static AppointMeWeb.Infrastrucure.Constants.DataConstants;

namespace AppointMeWeb.Infrastrucure.Data.Models
{
    [Comment("Application user data")]
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(ApplicationUserFirstNameMaxLength)]
        [Comment("Application first name")]
        public string FirstName {  get; set; }=string.Empty;

        [Required]
        [MaxLength(ApplicationUserLastNameMaxLength)]
        [Comment("Application last name")]
        public string LastName { get; set; }= string.Empty;

        [Required]
        [MaxLength(UserPhoneMaxLength)] 
        [Comment("Application phone")]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [Comment("User date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Comment("Associated business service provider")]
        public int? BusinessServiceProviderId { get; set; }

        [ForeignKey(nameof(BusinessServiceProviderId))]
        public BusinessServiceProvider? BusinessServiceProvider { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = [];

    }
}
