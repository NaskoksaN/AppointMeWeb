
using Microsoft.AspNetCore.Mvc;

using AppointMeWeb.Core.Contracts;
using static AppointMeWeb.WebConstants.AppointmentConstants;
using AppointMeWeb.Core.Models.AppointmeModels;

namespace AppointMeWeb.Areas.UserArea.Controllers
{
    public class AppointmentController : BaseController
    {
        private readonly IHelperService helperService;
        private readonly ILogger<FindController> logger;
        private readonly IBusinessService businessService;
        private readonly ICustomUserService customUserService;

        public AppointmentController(IHelperService _helperService
                , ILogger<FindController> _logger
                , IBusinessService _businessService
                , ICustomUserService _customUserService)
        {
            this.helperService = _helperService;
            this.logger = _logger;
            this.businessService = _businessService;
            this.customUserService = _customUserService;
        }

        
        [HttpGet]
        public async Task<IActionResult> MakeAppointment(int businessId)
        {


            AppointmentViewModel model = new()
            {
                NextThirtyDays = helperService.GetNextCountOfDays(CountOfDays),
                BusinessWorkingSchedule = await businessService.GetUserWorkingShedulesAsync<int>(businessId)
            };

            return View(model);
        }
    }
}
