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
        private readonly IFactory factoryService;

        public HomeController(ILogger<HomeController> _logger
                , IBusinessService _businessService
                , IAppointmentService _appointmentService
                , IFactory _factoryService)
        {
            this.logger = _logger;
            this.businessService = _businessService;
            this.appointmentService = _appointmentService;
            this.factoryService = _factoryService;
        }

        [HttpGet]

        public IActionResult UserHomeIndex()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                logger.LogError("Error in register controller: {HomeController}. Exception: {ExceptionMessage}", nameof(AppointmentController), ex.Message);
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

        [HttpPost]
        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {
            string userId = User.Id();
            try
            {    
                bool isCanceled = await appointmentService.CancelAppointmentAsync<string>(userId, appointmentId);

                if (isCanceled)
                {
                    return Json(new { success = true, message = "Appointment canceled successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Unable to cancel appointment." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error in CancelApp action: {ControllerName}. Exception: {ExceptionMessage}", nameof(HomeController), ex.Message);
                return StatusCode(500, "An error occurred while canceling the appointment.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRating(int appointmentId)
        {
            string userId = User.Id();
            try
            {
                bool iscreatedComment = await factoryService.AddRatingAsync(userId, appointmentId);
                if (iscreatedComment)
                {
                    return Json(new { success = true, message = "Rating add successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Unable to add rating" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error in SubmitRating  action: {ControllerName}. Exception: {ExceptionMessage}", nameof(HomeController), ex.Message);
                return StatusCode(500, "An error occurred while rating the appointment.");
            }

        }
    }
}
