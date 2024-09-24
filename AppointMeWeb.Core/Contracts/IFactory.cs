using AppointMeWeb.Core.Models.ApplicationUser;
using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Infrastrucure.Data.Models;

namespace AppointMeWeb.Core.Contracts
{
    public interface IFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="ApplicationUser"/> based on the provided registration form model.
        /// </summary>
        /// <param name="model">The registration form model containing user details.</param>
        /// <returns>A new <see cref="ApplicationUser"/> instance populated with the specified details.</returns>
        ApplicationUser CreateApplicationUser(RegisterFormModel registerFormModel);

        /// <summary>
        /// Creates a new business user based on the provided registration details and associates it with the specified user ID.
        /// </summary>
        /// <param name="model">The registration form model containing the business details.</param>
        /// <param name="userId">The ID of the user associated with the business.</param>
        /// <returns>The ID of the newly created <see cref="BusinessServiceProvider"/>.</returns>
        /// <exception cref="ApplicationException">Thrown when the database fails to save the information or an unexpected error occurs.</exception>
        public Task<int> CreateBusinessUserAndReturnId(RegisterFormModel model, string userId);

        /// <summary>
        /// Creates a work schedule for a business user based on the provided daily schedules.
        /// </summary>
        /// <param name="dailySchedules">A list of daily schedules containing details such as day, start time, end time, and whether it's a day off.</param>
        /// <param name="businessUserId">The identifier of the business user for whom the schedule is created.</param>
        /// <returns>A task that represents the asynchronous operation, with a value indicating whether the schedule was successfully created.</returns>
        /// <exception cref="ApplicationException">Thrown when there is an error saving the schedule to the database.</exception>
        Task<bool> CreateWorkScheduleAsync(List<DailyScheduleViewModel> dailySchedules, int businessUserId, TimeSpan duration);
        Task<bool> UpdateWorkScheduleAsync(List<DailyScheduleViewModel> existedSchedule, int businessUserId, TimeSpan duration);
    }
}
