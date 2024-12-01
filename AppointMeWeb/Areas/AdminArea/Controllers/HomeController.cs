using Microsoft.AspNetCore.Mvc;

namespace AppointMeWeb.Areas.AdminArea.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult AdminArea()
        {
            return View();
        }
    }
}
