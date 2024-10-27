using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Extensions;
using Microsoft.AspNetCore.Mvc;


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
                return View();
            }
            catch (Exception ex)
            {
                logger.LogError("Error in register controller: {ControllerName}. Exception: {ExceptionMessage}", nameof(AppointmentController), ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetActiveAppointments()
        {
            string userId = User.Id();
            var appointments = await appointmentService.GetUserAppointmentsAsync(userId);
            return Json(appointments.Active);
        }

        [HttpGet]
        public async Task<IActionResult> GetCanceledAppointments()
        {
            string userId = User.Id();
            var appointments = await appointmentService.GetUserAppointmentsAsync(userId);
            return Json(appointments.Canceled);
        }

        [HttpGet]
        public async Task<IActionResult> GetForRatingAppointments()
        {
            string userId = User.Id();
            var appointments = await appointmentService.GetUserAppointmentsAsync(userId);
            return Json(appointments.ForRate);
        }
    }
}
