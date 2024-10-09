using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointMeWeb.Core.Models.ClientRecordsModels
{
    public class ClientRecordViewModel
    {
        public int AppointmentId {  get; set; }
        public string FullName {  get; set; }=string.Empty;
        public string Email {  get; set; }=string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ReasonOfAppointment {  get; set; } = string.Empty;
        public DateOnly DateOfAppointment { get; set; }
    }
}
