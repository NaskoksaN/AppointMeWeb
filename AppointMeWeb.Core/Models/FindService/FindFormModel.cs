
using System.ComponentModel.DataAnnotations;

using AppointMeWeb.Infrastrucure.Data.Enum;
using static AppointMeWeb.Infrastrucure.Constants.DataConstants;
using static AppointMeWeb.Core.Constants.MessageConstants;
using AppointMeWeb.Core.Models.BusinessProvider;

namespace AppointMeWeb.Core.Models.FindService
{
    public class FindFormModel
    {
        [Required]
        public string TypeOfBusiness{ get; set; }=string.Empty;

        [Display(Name = "Search in town")]
        [StringLength(BusinessServiceProviderTownMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = BusinessServiceProviderTownMinLength)]
        public string? SearchingTown { get; set; }

        [StringLength(BusinessServiceProviderNameMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = BusinessServiceProviderNameMinLength)]
        [Display(Name = "Business Name")]
        public string? BusinessName { get; set; }

        [Display(Name = "Business Description")]
        [StringLength(BusinessServiceProviderDescriptionMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = BusinessServiceProviderDescriptionMinLength)]
        public string? SearchingInDescription { get; set; }

        public IEnumerable<BusinessType> BusinessTypes { get; set; } = [];
        public IEnumerable<BusinessViewModel> Businesses { get; set; } = [];
    }
}
