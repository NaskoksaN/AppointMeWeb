using Microsoft.AspNetCore.Mvc;

using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.AppointmeModels;
using static AppointMeWeb.WebConstants.AppointmentConstants;
using AppointMeWeb.Extensions;

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

        [HttpGet]
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
        [HttpPost]
        public async Task<IActionResult> MakeAppointment([FromBody] BookSlotFormModel model)
        {
            
            if (!ModelState.IsValid)
            {
                logger.LogError("Model state is invalid: {Errors}",
                    string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                

                return PartialView("_AvailableSlots", model);
            }

            try
            {
                string currentUserId = User.Id();
                bool result = await businessService.BookSlotAsync(model, currentUserId);
                if (result)
                {
                    logger.LogInformation("Successfully booked appointment for BusinessId: {BusinessId} on Date: {Date}", model.BusinessId, model.Date);
                    return Ok();
                }
                return BadRequest("slot already booked");
            }
            catch (Exception ex)
            {
                
                logger.LogError(ex, "Error occurred while making appointment for BusinessId: {BusinessId}, Date: {Date}", model.BusinessId, model.Date);
                return StatusCode(500, "Internal server error. Please try again later."); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetSlotsAsync([FromBody] AvailableSlotsFormModel model)
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
