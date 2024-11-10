using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.ApplicationUser;
using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Infrastrucure.Data.Common;
using AppointMeWeb.Infrastrucure.Data.Models;
using Microsoft.EntityFrameworkCore;

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

        public Task<bool> AddRatingAsync(string userId, int appointmentId)
        {
            throw new NotImplementedException();
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
        /// <exception cref="ApplicationException">Thrown when the business user is not found or when there is an error saving to the database.</exception>
        public async Task<bool> CreateWorkScheduleAsync(List<DailyScheduleViewModel> dailySchedules
                    , int businessUserId
                    , TimeSpan duration)
        {
            try
            {
                var businessUser = await sqlRepository
                        .AllReadOnly<BusinessServiceProvider>()
                        .FirstOrDefaultAsync(b=> b.Id==businessUserId);
                if(businessUser == null)
                {
                    throw new ApplicationException("Business user not found.");
                }
                List<WorkingSchedule> schedule = [];
                foreach (var day in dailySchedules)
                {
                    WorkingSchedule workingSchedule = CreateScheduleObject(day, businessUser.Id);
                   
                    schedule.Add(workingSchedule);
                }

                businessUser.AppointmentDuration = duration;
                await sqlRepository.AddRangeAsync<WorkingSchedule>(schedule);
                await sqlRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database failed to save info", ex);
            }

        }
        /// <summary>
        /// Updates the working schedule for a specified business service provider.
        /// </summary>
        /// <param name="existedSchedule">A list of existing daily schedules to be updated.</param>
        /// <param name="businessUserId">The unique identifier of the business service provider.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a boolean value indicating whether the update was successful.</returns>
        /// <exception cref="ApplicationException">Thrown when the business user is not found or when there is an error saving to the database.</exception>
        public async Task<bool> UpdateWorkScheduleAsync(List<DailyScheduleViewModel> existedSchedule
                , int businessUserId
                , TimeSpan duration)
        {
            try
            {
                var businessUser = await sqlRepository
                        .All<BusinessServiceProvider>()
                        .Include(b => b.WorkingSchedules) 
                        .FirstOrDefaultAsync(b => b.Id == businessUserId);
                if (businessUser == null)
                {
                    throw new ApplicationException("Business user not found.");
                }
                businessUser.WorkingSchedules.Clear();

                foreach (var day in existedSchedule)
                {
                    WorkingSchedule workingSchedule = CreateScheduleObject(day, businessUser.Id);

                    businessUser.WorkingSchedules.Add(workingSchedule);
                }
                businessUser.AppointmentDuration = duration;

                await sqlRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database failed to save info", ex);
            }

        }

        private WorkingSchedule CreateScheduleObject(DailyScheduleViewModel day, int businessUserId)
        {
            WorkingSchedule result = new WorkingSchedule()
            {
                Day = day.Day,
                StartTime = day.StartTime,
                EndTime = day.EndTime,
                IsDayOff = day.IsDayOff,
                BusinessServiceProviderId = businessUserId
            };

            return result;
        }
    }
}
