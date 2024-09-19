using Microsoft.AspNetCore.Mvc;

namespace AppointMeWeb.Areas.UserArea
{
    public class HomeController : BaseController
    {
        public IActionResult UserHomeIndex()
        {
            return View();
        }
    }
}
