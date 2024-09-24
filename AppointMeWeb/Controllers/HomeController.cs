using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Extensions;
using AppointMeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace AppointMeWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IBusinessService businessService;

        public HomeController(ILogger<HomeController> _logger
                , IBusinessService _businessService)
        {
            this.logger = _logger;
            this.businessService = _businessService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            if (User.Identity != null && User.Identity.IsAuthenticated) 
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

            try
            {
                IEnumerable<BusinessViewModel> model = await businessService.GetAllBusinessAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError("Error in register controller: {ExceptionMessage}", ex.Message);
                return View();
            }
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
