using Microsoft.AspNetCore.Mvc;

using AppointMeWeb.Areas.UserArea.Controllers;
using AppointMeWeb.Core.Constants;
using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Enums;
using AppointMeWeb.Core.Models.HomeModels;
using AppointMeWeb.Core.Models.Schedule;
using AppointMeWeb.Extensions;


namespace AppointMeWeb.Areas.BusinessArea.Controllers
{
    public class ScheduleController : BaseController
    {
        private readonly IBusinessService businessService;
        private readonly IAppointmentService appointmentService;
        private readonly IHelperService helperService;
        private readonly ILogger<BusinessSetupController> logger;

        public ScheduleController(IBusinessService _businessService
                , IAppointmentService _appointmentService
                , IHelperService _helperService
                , ILogger<BusinessSetupController> _logger)
        {
            this.businessService = _businessService;
            this.appointmentService = _appointmentService;
            this.helperService = _helperService;
            this.logger = _logger;
        }
        [HttpGet]
        public async Task<IActionResult> Today()
        {
            try
            {
                string userId = User.Id();
                IEnumerable<BusinessAppointmentViewModel> model = await appointmentService
                                    .GetAppointmentsAsync(userId, AppointmentSearchCriteria.Today);
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError("Error in register controller: {ScheduleController}. Exception: {ExceptionMessage}", nameof(AppointmentController), ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> Tomorrow()
        {
            try
            {
                string userId = User.Id();
                IEnumerable<BusinessAppointmentViewModel> model = await appointmentService
                                    .GetAppointmentsAsync(userId, AppointmentSearchCriteria.Tomorrow);

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError("Error in register controller: {ScheduleController}. Exception: {ExceptionMessage}", nameof(AppointmentController), ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
        [HttpGet]
        public async Task<IActionResult> NextSevenDays()
        {
            try
            {
                string userId = User.Id();
                IEnumerable<BusinessAppointmentViewModel> model = await appointmentService
                                    .GetAppointmentsAsync(userId, AppointmentSearchCriteria.ThisWeek);

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError("Error in register controller: {ScheduleController}. Exception: {ExceptionMessage}", nameof(AppointmentController), ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
        [HttpGet]
        public IActionResult DateRange()
        {
            try
            {
                DateRange model = new()
                {
                    StartDate = DateConstants.Tomorrow,
                    EndDate = DateConstants.NextThirtyDays,
                };

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError("Error in register controller: {ScheduleController}. Exception: {ExceptionMessage}", nameof(AppointmentController), ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> DateRange(DateRange model)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("Error in register controller: {ScheduleController}. Error : {DateRange}");
                model.StartDate = DateConstants.Tomorrow;
                model.EndDate = DateConstants.NextThirtyDays;
                return View(model);
            }
            try
            {
                string userId = User.Id();
                IEnumerable<BusinessAppointmentViewModel> modelDate = await appointmentService
                                    .GetAppointmentsAsync(userId, AppointmentSearchCriteria.DateRange, model);
                ViewBag.StartDate = model.StartDate;
                ViewBag.EndDate = model.EndDate;
                return View("ShowRangePeriod", modelDate);
            }
            catch (Exception ex)
            {
                logger.LogError("Error in register controller: {ScheduleController}. Exception: {Action DateRange}", nameof(AppointmentController), ex.Message);
                model.StartDate = DateConstants.Tomorrow;
                model.EndDate= DateConstants.NextThirtyDays;
                return View(model);
            }
        }
    }
}
