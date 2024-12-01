using AppointMeWeb.Core.Models.BusinessProvider;

namespace AppointMeWeb.Core.Models.FindService
{
    public class BusinessQueryServiceModel
    {
        public int CountOfBusiness { get; set; }
        public IEnumerable<BusinessViewModel> Businesses { get; set; } = [];
    }
}
