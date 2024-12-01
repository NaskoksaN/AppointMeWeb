using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static AppointMeWeb.Infrastrucure.Constants.DataConstants;

namespace AppointMeWeb.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = AdminRole)]
    public class BaseController : Controller
    {
        
    }
}
