using AppointMeWeb.Core.Enums;
using AppointMeWeb.Core.Models.AppointmeModels;
using AppointMeWeb.Core.Models.ClientRecordsModels;
using AppointMeWeb.Core.Models.HomeModels;
using AppointMeWeb.Core.Models.Schedule;

namespace AppointMeWeb.Core.Contracts
{
    public interface IAppointmentService
    {
        Dictionary<DateOnly, string> WorkingHours { get; }
        Dictionary<DateOnly, string> TooltipTexts { get; }

        Task<IEnumerable<BusinessAppointmentViewModel>> GetAppointmentsAsync(string userId, 
                    AppointmentSearchCriteria criteria,
                    DateRange? range = null);
        Task<Dictionary<DateOnly, List<AppointmentSlotViewModel>>> GetAvaibleSlotsAsync(int businessId);
        Task<IEnumerable<ClientRecordViewModel>> GetClientAppointmentsByEmailAndTermAsync(string searchByEmail, string businessUserId);
        Task<UserHomeIndexView> GetUserAppointmentsAsync(string userId);
    }
}
