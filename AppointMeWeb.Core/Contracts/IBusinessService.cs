using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Infrastrucure.Data.Models;

namespace AppointMeWeb.Core.Contracts
{
    public interface IBusinessService
    {
        /// <summary>
        /// Asynchronously retrieves all business service provider records from the database,
        /// mapping them to a collection of <see cref="BusinessViewModel"/> objects.
        /// If no records are found or an error occurs during retrieval, a <see cref="DataAccessException"/> is thrown.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing an enumerable of <see cref="BusinessViewModel"/>.</returns>
        /// <exception cref="Exception">Thrown when no businesses are found or when a database access error occurs.</exception>
        Task<IEnumerable<BusinessViewModel>> GetAllBusinessAsync();
        Task<List<DailyScheduleViewModel>> GetUserWorkingShedulesAsync(string userId);
    }
}
