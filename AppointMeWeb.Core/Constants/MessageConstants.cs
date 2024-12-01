

namespace AppointMeWeb.Core.Constants
{
    public static class MessageConstants
    {
        public const string RequiredMessage = "The {0} is required.";
        public const string LengthMessage = "The {0} must be between {2} and {1} characters long.";
        public const string EmailErrMsg = "Invalid email address.";
        public const string UrlErrMsg = "Invalid URL.";
        public const string MinAgeErrorMsg = "You must be at least {0} years old to register.";
        public const string SchedulePeriodErrMsg = "The difference between Start Date and End Date cannot exceed {0} days.";
        public const string EndDateBeforeStartDateErrMsg = "End Date must be greater than or equal to Start Date.";
        public const string StartDateNotFoundErrMsg = "Start Date property not found.";
        public const string EndTimeBeforeStartTimeErrMsg = "You’ve entered an End Time that’s earlier than the Start Time. Please make sure the End Time is greater than or equal to the Start Time.";
        public const string DurationNotFoundErrMsg = "Duration property not found.";
    }
}
