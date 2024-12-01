using System.ComponentModel.DataAnnotations;

using static AppointMeWeb.Infrastrucure.Constants.DataConstants;
using static AppointMeWeb.Core.Constants.MessageConstants;

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
