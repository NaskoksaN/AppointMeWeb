using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointMeWeb.Infrastrucure.Data.Models
{
    [Comment("User business rating")]
    public class Rating
    {
        [Key]
        [Comment("Rating Id")]
        public int Id { get; set; }

        [Required]
        [Comment("User evaluation for service")]
        public int Evaluation { get; set; }
        [Required]
        [Comment("User comment")]
        public string Comment {  get; set; }=string.Empty;

        [Required]
        public string ApplicationUserId { get; set; } = string.Empty;
        [Required]
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        [Required]
        public int BusinessId {  get; set; }
        [Required]
        [ForeignKey(nameof(BusinessId))]
        public BusinessServiceProvider BusinessServiceProvider { get; set; } = null!;
    }
}
