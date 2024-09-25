using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.AppointmeModels;
using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Infrastrucure.Data.Common;
using AppointMeWeb.Infrastrucure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;


namespace AppointMeWeb.Core.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IBusinessService businessService;
        private readonly IRepository sqlService;
        private readonly IHelperService helperService;

        
        public AppointmentService(IBusinessService _businessService
            , IRepository _sqlService
            , IHelperService _helperService)
        {
            this.businessService = _businessService;
            this.sqlService = _sqlService;
            this.helperService = _helperService;
            this.WorkingHours = [];
            this.TooltipTexts = [];
        }
        public Dictionary<DateOnly, string> WorkingHours { get; set; }
        public Dictionary<DateOnly, string> TooltipTexts { get;  set; }

        public async Task<Dictionary<DateOnly, List<AppointmentSlotViewModel>>> GetAvaibleSlotsAsync(int businessId)
        {
            try
            {
                BusinessServiceProvider business = await businessService
                                                .GetBusinessByIdAsync(businessId);
                
                List<AppointmentSlotViewModel> businessAppointments = await sqlService
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

                List<DateOnly> NextThirtyDays = helperService.GetNextCountOfDays(30);
                List<DailyScheduleViewModel> businessWorkingSchedule = await businessService
                                        .GetUserWorkingShedulesAsync<int>(business.Id);
                TimeSpan duration = business.AppointmentDuration;

                Dictionary <DateOnly, List<AppointmentSlotViewModel>> allSlots = [];

                foreach (var day in NextThirtyDays) 
                {
                    DayOfWeek dayOfWeek = day.DayOfWeek;
                    DailyScheduleViewModel? schedule = businessWorkingSchedule
                                  .Where(bws => bws.Day==dayOfWeek)
                                  .FirstOrDefault();
                    var workingTime = schedule != null && !schedule.IsDayOff
                                    ? $"Working time from {schedule.StartTime:hh\\:mm} - to {schedule.EndTime:hh\\:mm}{Environment.NewLine}"
                                    : $"Day OFF, no working hours{Environment.NewLine}";

                    WorkingHours[day] = workingTime;

                    List<AppointmentSlotViewModel> daylySlot = [];
                    
                    if (schedule != null &&
                        !schedule.IsDayOff &&
                        schedule.StartTime.HasValue &&
                        schedule.EndTime.HasValue)
                    {
                        for (TimeSpan i = schedule.StartTime.Value; i <= schedule.EndTime-duration; i += duration)
                        {
                            AppointmentSlotViewModel tempSlotModel = new()
                            {
                                Date = day,
                                StartTime = i,
                                EndTime = i+duration,
                                IsBooked = false,
                            };
                            daylySlot.Add(tempSlotModel);
                        }
                    }
                    else
                    {
                        daylySlot = new List<AppointmentSlotViewModel>();
                    }
                    allSlots[day] = daylySlot;

                    var availableSlots = allSlots[day];
                    var tooltipText = availableSlots.Count > 0
                        ? "Available" + string.Join(", ", availableSlots.Select(slot => $"slot: {slot.StartTime:hh\\:mm} - {slot.EndTime:hh\\:mm}"))
                        : "No available slots";

                    TooltipTexts[day] = tooltipText;

                }

                return allSlots;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error fetching available slots: " + ex.Message, ex);
            }
        }

    }
}
