using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.ApplicationUser;
using AppointMeWeb.Infrastrucure.Data.Common;
using AppointMeWeb.Infrastrucure.Data.Models;
using Microsoft.Extensions.Logging;

namespace AppointMeWeb.Core.Services
{
    public class Factory : IFactory
    {
        private readonly IRepository sqlRepository;
        private readonly ILogger logger;

        public Factory(IRepository _sqlRepository
            , ILogger<IFactory> _logger)
        {
            this.sqlRepository = _sqlRepository;
            this.logger = _logger;
        }

        public async Task<int> CreateBusinessUserAndReturnId(RegisterFormModel model, string userId)
        {
            
            try
            {
                BusinessServiceProvider businessProvider = new()
                {
                    BusinessType = model.BusinessType,
                    Name = model.Name,
                    BusinessDescription = model.Description ,
                    Town = model.Town ,
                    Address = model.Address ,
                    Url = model.Url,
                    ApplicationUserId = userId,
                };
                await sqlRepository.AddAsync<BusinessServiceProvider>(businessProvider);
                await sqlRepository.SaveChangesAsync();
                return businessProvider.Id;
            }
            catch (Exception ex)
            {
                logger.LogError(nameof(CreateBusinessUserAndReturnId), ex);
                throw new ApplicationException("Database failed to save info", ex);
            }

        }

        
    }
}
