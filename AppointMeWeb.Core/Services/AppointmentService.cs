using Microsoft.EntityFrameworkCore;

using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.AppointmeModels;
using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Infrastrucure.Data.Common;
using AppointMeWeb.Infrastrucure.Data.Models;
using static AppointMeWeb.Core.Constants.CoreConstants;
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

        //public async Task<Dictionary<DateOnly, List<AppointmentSlotViewModel>>> GetAvaibleSlotsAsync(int businessId)
        //{
        //    try
        //    {
        //        BusinessServiceProvider business = await businessService
        //                                        .GetBusinessByIdAsync(businessId);

        //        List<AppointmentSlotViewModel> businessAppointments = await sqlService
        //                .All<Appointment>()
        //                .Where(a => a.BusinessServiceProviderId == business.Id)
        //                .Select(a => new AppointmentSlotViewModel()
        //                {
        //                    Date = a.Day,
        //                    StartTime = a.StartTime,
        //                    EndTime = a.EndTime,
        //                    IsBooked = a.IsBooked,
        //                })
        //                .ToListAsync();

        //        List<DateOnly> NextThirtyDays = helperService.GetNextCountOfDays(CountOfDays);
        //        List<DailyScheduleViewModel> businessWorkingSchedule = await businessService
        //                                .GetUserWorkingShedulesAsync<int>(business.Id);
        //        TimeSpan duration = business.AppointmentDuration;

        //        Dictionary <DateOnly, List<AppointmentSlotViewModel>> allSlots = [];

        //        foreach (var day in NextThirtyDays) 
        //        {
        //            DayOfWeek dayOfWeek = day.DayOfWeek;
        //            DailyScheduleViewModel? schedule = businessWorkingSchedule
        //                          .Where(bws => bws.Day==dayOfWeek)
        //                          .FirstOrDefault();
        //            var workingTime = schedule != null && !schedule.IsDayOff
        //                            ? $"Working time from {schedule.StartTime:hh\\:mm} - to {schedule.EndTime:hh\\:mm}{Environment.NewLine}"
        //                            : $"Day OFF, no working hours{Environment.NewLine}";

        //            WorkingHours[day] = workingTime;

        //            List<AppointmentSlotViewModel> daylySlot = [];

        //            if (schedule != null &&
        //                !schedule.IsDayOff &&
        //                schedule.StartTime.HasValue &&
        //                schedule.EndTime.HasValue)
        //            {
        //                for (TimeSpan i = schedule.StartTime.Value; i <= schedule.EndTime-duration; i += duration)
        //                {
        //                    var slotStatus = businessAppointments
        //                            .Where(ba=> ba.Date.Equals(day)
        //                                    && ba.StartTime== i
        //                                    && ba.EndTime==i+duration)
        //                            .FirstOrDefault();
        //                    AppointmentSlotViewModel tempSlotModel = new()
        //                    {
        //                        Date = day,
        //                        StartTime = i,
        //                        EndTime = i+duration,
        //                        IsBooked = slotStatus != null ? slotStatus.IsBooked : false,
        //                    };
        //                    daylySlot.Add(tempSlotModel);
        //                }
        //            }
        //            else
        //            {
        //                daylySlot = new List<AppointmentSlotViewModel>();
        //            }
        //            allSlots[day] = daylySlot;

        //            var availableSlots = allSlots[day];
        //            var tooltipText = availableSlots.Count > 0 
        //                ? "Available" + string.Join(", ", availableSlots
        //                                .Where(s=> s.IsBooked==false)
        //                                .Select(slot => $"slot: {slot.StartTime:hh\\:mm} - {slot.EndTime:hh\\:mm}"))
        //                : "No available slots";

        //            TooltipTexts[day] = tooltipText;

        //        }

        //        return allSlots;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new InvalidOperationException("Error fetching available slots: " + ex.Message, ex);
        //    }
        //}

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
