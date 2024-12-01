using AppointMeWeb.Core.Enums;
using AppointMeWeb.Core.Models.BusinessProvider;
using System.ComponentModel.DataAnnotations;
using static AppointMeWeb.Core.Constants.PageConst;

namespace AppointMeWeb.Core.Models.FindService
{
    public class AllBusinessQueryModel
    {
        public int SetupBusinessPerPage { get; set; } = BusinessPerPage;
        public string TypeOfBusiness { get; set; } = string.Empty;

        [Display(Name = "Search by town")]
        public string? SearchingTown { get; set; }

        [Display(Name = "Search by name")]
        public string? BusinessName { get; set; }

        [Display(Name = "Search in description")]
        public string? SearchingInDescription { get; set; }
        public int CurrentPage { get; set; } = StartPage;
        public int CountOfBusiness { get; set; }
        [Display(Name = "Sort by name")]
        public AlphabeticSort Sorting { get; set; }

        public IEnumerable<string> BusinessTypes { get; set; } = [];
        public IEnumerable<BusinessViewModel> Businesses { get; set; } = [];
        
        
    }
}
