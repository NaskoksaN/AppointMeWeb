using System.ComponentModel.DataAnnotations;

namespace AppointMeWeb.Core.Models.ApplicationUser
{

    /// <summary>
    /// Fetch roles from Db
    /// </summary>
    public class RoleViewModel
    {
        [Required]
        public string Id { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
