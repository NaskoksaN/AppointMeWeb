using Microsoft.AspNetCore.Mvc;

namespace AppointMeWeb.Areas.BusinessArea.Controller
{
    public class HomeController : BaseController
    {
        public IActionResult BusinessHomeIndex()
        {
            return View();
        }
    }
}
