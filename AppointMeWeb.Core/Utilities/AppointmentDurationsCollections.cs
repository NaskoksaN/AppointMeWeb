namespace AppointMeWeb.Core.Utilities
{
    public static class AppointmentDurationsCollections
    {
        private  static readonly Dictionary<string, TimeSpan> appointmentDurations = new Dictionary<string, TimeSpan>
    {
        { "20 minutes", TimeSpan.FromMinutes(20) },
        { "30 minutes", TimeSpan.FromMinutes(30) },
        { "40 minutes", TimeSpan.FromMinutes(40) },
        { "1 hour", TimeSpan.FromHours(1) },
        { "1.5 hours", TimeSpan.FromHours(1.5) },
        { "2 hours", TimeSpan.FromHours(2) }
    };

        public static IReadOnlyDictionary<string, TimeSpan> AppointmentDurations => appointmentDurations;
    }
}
