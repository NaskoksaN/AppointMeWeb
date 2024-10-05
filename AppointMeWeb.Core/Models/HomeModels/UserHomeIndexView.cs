using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointMeWeb.Core.Models.HomeModels
{
    public class UserHomeIndexView
    {
        public IEnumerable<UserHomeIndexViewModel> Active { get; set; } = [];
        public IEnumerable<UserHomeIndexViewModel> Canceled { get; set; } = [];

    }
}
