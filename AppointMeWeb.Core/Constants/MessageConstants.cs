using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointMeWeb.Core.Constants
{
    public static class MessageConstants
    {
        public const string RequiredMessage = "The {0} is required.";
        public const string LengthMessage = "The {0} must be between {2} and {1} characters long.";
        public const string EmailErrMsg = "Invalid email address.";
        public const string UrlErrMsg = "Invalid URL.";
    }
}
