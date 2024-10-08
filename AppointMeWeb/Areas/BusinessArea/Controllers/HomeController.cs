using AppointMeWeb.Areas.UserArea.Controllers;
using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.HomeModels;
using AppointMeWeb.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AppointMeWeb.Areas.BusinessArea.Controllers
{
    public class HomeController : BaseController
    {
        
        private readonly IAppointmentService appointmentService;
        private readonly ILogger<HomeController> logger;

        public HomeController(IAppointmentService _appointmentService,
            ILogger<HomeController> _logger)
        {
           
            this.appointmentService = _appointmentService;
            this.logger = _logger;
        }

        public async Task<IActionResult> BusinessHomeIndex()
        {
            return View();

        }
    }
}
