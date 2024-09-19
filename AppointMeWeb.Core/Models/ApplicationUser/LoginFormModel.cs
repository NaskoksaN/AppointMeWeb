using System.ComponentModel.DataAnnotations;
using System.Net;

namespace AppointMeWeb.Core.Models.ApplicationUser
{
    public class LoginFormModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public string? ReturnUrl { get; set; }

        
    }
}
