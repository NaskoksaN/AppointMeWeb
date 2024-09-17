using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointMeWeb.Core.Models.ApplicationUser
{
    public class MyProfileViewModel
    {
        public LoginFormModel? LoginFormModel { get; set; }
        public RegisterFormModel? RegisterFormModel { get; set; } = null!;
    }
}
