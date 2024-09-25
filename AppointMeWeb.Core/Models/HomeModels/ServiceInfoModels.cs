using AppointMeWeb.Infrastrucure.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointMeWeb.Core.Models.HomeModels
{
    public class ServiceInfoModels
    {
        public BusinessType BusinessType { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}
