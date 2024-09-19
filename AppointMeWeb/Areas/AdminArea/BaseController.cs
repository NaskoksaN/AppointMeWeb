using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static AppointMeWeb.Infrastrucure.Constants.DataConstants;

namespace AppointMeWeb.Areas.AdminArea
{
    [Area("BusinessArea")]
    [Authorize(Roles = BusinessRole)]
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
