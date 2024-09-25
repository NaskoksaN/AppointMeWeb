using AppointMeWeb.Infrastrucure.Data.Enum;
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
        /// Indicates whether the user is registering as a business provider.
        /// </summary>
        public bool IsBusinessProvider { get; set; }

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
        public DateOnly DOB { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        [StringLength(UserPhoneMaxLength,
            MinimumLength = UserPhoneMinLength,
            ErrorMessage = LengthMessage)]
        public string PhoneNumber { get; set; }=string.Empty;

        [Required]
        public string SelectedRole { get; set; } = WebUserRole;

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Business Type")]
        public BusinessType BusinessType { get; set; }

        //[Required(ErrorMessage = RequiredMessage)]
        [StringLength(BusinessServiceProviderNameMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = BusinessServiceProviderNameMinLength)]
        [Display(Name = "Business Name")]
        public string? Name { get; set; } 

        //[Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Business Description")]
        [StringLength(BusinessServiceProviderDescriptionMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = BusinessServiceProviderDescriptionMinLength)]
        public string? Description { get; set; } 

        //[Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Business Town")]
        [StringLength(BusinessServiceProviderTownMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = BusinessServiceProviderTownMinLength)]
        public string? Town { get; set; } 

        //[Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Business Address")]
        [StringLength(BusinessServiceProviderAddressMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = BusinessServiceProviderAddressMinLength)]
        public string? Address { get; set; } 

        //[Required(ErrorMessage = RequiredMessage)]
        [Url(ErrorMessage = UrlErrMsg)]
        [Display(Name = "Business URL")]
        public string? Url { get; set; }

        public IEnumerable<RoleViewModel> Roles { get; set; } = new List<RoleViewModel>();
    }
}
