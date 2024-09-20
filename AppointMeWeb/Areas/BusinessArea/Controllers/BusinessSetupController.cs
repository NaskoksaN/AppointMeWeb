using Microsoft.AspNetCore.Mvc;

using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Extensions;
using System.Security.Claims;

namespace AppointMeWeb.Areas.BusinessArea.Controllers
{
    public class BusinessSetupController : BaseController
    {
        private readonly IBusinessService businessService;
        private readonly ICustomUserService customUserService;
        private readonly IFactory factory;

        public BusinessSetupController(IBusinessService _businessService
                , ICustomUserService _customUserService
                , IFactory _factory)
        {
            this.businessService = _businessService;
            this.customUserService = _customUserService;
            this.factory = _factory;
        }

        [HttpGet]
        public IActionResult Setup()
        {
            BusinessProviderFormModel model = new()
            {
                Days = Enum.GetValues(typeof(DayOfWeek))
                   .Cast<DayOfWeek>()
                   .Skip(1)
                   .Concat(new[] { DayOfWeek.Sunday })
                   .ToList()
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

                BusinessProviderFormModel modelView = new()
                {
                    Days = Enum.GetValues(typeof(DayOfWeek))
                  .Cast<DayOfWeek>()
                  .Skip(1)
                  .Concat(new[] { DayOfWeek.Sunday })
                  .ToList()
                };
                return View(modelView);
            }

            
        }
    }
}
