using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static AppointMeWeb.Infrastrucure.Constants.DataConstants;

namespace AppointMeWeb.Areas.UserArea.Controller
{

    [Area("UserArea")]
    [Authorize(Roles = WebUserRole)]
    public class BaseController : Controller
    {

    }
}
