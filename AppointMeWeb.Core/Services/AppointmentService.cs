using Microsoft.EntityFrameworkCore;

using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Enums;
using AppointMeWeb.Core.Models.AppointmeModels;
using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Core.Models.HomeModels;
using AppointMeWeb.Core.Models.Schedule;
using AppointMeWeb.Infrastrucure.Data.Common;
using AppointMeWeb.Infrastrucure.Data.Enum;
using AppointMeWeb.Infrastrucure.Data.Models;

using static AppointMeWeb.Core.Constants.CoreConstants;
using static AppointMeWeb.Core.Constants.DateConstants;
using AppointMeWeb.Core.Models.ClientRecordsModels;


namespace AppointMeWeb.Core.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IBusinessService businessService;
        private readonly IRepository sqlService;
        private readonly IHelperService helperService;
        private readonly ICustomUserService userService;

        public AppointmentService(IBusinessService _businessService
            , IRepository _sqlService
            , IHelperService _helperService
            ,ICustomUserService _userService)
        {
            this.businessService = _businessService;
            this.sqlService = _sqlService;
            this.helperService = _helperService;
            this.WorkingHours = [];
            this.TooltipTexts = [];
            this.userService = _userService;
        }
        /// <summary>
        /// Cancels an appointment by updating its status to "Canceled" based on the provided appointment ID
        /// and user or business identifier. 
        /// </summary>
        /// <typeparam name="T">The type of the identifier, which can be either a string for User ID 
        /// or an int for Business Service Provider ID.</typeparam>
        /// <param name="id">The user or business identifier (string for User ID, int for Business ID).</param>
        /// <param name="appointmentId">The ID of the appointment to cancel.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains 
        /// true if the appointment was successfully canceled, or false if no matching appointment was found.</returns>
        /// <exception cref="InvalidOperationException">Thrown if an error occurs while attempting to 
        /// cancel the appointment.</exception>
        public async Task<bool> CancelAppointmentAsync<T>(T id, int appointmentId)
        {
            try
            {
                var query = sqlService
                                    .All<Appointment>()
                                    .Where(a => a.Id == appointmentId);
                                   
                if (id is string userId) 
                {
                    query = query.Where(t=> t.UserId== userId);
                }else if(id is int businessId)
                {
                    query = query.Where(t => t.BusinessServiceProviderId == businessId);
                }

                Appointment? tempAppointment = await query.FirstOrDefaultAsync();

                if (tempAppointment == null)
                {
                    throw new InvalidOperationException("No appointment found with the specified ID and user/business identifier.");
                }

                tempAppointment.Status= AppointmentStatus.Canceled;
                sqlService.Update<Appointment>(tempAppointment);
                await sqlService.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error canceling slot: " + ex.Message, ex);
            }
        }
        /// <summary>
        /// Gets or sets a dictionary containing the working hours for each date.
        /// The key is the date, and the value is a string representing the working hours for that date.
        /// </summary>
        public Dictionary<DateOnly, string> WorkingHours { get; set; }

        /// <summary>
        /// Gets or sets a dictionary containing tooltip texts for appointment slots on each date.
        /// The key is the date, and the value is a string with the tooltip text indicating available slots.
        /// </summary>
        public Dictionary<DateOnly, string> TooltipTexts { get;  set; }

        /// <summary>
        /// Asynchronously retrieves available appointment slots for the next thirty days for a specified business.
        /// </summary>
        /// <param name="businessId">The identifier of the business for which to fetch available appointment slots.</param>
        /// <returns>
        /// A dictionary where the key is the date and the value is a list of <see cref="AppointmentSlotViewModel"/> objects 
        /// representing the available appointment slots for that date.
        /// </returns>
        /// <exception cref="InvalidOperationException">Thrown when there is an error fetching available slots.</exception>
        public async Task<Dictionary<DateOnly, List<AppointmentSlotViewModel>>> GetAvaibleSlotsAsync(int businessId)
        {
            try
            {
                var business = await businessService.GetBusinessByIdAsync(businessId);

                var businessAppointments = await sqlService
                    .All<Appointment>()
                    .Where(a => a.BusinessServiceProviderId == business.Id)
                    .Select(a => new AppointmentSlotViewModel()
                    {
                        Date = a.Day,
                        StartTime = a.StartTime,
                        EndTime = a.EndTime,
                        IsBooked = a.IsBooked,
                    })
                    .ToListAsync();

                var nextThirtyDays = helperService.GetNextCountOfDays(CountOfDays);
                var businessWorkingSchedule = await businessService.GetUserWorkingShedulesAsync<int>(business.Id);
                var duration = business.AppointmentDuration;

                var allSlots = new Dictionary<DateOnly, List<AppointmentSlotViewModel>>();

                foreach (var day in nextThirtyDays)
                {
                    var dayOfWeek = day.DayOfWeek;
                    var schedule = businessWorkingSchedule.FirstOrDefault(bws => bws.Day == dayOfWeek);

                    WorkingHours[day] = GetWorkingHours(schedule);

                    var dailySlots = GetDailySlots(schedule, day, duration, businessAppointments);
                    allSlots[day] = dailySlots;

                    TooltipTexts[day] = CreateTooltipText(dailySlots);
                }

                return allSlots;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error fetching available slots: " + ex.Message, ex);
            }
        }
       
        public async Task<UserHomeIndexView> GetUserAppointmentsAsync(string userId)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            TimeSpan period = TimeSpan.FromDays(RatePeriod);
            DateOnly sevenDaysAgo = today.AddDays(-period.Days); // Adjusted for clarity

            IEnumerable<UserHomeIndexViewModel> models = await sqlService.AllReadOnly<Appointment>()
                .Where(a => a.UserId == userId && a.Day >= sevenDaysAgo)
                .Select(a => new UserHomeIndexViewModel()
                {
                    AppointmentId = a.Id,
                    BusinessProviderId = a.BusinessServiceProviderId,
                    BusinessProviderName = a.BusinessServiceProvider.Name,
                    AppointmentDate = a.Day,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    Status = a.Status,
                })
                .OrderBy(a => a.AppointmentDate)
                .ThenBy(a => a.StartTime)
                .ToListAsync();

            UserHomeIndexView userAppointments = new()
            {
                Active = models.Where(m => m.Status == AppointmentStatus.Confirmed
                            && m.AppointmentDate >= today)
                            .ToList(),
                Canceled = models.Where(m => m.Status == AppointmentStatus.Canceled
                            && m.AppointmentDate >= today)
                            .ToList(),
                ForRate = models.Where(m => m.Status == AppointmentStatus.Confirmed
                            && m.AppointmentDate < today 
                            && m.AppointmentDate >= sevenDaysAgo) 
                            .ToList(),
            };

            return userAppointments;
        }

        /// <summary>
        /// Asynchronously retrieves a collection of business appointments for a specified user based on the given search criteria.
        /// </summary>
        /// <param name="userId">The unique identifier of the user requesting the appointments.</param>
        /// <param name="criteria">The criteria to filter appointments, which can be Today, Tomorrow, or ThisWeek.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing an <see cref="IEnumerable{BusinessAppointmentViewModel}"/> 
        /// that holds the filtered appointments for the specified user.
        /// </returns>
        /// <exception cref="NullReferenceException">
        /// Thrown when no business service provider is found for the specified user ID.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the provided search criteria is invalid.
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown when an error occurs while retrieving appointments.
        /// </exception>
        public async Task<IEnumerable<BusinessAppointmentViewModel>> GetAppointmentsAsync(string userId, 
                    AppointmentSearchCriteria criteria,
                    DateRange? range = null)
        {
            try
            {
                BusinessServiceProvider business = await sqlService
                        .AllReadOnly<BusinessServiceProvider>()
                        .Where(b => b.ApplicationUserId == userId)
                        .FirstOrDefaultAsync() ?? throw new NullReferenceException("Business user not exist");

                TimeSpan currentTime = DateTime.Now.TimeOfDay;

                IQueryable<Appointment> resultAsIQueryable = sqlService
                            .AllReadOnly<Appointment>()
                            .Where(a => a.BusinessServiceProviderId == business.Id
                                    && a.Day> Today
                                    && a.Status == AppointmentStatus.Confirmed);

                switch (criteria)
                {
                    case AppointmentSearchCriteria.Today:
                        resultAsIQueryable = resultAsIQueryable
                            .Where(a => a.Day.Equals(Today)); 
                        break;

                    case AppointmentSearchCriteria.Tomorrow:
                        resultAsIQueryable = resultAsIQueryable
                            .Where(a => a.Day.Equals(Tomorrow)); 
                        break;

                    case AppointmentSearchCriteria.ThisWeek:
                        resultAsIQueryable = resultAsIQueryable
                            .Where(a => a.Day >= Tomorrow && a.Day <= NextWeekEnd);
                        break;

                    case AppointmentSearchCriteria.DateRange:
                        resultAsIQueryable = resultAsIQueryable
                            .Where(a => a.Day >= Tomorrow && a.Day <= NextThirtyDays);
                        break;

                    default:
                        throw new ArgumentException("Invalid search criteria");
                }

                IEnumerable<BusinessAppointmentViewModel> model = await resultAsIQueryable

                        .Select(a => new BusinessAppointmentViewModel()
                        {
                            AppointmentId = a.Id,
                            UserId = a.UserId,
                            UserNames = $"{a.ApplicationUser.FirstName} {a.ApplicationUser.LastName}",
                            UserPhone = a.ApplicationUser.Phone,
                            UserMessage = a.Message,
                            UserEmail = a.ApplicationUser.Email,
                            AppointmentDate = a.Day,
                            StartTime = a.StartTime,
                            EndTime = a.EndTime,
                            Status = a.Status
                        })
                        .OrderBy(a => a.StartTime)
                        .ToListAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving appointments.", ex);
            }

        }
        
        /// <summary>
        /// Retrieves client appointments based on the client's email and the associated business user ID.
        /// </summary>
        /// <param name="searchByEmail">The email of the client whose appointments are being searched.</param>
        /// <param name="businessUserId">The ID of the business user associated with the appointments.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an 
        /// enumerable collection of <see cref="ClientRecordViewModel"/> objects representing 
        /// the client's appointments within a specified date range.
        /// </returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving appointments.</exception>
        public async Task<IEnumerable<ClientRecordViewModel>> GetClientAppointmentsByEmailAndTermAsync(string searchByEmail, string businessUserId)
        {
            try
            {
                ApplicationUser user = await userService
                                .GetUserByEmailAsync(searchByEmail);
                BusinessServiceProvider business = await userService
                                .GetBusinessUserAsync<string>(businessUserId);

                int timePeriod = GetAppointmentTermByBusinessType(business.BusinessType.Value);
                DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
                DateOnly targetDate = currentDate.AddMonths(-timePeriod);

                IEnumerable<ClientRecordViewModel> model = await sqlService
                                .AllReadOnly<Appointment>()
                                .Where(a => a.UserId == user.Id
                                && a.BusinessServiceProviderId == business.Id 
                                && (a.Day>targetDate && a.Day<currentDate))
                                .Select(a => new ClientRecordViewModel()
                                {
                                    AppointmentId = a.Id,
                                    FullName = $"{a.ApplicationUser.FirstName} {a.ApplicationUser.LastName}",
                                    Email=a.ApplicationUser.Email,
                                    PhoneNumber = a.ApplicationUser.PhoneNumber,
                                    ReasonOfAppointment =a.Message,
                                    DateOfAppointment =a.Day,
                                })
                                .ToListAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving appointments.", ex);
            }
        }
        
        /// <summary>
        /// Gets the appointment term in months based on the specified business type.
        /// </summary>
        /// <param name="businessType">The type of business for which to retrieve the appointment term.</param>
        /// <returns>
        /// The appointment term in months. Returns:
        /// - 3 months for service types such as Doctor, Dentist, Therapist, etc.
        /// - 2 months for service types such as Barber, Hairdresser, Manicurist.
        /// - 6 months for service types such as Accountant, Lawyer, Personal Trainer.
        /// - 2 months for any other business types.
        /// </returns>
        private int GetAppointmentTermByBusinessType(BusinessType businessType)
        {
            switch (businessType)
            {
                case BusinessType.Doctor:
                case BusinessType.Dentist:
                case BusinessType.Therapist:
                case BusinessType.Cleaner:
                case BusinessType.MassageTherapist:
                case BusinessType.Photographer:
                case BusinessType.CarService:
                case BusinessType.Plumber:
                case BusinessType.Electrician:
                case BusinessType.Mechanic:
                    return 3; 

                case BusinessType.Barber:
                case BusinessType.Hairdresser:
                case BusinessType.Manicurist:
                    return 2; 

                case BusinessType.Accountant:
                case BusinessType.Lawyer:
                case BusinessType.PersonalTrainer:
                    return 6; 

                default:
                    return 2; 
            }
        }

        /// <summary>
        /// Retrieves the working hours from the provided daily schedule.
        /// </summary>
        /// <param name="schedule">The daily schedule containing start and end working times. Can be null.</param>
        /// <returns>
        /// A string indicating the working hours for the day. Returns:
        /// - A message with the working hours if the schedule is not null and it's not a day off.
        /// - A message indicating the day is off if the schedule is null or marked as a day off.
        /// </returns>
        private string GetWorkingHours(DailyScheduleViewModel? schedule)
        {
            return schedule != null && !schedule.IsDayOff
                ? $"Working time from {schedule.StartTime:hh\\:mm} - to {schedule.EndTime:hh\\:mm}{Environment.NewLine}"
                : $"Day OFF, no working hours{Environment.NewLine}";
        }

        /// <summary>
        /// Generates a list of available appointment slots for a given day based on the daily schedule.
        /// </summary>
        /// <param name="schedule">The daily schedule containing start and end working times. Can be null.</param>
        /// <param name="day">The date for which to generate the appointment slots.</param>
        /// <param name="duration">The duration of each appointment slot.</param>
        /// <param name="businessAppointments">A list of existing business appointments for checking availability.</param>
        /// <returns>
        /// A list of <see cref="AppointmentSlotViewModel"/> objects representing the available appointment slots for the specified day.
        /// If the schedule is null, the day is marked as off, or if start/end times are not set, returns an empty list.
        /// </returns>
        private List<AppointmentSlotViewModel> GetDailySlots(DailyScheduleViewModel? schedule, DateOnly day, TimeSpan duration, List<AppointmentSlotViewModel> businessAppointments)
        {
            if (schedule == null || schedule.IsDayOff || !schedule.StartTime.HasValue || !schedule.EndTime.HasValue)
            {
                return new List<AppointmentSlotViewModel>();
            }

            var slots = new List<AppointmentSlotViewModel>();
            for (TimeSpan i = schedule.StartTime.Value; i <= schedule.EndTime - duration; i += duration)
            {
                var slotStatus = businessAppointments
                    .FirstOrDefault(ba => ba.Date == day && ba.StartTime == i && ba.EndTime == i + duration);

                slots.Add(new AppointmentSlotViewModel
                {
                    Date = day,
                    StartTime = i,
                    EndTime = i + duration,
                    IsBooked = slotStatus?.IsBooked ?? false
                });
            }

            return slots;
        }

        /// <summary>
        /// Creates a tooltip text indicating available appointment slots based on the provided list.
        /// </summary>
        /// <param name="availableSlots">A list of appointment slots to check for availability.</param>
        /// <returns>
        /// A string indicating the available slots in the format "Available: slot: HH:mm - HH:mm".
        /// If no slots are available, returns "No available slots".
        /// </returns>
        private string CreateTooltipText(List<AppointmentSlotViewModel> availableSlots)
        {
            return availableSlots.Any(s => !s.IsBooked)
                ? "Available: " + string.Join(", ", availableSlots
                            .Where(s => !s.IsBooked)
                            .Select(slot => $"slot: {slot.StartTime:hh\\:mm} - {slot.EndTime:hh\\:mm}"))
                : "No available slots";
        }

        public Task<bool> CancelAppointmentByAppointmentIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
