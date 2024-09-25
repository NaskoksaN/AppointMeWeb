namespace AppointMeWeb.Core.Models.AppointmeModels
{
    public class AppointmentSlotViewModel
    {
        public DateOnly Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsBooked { get; set; }
    }
}
