﻿using System.ComponentModel.DataAnnotations;

using static AppointMeWeb.Infrastrucure.Constants.DataConstants;
using static AppointMeWeb.Core.Constants.MessageConstants;

namespace AppointMeWeb.Core.Models.AppointmeModels
{
    public class BookSlotFormModel
    {
        [Required]
        public int BusinessId { get; set; }

        //[StringLength(AppointmentMessageMaxLength,
        //    ErrorMessage = LengthMessage,
        //    MinimumLength = AppointmentMessageMinLength)]
        public string? VisitMessage { get; set; }
        public DateOnly Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}