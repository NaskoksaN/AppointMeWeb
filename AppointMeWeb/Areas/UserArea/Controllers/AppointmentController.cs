
using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.AppointmeModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static AppointMeWeb.WebConstants.AppointmentConstants;

namespace AppointMeWeb.Areas.UserArea.Controllers
{

    
    
    

    public class AppointmentController : BaseController
    {
        private readonly IHelperService helperService;
        private readonly ILogger<FindController> logger;
        private readonly IBusinessService businessService;
        private readonly ICustomUserService customUserService;
        private readonly IAppointmentService appointmentService;

        public AppointmentController(IHelperService _helperService
                , ILogger<FindController> _logger
                , IBusinessService _businessService
                , ICustomUserService _customUserService
                , IAppointmentService _appointmentService)
        {
            this.helperService = _helperService;
            this.logger = _logger;
            this.businessService = _businessService;
            this.customUserService = _customUserService;
            this.appointmentService = _appointmentService;
        }

        
       
        [HttpGet("UserArea/Appointment/MakeAppointment/{businessId}")]
        public async Task<IActionResult> MakeAppointment(int businessId)
        {
            try
            {
                AppointmentViewModel model = new()
                {
                    NextThirtyDays = helperService
                            .GetNextCountOfDays(CountOfDays),
                    BusinessWorkingSchedule = await businessService
                                            .GetUserWorkingShedulesAsync<int>(businessId),
                    AvailableSlots = await appointmentService.GetAvaibleSlotsAsync(businessId),
                    WorkingHours = appointmentService.WorkingHours,
                    TooltipTexts = appointmentService.TooltipTexts,
                    BusinessId = businessId
                };
                
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError("Error in register controller: {ControllerName}. Exception: {ExceptionMessage}", nameof(AppointmentController), ex.Message);
                return View();
            }
        }
        [HttpPost("UserArea/Appointment/GetSlots")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetSlotsAsync([FromBody] AvailableSlotsViewModel model)
        {
            var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            logger.LogInformation("Request Payload: {RequestBody}", requestBody);

            logger.LogInformation("Received BusinessId: {BusinessId}, Date: {Date}, Slots: {Slots}",
                model.BusinessId, model.Date, model.Slots);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                logger.LogError("Model State Errors: {Errors}", string.Join(", ", errors));
                return BadRequest(ModelState);
            }


            try
            {
               
                return PartialView("_AvailableSlots", model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while getting slots.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

    }
}
