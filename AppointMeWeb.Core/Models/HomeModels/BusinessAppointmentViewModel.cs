using AppointMeWeb.Infrastrucure.Data.Enum;

namespace AppointMeWeb.Core.Models.HomeModels
{
    public class BusinessAppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public string UserId { get; set; } =string.Empty;
        public string UserNames { get; set; }=string.Empty;
        public string UserPhone { get; set; } = string.Empty;
        public string? UserMessage { get; set; } 
        public string UserEmail { get; set; } = string.Empty;
        public DateOnly AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
