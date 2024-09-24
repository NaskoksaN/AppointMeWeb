using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Infrastrucure.Data.Common;
using AppointMeWeb.Infrastrucure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointMeWeb.Core.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IFactory factory;
        private readonly IRepository sqlService;
        public BusinessService(IFactory _factory
                , IRepository _sqlService)
        {

            this.factory = _factory;
            this.sqlService = _sqlService;
        }

        /// <summary>
        /// Asynchronously retrieves all business service provider records from the database,
        /// mapping them to a collection of <see cref="BusinessViewModel"/> objects.
        /// If no records are found or an error occurs during retrieval, a <see cref="DataAccessException"/> is thrown.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing an enumerable of <see cref="BusinessViewModel"/>.</returns>
        /// <exception cref="Exception">Thrown when no businesses are found or when a database access error occurs.</exception>
        public async Task<IEnumerable<BusinessViewModel>?> GetAllBusinessAsync()
        {
            try
            {
                IEnumerable<BusinessViewModel> result = await sqlService
                    .AllReadOnly<BusinessServiceProvider>()
                    .Select(b => new BusinessViewModel()
                    {
                        Id = b.Id,
                        Name = b.Name,
                        BusinessType = b.BusinessType.ToString(),
                        Description = b.BusinessDescription,
                        Phone = b.ApplicationUser.Phone,
                        Email = b.ApplicationUser.Email,
                        Town = b.Town,
                        Address = b.Address,
                        WebsiteUrl = b.Url,
                    })
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while accessing the database.", ex);
            }
        }

        public async Task<List<DailyScheduleViewModel>>? GetUserWorkingShedulesAsync(string userId)
        {

            var schedule = await sqlService
                .AllReadOnly<BusinessServiceProvider>()
                .Where(b => b.ApplicationUserId == userId)
                .Select(b => new BusinessProviderFormModel()
                {
                    ExistedSchedule = b.WorkingSchedules
                            .Select(w => new DailyScheduleViewModel()
                            {
                                Day = w.Day,
                                IsDayOff = w.IsDayOff,
                                StartTime = w.StartTime,
                                EndTime = w.EndTime,
                            }).ToList(),
                })
                .FirstOrDefaultAsync();
            return schedule?.ExistedSchedule;

        }
    }
}
