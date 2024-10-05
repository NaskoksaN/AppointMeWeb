using AppointMeWeb.Infrastrucure.Data.Enum;

namespace AppointMeWeb.Core.Models.HomeModels
{
    public class ServiceInfoModels
    {
        public BusinessType BusinessType { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}