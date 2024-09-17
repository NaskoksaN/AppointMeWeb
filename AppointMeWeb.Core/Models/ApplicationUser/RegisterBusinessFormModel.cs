using AppointMeWeb.Infrastrucure.Data.Enum;
using System.ComponentModel.DataAnnotations;
using static AppointMeWeb.Infrastrucure.Constants.DataConstants;
using static AppointMeWeb.Core.Constants.MessageConstants;

namespace AppointMeWeb.Core.Models.ApplicationUser
{
    public class RegisterBusinessFormModel:RegisterFormModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Business Type")]
        public BusinessType BusinessType { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(BusinessServiceProviderNameMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = BusinessServiceProviderNameMinLength)]
        [Display(Name = "Business Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Business Description")]
        [StringLength(BusinessServiceProviderDescriptionMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = BusinessServiceProviderDescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [EmailAddress(ErrorMessage = EmailErrMsg)]
        [Display(Name = "Business Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Business Phone")]
        [StringLength(UserPhoneMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = UserPhoneMinLength)]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Business Town")]
        [StringLength(BusinessServiceProviderTownMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = BusinessServiceProviderTownMinLength)]
        public string Town { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Business Address")]
        [StringLength(BusinessServiceProviderAddressMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = BusinessServiceProviderAddressMinLength)]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [Url(ErrorMessage = UrlErrMsg)]
        [Display(Name = "Business URL")]
        public string Url { get; set; } = string.Empty;
    }
}
