using System.ComponentModel.DataAnnotations;

using static AppointMeWeb.Core.Constants.MessageConstants;
using static AppointMeWeb.Infrastrucure.Constants.DataConstants;

namespace AppointMeWeb.Core.Models.ApplicationUser
{
    public class RegisterFormModel
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        // Remaining API warnings ommited.
        public string? ReturnUrl { get; set; }


        /// <summary>
        ///User first name
        /// </summary>
        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "User first name")]
        [StringLength(ApplicationUserFirstNameMaxLength,
            MinimumLength = ApplicationUserFirstNameMinLength,
            ErrorMessage = LengthMessage)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "User last name")]
        [StringLength(ApplicationUserLastNameMaxLength,
            MinimumLength = ApplicationUserLastNameMinLength,
            ErrorMessage = LengthMessage)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        [StringLength(UserPhoneMaxLength,
            MinimumLength = UserPhoneMinLength,
            ErrorMessage = LengthMessage)]
        public string PhoneNumber { get; set; }=string.Empty;

        public string SelectedRole { get; set; } = string.Empty;

        public string? EMail { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; } = new List<RoleViewModel>();
    }
}
