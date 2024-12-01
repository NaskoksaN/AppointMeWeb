
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
        public async Task<IActionResult> FindService([FromQuery] AllBusinessQueryModel query)
        {
           
            try
            {
                var querryResult = await businessService.GetAllBusinessAsQueryAsync(
                    query.TypeOfBusiness,
                    query.SearchingTown,
                    query.BusinessName,
                    query.SearchingInDescription,
                    query.CurrentPage,
                    query.SetupBusinessPerPage
                    );

                query.BusinessTypes = helperService.GetEnumValuesAsString<BusinessType>();
                query.Businesses = querryResult.Businesses;
                query.CountOfBusiness = querryResult.CountOfBusiness;

                return View(query);
            }
            catch (Exception ex)
            {
                logger.LogError("Error in register controller: {FindController}. Exception: {ExceptionMessage}", nameof(AppointmentController), ex.Message);
                return View();
            }
        }
        
    }
}
