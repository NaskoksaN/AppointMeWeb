using Microsoft.AspNetCore.Mvc;

namespace AppointMeWeb.Areas.UserArea.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult UserHomeIndex()
        {
            return View();
        }
    }
}
