﻿using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AppointMeWeb.Areas.BusinessArea.Controllers
{
    public class BusinessSetupController : BaseController
    {
        private readonly IBusinessService businessService;
        private readonly ICustomUserService customUserService;
        private readonly IFactory factory;
        private readonly IHelperService helperService;
        private readonly ILogger<BusinessSetupController> logger;

        public BusinessSetupController(IBusinessService _businessService
                , ICustomUserService _customUserService
                , IFactory _factory
                , IHelperService _helperService
                , ILogger<BusinessSetupController> _logger)
        {
            this.businessService = _businessService;
            this.customUserService = _customUserService;
            this.factory = _factory;
            this.helperService = _helperService;
            this.logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> Setup()
        {
            string userId = User.Id();
            BusinessProviderFormModel model = new()
            {
                Days = helperService.GetDaysOfWeek(),
                ExistedSchedule = await businessService.GetUserWorkingShedulesAsync(userId)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Setup([FromForm] BusinessProviderFormModel model)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("Error in register controller: {BusinessSetupController}. Exception: {fail to validate BusinessProviderFormModel model}");
                
                string userId = User.Id();
                model.Days = helperService.GetDaysOfWeek();
                model.ExistedSchedule = await businessService.GetUserWorkingShedulesAsync(userId);
                return View(model);
            }
            try
            {
                string userId = User.Id();
                var businessUser = await customUserService
                            .GetBusinessUserAsync<string>(userId);
                bool isWorkScheduleUpdate = model.ExistedSchedule != null && model.ExistedSchedule.Count ==0
                        ? await factory
                            .CreateWorkScheduleAsync(model.DailySchedules, businessUser.Id, model.AppointmentDuration)
                        : await factory.UpdateWorkScheduleAsync(model.ExistedSchedule, businessUser.Id, model.AppointmentDuration);
                // todo show result - create/update sucseeful SignalAr hub or...
                return RedirectToAction("BusinessHomeIndex", "Home");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, message: ex.Message);

                string userId = User.Id();
                BusinessProviderFormModel modelView = new()
                {
                    Days = helperService.GetDaysOfWeek(),
                    ExistedSchedule = await businessService.GetUserWorkingShedulesAsync(userId),
                };
                return View(modelView);
            }

        }
    }
}
