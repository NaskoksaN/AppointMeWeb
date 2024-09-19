using Microsoft.AspNetCore.Mvc;

namespace AppointMeWeb.Areas.UserArea.Controller
{
    public class HomeController : BaseController
    {
        public IActionResult UserHomeIndex()
        {
            return View();
        }
    }
}
