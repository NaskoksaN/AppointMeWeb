using Microsoft.AspNetCore.Mvc;

using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.HomeModels;
using AppointMeWeb.Extensions;


namespace AppointMeWeb.Areas.UserArea.Controllers
{
    public class HomeController : BaseController
    {

        private readonly ILogger<HomeController> logger;
        private readonly IBusinessService businessService;
        private readonly IAppointmentService appointmentService;

        public HomeController(ILogger<HomeController> _logger
                , IBusinessService _businessService
                , IAppointmentService _appointmentService)
        {
            this.logger = _logger;
            this.businessService = _businessService;
            this.appointmentService = _appointmentService;
        }

        [HttpGet]
        
        public async Task<IActionResult> UserHomeIndex()
        {
            try
            {
                string userId = User.Id();
                UserHomeIndexView userAppointments = await appointmentService
                                        .GetUserAppointmentsAsync(userId);
                return View(userAppointments);
            }
            catch (Exception ex)
            {
                logger.LogError("Error in register controller: {ControllerName}. Exception: {ExceptionMessage}", nameof(AppointmentController), ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
            
        }
    }
}
