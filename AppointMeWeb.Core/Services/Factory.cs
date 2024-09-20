using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.ApplicationUser;
using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Infrastrucure.Data.Common;
using AppointMeWeb.Infrastrucure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppointMeWeb.Core.Services
{
    public class Factory : IFactory
    {
        private readonly IRepository sqlRepository;

        public Factory(IRepository _sqlRepository
            )
        {
            this.sqlRepository = _sqlRepository;
        }

        /// <summary>
        /// Creates a new instance of <see cref="ApplicationUser"/> based on the provided registration form model.
        /// </summary>
        /// <param name="model">The registration form model containing user details.</param>
        /// <returns>A new <see cref="ApplicationUser"/> instance populated with the specified details.</returns>
        public ApplicationUser CreateApplicationUser( RegisterFormModel model)
        {
            return new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DOB,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                UserName = model.Email,
            };
        }

        /// <summary>
        /// Creates a new business user based on the provided registration details and associates it with the specified user ID.
        /// </summary>
        /// <param name="model">The registration form model containing the business details.</param>
        /// <param name="userId">The ID of the user associated with the business.</param>
        /// <returns>The ID of the newly created <see cref="BusinessServiceProvider"/>.</returns>
        /// <exception cref="ApplicationException">Thrown when the database fails to save the information or an unexpected error occurs.</exception>
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
            catch (DbUpdateException dbEx)
            {
                throw new ApplicationException("Database failed to save info", dbEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred.", ex);
            }

        }

        /// <summary>
        /// Creates a work schedule for a business user based on the provided daily schedules.
        /// </summary>
        /// <param name="dailySchedules">A list of daily schedules containing details such as day, start time, end time, and whether it's a day off.</param>
        /// <param name="businessUserId">The identifier of the business user for whom the schedule is created.</param>
        /// <returns>A task that represents the asynchronous operation, with a value indicating whether the schedule was successfully created.</returns>
        /// <exception cref="ApplicationException">Thrown when there is an error saving the schedule to the database.</exception>
        public async Task<bool> CreateWorkSchedule(List<DailyScheduleViewModel> dailySchedules, int businessUserId)
        {
            try
            {
                List<WorkingSchedule> schedule = new();
                foreach (var day in dailySchedules)
                {
                    WorkingSchedule workingSchedule = new WorkingSchedule()
                    {
                        Day = day.Day,
                        StartTime = day.StartTime,
                        EndTime = day.EndTime,
                        IsDayOff = day.IsDayOff,
                        BusinessServiceProviderId = businessUserId
                    };
                    schedule.Add(workingSchedule);
                }
                await sqlRepository.AddRangeAsync<WorkingSchedule>(schedule);
                await sqlRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database failed to save info", ex);
            }

        }
    }
}
