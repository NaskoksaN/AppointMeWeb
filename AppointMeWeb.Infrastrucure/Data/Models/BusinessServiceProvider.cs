
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using AppointMeWeb.Infrastrucure.Data.Enum;
using static AppointMeWeb.Infrastrucure.Constants.DataConstants;
using System.ComponentModel.DataAnnotations.Schema;

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
        public BusinessType? BusinessType { get; set; }

        [Required]
        [MaxLength(BusinessServiceProviderNameMaxLength)]
        [Comment("Buinsess name")]
        public string Name { get; set; }=string.Empty;

        [Required]
        [MaxLength(BusinessServiceProviderDescriptionMaxLength)]
        [Comment("Buinsess description")]
        public string BusinessDescription {  get; set; }=string.Empty;

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

        [Required]
        public string ApplicationUserId {  get; set; } = string.Empty;
        [Required]
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public ICollection<Appointment> Appointments { get; set; } = [];
        public ICollection<WorkingSchedule> WorkingSchedules { get; set; } = [];
    }
}

