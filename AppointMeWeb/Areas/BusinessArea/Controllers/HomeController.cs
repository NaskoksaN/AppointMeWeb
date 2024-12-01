using AppointMeWeb.Areas.UserArea.Controllers;
using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Core.Models.HomeModels;
using AppointMeWeb.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AppointMeWeb.Areas.BusinessArea.Controllers
{
    public class HomeController : BaseController
    {
        
        private readonly IBusinessService businessService;
        private readonly ILogger<HomeController> logger;

        public HomeController(IBusinessService _businessService,
            ILogger<HomeController> _logger)
        {
           
            this.businessService = _businessService;
            this.logger = _logger;
        }

        public async Task<IActionResult> BusinessHomeIndex()
        {
            string userId = User.Id();

            BusinessStatisticsViewModel model = await businessService.GetBusinessStatisticsAsync(userId);

            return View(model);
        }
    }
}
