namespace AppointMeWeb.Core.Constants
{
    public static class DateConstants
    {
        public static readonly DateOnly Today = DateOnly.FromDateTime(DateTime.Now);
        public static readonly DateOnly Tomorrow = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
        public static readonly DateOnly NextWeekEnd = DateOnly.FromDateTime(DateTime.Now.AddDays(7));
        public static readonly DateOnly NextThirtyDays = Tomorrow.AddDays(30);
        public static int RatePeriod = 7;

    }
}
