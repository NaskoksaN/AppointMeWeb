using AppointMeWeb.Core.Enums;
using AppointMeWeb.Core.Models.AppointmeModels;
using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Core.Models.FindService;
using AppointMeWeb.Core.Models.HomeModels;
using AppointMeWeb.Infrastrucure.Data.Models;

namespace AppointMeWeb.Core.Contracts
{
    public interface IBusinessService
    {
        Task<bool> BookSlotAsync(BookSlotFormModel model, string currentUserId);
        public List<ServiceInfoModels> BusinessInfo();

        /// <summary>
        /// Asynchronously retrieves all business service provider records from the database,
        /// mapping them to a collection of <see cref="BusinessViewModel"/> objects.
        /// If no records are found or an error occurs during retrieval, a <see cref="DataAccessException"/> is thrown.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing an enumerable of <see cref="BusinessViewModel"/>.</returns>
        /// <exception cref="Exception">Thrown when no businesses are found or when a database access error occurs.</exception>
        Task<IEnumerable<BusinessViewModel>> GetAllBusinessAsync();
        Task<BusinessServiceProvider> GetBusinessByIdAsync(int businessId);

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
        Task<List<DailyScheduleViewModel>> GetUserWorkingShedulesAsync<T>(T userId);

        Task<BusinessQueryServiceModel> GetAllBusinessAsQueryAsync(
            string? category = null,
            string? searchingTown = null,
            string? BusinessName=null,
            string? SearchingInDescription=null,
            int currentPage = 1,
            int businessPerPage = 1,
            AlphabeticSort sorting= AlphabeticSort.Ascending);
        Task<int> GetBusinessIdFromUserIdAsync(string userId);
        Task<BusinessStatisticsViewModel> GetBusinessStatisticsAsync(string userId);
    }
}
