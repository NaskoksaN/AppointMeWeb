using AppointMeWeb.Extensions;
using AppointMeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace AppointMeWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            if (User.Identity.IsAuthenticated) 
            {
                if (User.IsBusinessProvider()) 
                {
                    return RedirectToAction("BusinessHomeIndex", "Home", new { area = "BusinessArea" });
                }
                else if (User.IsAdmin()) 
                {
                    return RedirectToAction("AdminHomeIndex", "Home", new { area = "AdminArea" });
                }
                else if (User.IsUser()) 
                {
                    return RedirectToAction("UserHomeIndex", "Home", new { area = "UserArea" });
                }
            }

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
