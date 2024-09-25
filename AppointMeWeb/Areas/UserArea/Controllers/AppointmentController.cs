
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


            AppointmentViewModel model = new()
            {
                NextThirtyDays = helperService
                            .GetNextCountOfDays(CountOfDays),
                BusinessWorkingSchedule = await businessService
                                            .GetUserWorkingShedulesAsync<int>(businessId),
                AvailableSlots = await appointmentService.GetAvaibleSlotsAsync(businessId)
            };

            return View(model);
        }
    }
}
