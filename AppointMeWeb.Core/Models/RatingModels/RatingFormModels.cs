using System.ComponentModel.DataAnnotations;

using AppointMeWeb.Core.Enums;

using static AppointMeWeb.Infrastrucure.Constants.DataConstants;
using static AppointMeWeb.Core.Constants.MessageConstants;

namespace AppointMeWeb.Core.Models.RatingModels
{
    public class RatingFormModels
    {
        [Required]
        public int AppointmentId {  get; set; }
       
        [Required]
        public int Rating {  get; set; }

        [Required]
        [StringLength(AppointmentCommentMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = AppointmentCommentMinLength)]
        public string Comment {  get; set; }=string.Empty;
    }
}
