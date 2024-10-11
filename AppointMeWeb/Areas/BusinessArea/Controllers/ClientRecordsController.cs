using AppointMeWeb.Areas.UserArea.Controllers;
using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.ClientRecordsModels;
using AppointMeWeb.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AppointMeWeb.Areas.BusinessArea.Controllers
{
    public class ClientRecordsController : BaseController 
    {
        private readonly ILogger<ClientRecordsController> logger;
        private readonly IAppointmentService appointmentService;

        public ClientRecordsController(ILogger<ClientRecordsController> _logger, 
            IAppointmentService _appointmentService)
        {
            this.logger = _logger;
            this.appointmentService = _appointmentService;
        }

        [HttpGet]
        public IActionResult SearchRecords()
        {
            ClientRecordFormModel model = new();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SearchRecords(ClientRecordFormModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", $"Invalid {nameof(ClientRecordFormModel)}");
                ClientRecordFormModel modelView = new();
                return View(modelView);
            }
            try
            {
                model.UserId = User.Id();
                IEnumerable<ClientRecordViewModel> clientModel = await appointmentService
                                    .GetClientAppointmentsByEmailAndTermAsync(model.SearchByEmail, model.UserId);
                ViewData["Title"] = model.SearchByEmail;
                return View("ShowClientRecord", clientModel);
            }
            catch(Exception ex) 
            {
                logger.LogError("Error in register controller: {ControllerName}. Exception: {ExceptionMessage}", nameof(AppointmentController), ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
            
        }
    }
    
}
