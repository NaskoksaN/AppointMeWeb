using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointMeWeb.Core.Constants
{
    public static class CoreConstants
    {
        public const int CountOfDays = 30;
        public const string RegexEmailValidation = @"^[^@\s]+@[^@\s]+\.[a-zA-Z]{2,}$";
        public const int MinAgeUser = 16;
    }
}
