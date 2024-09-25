using AppointMeWeb.Core.Models.AppointmeModels;

namespace AppointMeWeb.Core.Contracts
{
    public interface IAppointmentService
    {
        Dictionary<DateOnly, string> WorkingHours { get; }
        Dictionary<DateOnly, string> TooltipTexts { get; }

        Task<Dictionary<DateOnly, List<AppointmentSlotViewModel>>> GetAvaibleSlotsAsync(int businessId);
       
    }
}
