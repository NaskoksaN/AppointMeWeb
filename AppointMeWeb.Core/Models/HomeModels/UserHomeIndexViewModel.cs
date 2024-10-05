using AppointMeWeb.Infrastrucure.Data.Enum;

namespace AppointMeWeb.Core.Models.HomeModels
{
    public class UserHomeIndexViewModel
    {
        public int AppointmentId {  get; set; }
        public int BusinessProviderId {  get; set; }
        public string BusinessProviderName { get; set; }=string.Empty;
        public DateOnly AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public AppointmentStatus Status {  get; set; }
    }
}
