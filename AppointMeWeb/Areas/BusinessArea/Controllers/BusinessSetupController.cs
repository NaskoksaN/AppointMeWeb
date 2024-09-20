using Microsoft.AspNetCore.Mvc;

using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Extensions;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace AppointMeWeb.Areas.BusinessArea.Controllers
{
    public class BusinessSetupController : BaseController
    {
        private readonly IBusinessService businessService;
        private readonly ICustomUserService customUserService;
        private readonly IFactory factory;
        private readonly IHelperService helperService;
        private readonly ILogger logger;

        public BusinessSetupController(IBusinessService _businessService
                , ICustomUserService _customUserService
                , IFactory _factory
                , IHelperService _helperService
                , ILogger _logger)
        {
            this.businessService = _businessService;
            this.customUserService = _customUserService;
            this.factory = _factory;
            this.helperService = _helperService;
            this.logger = _logger;
        }

        [HttpGet]
        public IActionResult Setup()
        {
            BusinessProviderFormModel model = new()
            {
                Days = helperService.GetDaysOfWeek(),
                
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Setup([FromForm] BusinessProviderFormModel model)
        {
            try
            {
                string userId = User.Id();
                int businessUserId = await customUserService
                            .UpdateBusinessUserDurationDetails(model.AppointmentDuration, userId);
                bool isWorkScheduleUpdate = await factory
                            .CreateWorkSchedule(model.DailySchedules, businessUserId);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while setting up business provider details.");
                BusinessProviderFormModel modelView = new()
                {
                    Days = helperService.GetDaysOfWeek()
                };
                return View(modelView);
            }

            
        }
    }
}
