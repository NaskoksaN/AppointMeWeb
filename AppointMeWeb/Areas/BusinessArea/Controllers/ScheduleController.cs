﻿using AppointMeWeb.Areas.UserArea.Controllers;
using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Enums;
using AppointMeWeb.Core.Models.HomeModels;
using AppointMeWeb.Extensions;
using Microsoft.AspNetCore.Mvc;

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
                logger.LogError("Error in register controller: {ControllerName}. Exception: {ExceptionMessage}", nameof(AppointmentController), ex.Message);
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
                logger.LogError("Error in register controller: {ControllerName}. Exception: {ExceptionMessage}", nameof(AppointmentController), ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}