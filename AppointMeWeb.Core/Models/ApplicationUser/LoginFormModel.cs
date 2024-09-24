using System.ComponentModel.DataAnnotations;
using System.Net;

namespace AppointMeWeb.Core.Models.ApplicationUser
{
    public class LoginFormModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        public string? ReturnUrl { get; set; }

        
    }
}
