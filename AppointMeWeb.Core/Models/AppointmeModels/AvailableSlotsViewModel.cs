namespace AppointMeWeb.Core.Models.AppointmeModels
{
    public class AvailableSlotsViewModel
    {
        public int BusinessId { get; set; }
        public DateOnly Date { get; set; }
        public List<AppointmentSlotViewModel> Slots { get; set; } = [];
    }
}
