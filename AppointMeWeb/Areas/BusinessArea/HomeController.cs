using Microsoft.AspNetCore.Mvc;

namespace AppointMeWeb.Areas.BusinessArea
{
    public class HomeController : BaseController
    {
        public IActionResult BusinessHomeIndex()
        {
            return View();
        }
    }
}
