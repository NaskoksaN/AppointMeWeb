using Microsoft.AspNetCore.Mvc;

namespace AppointMeWeb.Areas.AdminArea.Controller
{
    public class HomeController : BaseController
    {
        public IActionResult AdminHomeIndex()
        {
            return View();
        }
    }
}
