using System.ComponentModel.DataAnnotations;

using static AppointMeWeb.Core.Constants.MessageConstants;
using static AppointMeWeb.Core.Constants.CoreConstants;

namespace AppointMeWeb.Core.Models.ClientRecordsModels
{
    public class ClientRecordFormModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        //[EmailAddress(ErrorMessage = EmailErrMsg)]
        [RegularExpression(RegexEmailValidation, ErrorMessage = EmailErrMsg)]
        public string SearchByEmail{ get; set; } =string.Empty;
        
        public string UserId {  get; set; }=string.Empty;
    }
}
