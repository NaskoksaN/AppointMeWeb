using AppointMeWeb.Core.Models.BusinessProvider;

namespace AppointMeWeb.Core.Models.AppointmeModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public List<DateTime> NextThirtyDays { get; set; } = [];
        public List<DailyScheduleViewModel> BusinessWorkingSchedule { get; set; } = [];
        public List<AppointmentSlotViewModel> AvailableSlots { get; set; } = [];
    }

}

