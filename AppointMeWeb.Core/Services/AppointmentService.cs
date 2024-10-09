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
        public Dictionary<DateOnly, string> WorkingHours { get; set; }
        public Dictionary<DateOnly, string> TooltipTexts { get;  set; }
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

            IEnumerable<UserHomeIndexViewModel> models = await sqlService.AllReadOnly<Appointment>()
                        .Where(a => a.UserId == userId && a.Day >= today)
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
                        .ToListAsync();

            UserHomeIndexView userAppointments = new()
            {
                Active = models.Where(m => m.Status == AppointmentStatus.Confirmed)
                            .ToList(),
                Canceled = models.Where(m => m.Status == AppointmentStatus.Canceled)
                            .ToList()
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
        private string GetWorkingHours(DailyScheduleViewModel? schedule)
        {
            return schedule != null && !schedule.IsDayOff
                ? $"Working time from {schedule.StartTime:hh\\:mm} - to {schedule.EndTime:hh\\:mm}{Environment.NewLine}"
                : $"Day OFF, no working hours{Environment.NewLine}";
        }
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
        private string CreateTooltipText(List<AppointmentSlotViewModel> availableSlots)
        {
            return availableSlots.Any(s => !s.IsBooked)
                ? "Available: " + string.Join(", ", availableSlots
                            .Where(s => !s.IsBooked)
                            .Select(slot => $"slot: {slot.StartTime:hh\\:mm} - {slot.EndTime:hh\\:mm}"))
                : "No available slots";
        }

        
    }
}
