using AppointMeWeb.Core.Models.BusinessProvider;

namespace AppointMeWeb.Core.Models.AppointmeModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }

        public List<DateOnly> NextThirtyDays { get; set; } = [];
        public List<DailyScheduleViewModel> BusinessWorkingSchedule { get; set; } = [];
        public string AvaibleSlots {  get; set; } =string.Empty;
        public Dictionary<DateOnly, string> WorkingHours { get; set; } = [];
        public Dictionary<DateOnly, string> TooltipTexts { get; set; } = [];

        public Dictionary<DateOnly, List<AppointmentSlotViewModel>> AvailableSlots { get; set; } = [];
    }

}

