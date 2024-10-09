namespace AppointMeWeb.Core.Models.HomeModels
{
    public class BusinessHomeIndexView
    {
        public IEnumerable<BusinessAppointmentViewModel> Active { get; set; } = [];
        public IEnumerable<BusinessAppointmentViewModel> Canceled { get; set; } = [];
    }
}
