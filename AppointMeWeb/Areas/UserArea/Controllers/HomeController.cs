using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AppointMeWeb.Areas.UserArea.Controllers
{
    public class HomeController : BaseController
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
        
        public IActionResult UserHomeIndex()
        {
            return View();
        }
    }
}
