
using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Core.Models.FindService;
using AppointMeWeb.Infrastrucure.Data.Enum;
using Microsoft.AspNetCore.Mvc;
using static AppointMeWeb.WebConstants.AppointmentConstants;

namespace AppointMeWeb.Areas.UserArea.Controllers
{
    public class FindController : BaseController
    {
        private readonly IHelperService helperService;
        private readonly ILogger<FindController> logger;
        private readonly IBusinessService businessService;
        private readonly ICustomUserService customUserService;

        public FindController(IHelperService _helperService
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
        public async Task<IActionResult> FindService()
        {
           
            try
            {
                IEnumerable<BusinessType> businessType = helperService
                                   .GetEnumValues<BusinessType>();
                IEnumerable<BusinessViewModel> businessCollection = await businessService
                                                                    .GetAllBusinessAsync();
                FindFormModel model = new ()
                {
                    BusinessTypes = businessType,
                    Businesses = businessCollection
                };
                
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError("Error in register controller: {ControllerName}. Exception: {ExceptionMessage}", nameof(AppointmentController), ex.Message);
                return View();
            }
        }
        
    }
}
