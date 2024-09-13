
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using AppointMeWeb.Infrastrucure.Data.Enum;
using static AppointMeWeb.Infrastrucure.Constants.DataConstants;

namespace AppointMeWeb.Infrastrucure.Data.Models
{
    [Comment("Details of service providers")]
    public  class BusinessServiceProvider
    {

        [Key]
        [Comment("Buinsess identifier")]
        public int Id { get; set; }

        [Required]
        [Comment("Type of buinsess section")]
        public BusinessType BusinessType { get; set; }

        [Required]
        [MaxLength(BusinessServiceProviderNameMaxLength)]
        [Comment("Buinsess name")]
        public string Name { get; set; }=string.Empty;

        [Required]
        [MaxLength(BusinessServiceProviderDescriptionMaxLength)]
        [Comment("Buinsess description")]
        public string Description {  get; set; }=string.Empty;

        [Required]
        [MaxLength(BusinessServiceProviderEmailMaxLength)]
        [Comment("Buinsess E-mail")]
        public string Email {  get; set; }=string.Empty;

        [Required]
        [MaxLength(UserPhoneMaxLength)]
        [Comment("Buinsess phone")]
        public string Phone {  get; set; }=string.Empty;

        [Required]
        [MaxLength(BusinessServiceProviderTownMaxLength)]
        [Comment("Buinsess town")]
        public string Town { get; set; } = string.Empty;

        [Required]
        [MaxLength(BusinessServiceProviderAddressMaxLength)]
        [Comment("Buinsess address")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Comment("Buinsess web-link")]
        [MaxLength(BusinessServiceProviderUrlMaxLength)]
        public string Url { get; set; } = string.Empty;

        public ICollection<Appointment> Appointments { get; set; } = [];
        public ICollection<WorkingSchedule> WorkingSchedules { get; set; } = [];
    }
}

