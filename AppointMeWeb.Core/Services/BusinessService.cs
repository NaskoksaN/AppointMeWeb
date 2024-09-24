using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Infrastrucure.Data.Common;
using AppointMeWeb.Infrastrucure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointMeWeb.Core.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IRepository sqlService;
        public BusinessService( IRepository _sqlService)
        {

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

        /// <summary>
        /// Retrieves the working schedule of a business service provider based on the provided user ID.
        /// Supports both integer (provider ID) and string (application user ID) types.
        /// </summary>
        /// <typeparam name="T">The type of the user ID (int or string).</typeparam>
        /// <param name="userId">The user ID of the business service provider.</param>
        /// <returns>
        /// A list of <see cref="DailyScheduleViewModel"/> representing the working schedule of the provider,
        /// or null if no matching provider is found.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the user ID type is unsupported.</exception>
        public async Task<List<DailyScheduleViewModel>>? GetUserWorkingShedulesAsync<T>(T userId) 
        {

            var query = sqlService.AllReadOnly<BusinessServiceProvider>();

            query = userId switch
            {
                int id => query.Where(b => b.Id == id),
                string appUserId => query.Where(b => b.ApplicationUserId == appUserId),
                _ => throw new ArgumentException("Unsupported user ID type.")
            };

            var schedule = await query
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
