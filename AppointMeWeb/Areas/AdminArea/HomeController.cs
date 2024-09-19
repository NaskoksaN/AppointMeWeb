using Microsoft.AspNetCore.Mvc;

namespace AppointMeWeb.Areas.AdminArea
{
    public class HomeController : BaseController
    {
        public IActionResult AdminHomeIndex()
        {
            return View();
        }
    }
}
