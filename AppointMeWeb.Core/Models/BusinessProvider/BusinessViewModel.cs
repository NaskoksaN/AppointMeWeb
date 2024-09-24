using System.ComponentModel.DataAnnotations;

namespace AppointMeWeb.Core.Models.BusinessProvider
{
    public class BusinessViewModel
    {
        [Required]
        public int Id { get; set; }
       
        public string Name { get; set; } = string.Empty;
      
        public string BusinessType {  get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public string Phone { get; set; } = string.Empty;
      
        public string Email { get; set; } = string.Empty;
       
        public string Town { get; set; } = string.Empty;
    
        public string Address { get; set; } = string.Empty;
        public string WebsiteUrl { get; set; } = string.Empty; 

    }
}
