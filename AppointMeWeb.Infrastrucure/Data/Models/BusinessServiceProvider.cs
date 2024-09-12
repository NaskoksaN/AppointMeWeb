using AppointMeWeb.Infrastrucure.Data.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AppointMeWeb.Infrastrucure.Data.Models
{
    [Comment("Details of service providers")]
    public  class BusinessServiceProvider
    {

        [Key]
        [Comment("Buinsess identifier")]
        public int Id { get; set; }

        [Required]
        public BusinessType BusinessType { get; set; }

        [Required]
        [MaxLength(40)]
        [Comment("Buinsess name")]
        public string Name { get; set; }=string.Empty;

        [Required]
        [MaxLength(100)]
        [Comment("Buinsess description")]
        public string Description {  get; set; }=string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        [Comment("Buinsess E-mail")]
        public string Email {  get; set; }=string.Empty;

        [Required]
        [MaxLength(15)]
        [Comment("Buinsess phone")]
        public string Phone {  get; set; }=string.Empty;

        [Required]
        [MaxLength(30)]
        [Comment("Buinsess town")]
        public string Town { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Comment("Buinsess address")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Url]
        [Comment("Buinsess web-link")]
        public string Url { get; set; } = string.Empty;

        public virtual ICollection<Appointment> Appointments { get; set; } = [];
        public virtual ICollection<WorkingSchedule> WorkingSchedules { get; set; } = [];
    }
}

