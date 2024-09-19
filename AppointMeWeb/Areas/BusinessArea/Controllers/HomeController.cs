using Microsoft.AspNetCore.Mvc;

namespace AppointMeWeb.Areas.BusinessArea.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult BusinessHomeIndex()
        {
            return View();
        }
    }
}
