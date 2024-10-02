using System.ComponentModel.DataAnnotations;



namespace AppointMeWeb.Core.Models.AppointmeModels
{
    public class AvailableSlotsFormModel
    {
        [Required]
        public int BusinessId { get; set; }
        public string? VisitMessage { get; set; }
        public DateOnly Date { get; set; }
        public List<AppointmentSlotViewModel> Slots { get; set; } = [];
    }
}
