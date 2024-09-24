﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using AppointMeWeb.Core.Utilities;
using AppointMeWeb.Infrastrucure.Data.Models;

namespace AppointMeWeb.Core.Models.BusinessProvider
{
    public class BusinessProviderFormModel
    {

        [Required]
        public TimeSpan AppointmentDuration { get; set; } = TimeSpan.FromMinutes(30);

        public Dictionary<string, TimeSpan> Durations => (Dictionary<string, TimeSpan>)AppointmentDurationsCollections.AppointmentDurations;
        public List<DayOfWeek> Days { get; set; } = [];
        public List<DailyScheduleViewModel> DailySchedules { get; set; } = [];

        public List<DailyScheduleViewModel>? ExistedSchedule { get; set; } = [];
        
    }

}
