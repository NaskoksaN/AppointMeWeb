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
            BusinessServiceProvider businessProvider = new()
            {
                BusinessType = model.BusinessType,
                Name = model.Name = null!,
                BusinessDescription = model.Description = null!,
                Town = model.Town = null!,
                Address = model.Address = null!,
                Url = model.Url = null!,
                ApplicationUserId = userId,
            };
            try
            {
                await sqlRepository.AddAsync<BusinessServiceProvider>(businessProvider);
                await sqlRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(nameof(CreateBusinessUserAndReturnId), ex);
                throw new ApplicationException("Database failed to save info", ex);
            }

            return businessProvider.Id;
        }

        
    }
}
