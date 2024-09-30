namespace AppointMeWeb.Infrastrucure.Constants
{
    public static class DataConstants
    {
        public const int ApplicationUserFirstNameMaxLength = 30;
        public const int ApplicationUserFirstNameMinLength = 5;

        public const int ApplicationUserLastNameMaxLength = 30;
        public const int ApplicationUserLastNameMinLength = 5;

        public const int UserPhoneMaxLength = 20;
        public const int UserPhoneMinLength = 6;

        public const int AppointmentMessageMaxLength = 150;

        public const int BusinessServiceProviderNameMaxLength = 50;
        public const int BusinessServiceProviderNameMinLength = 1;

        public const int BusinessServiceProviderDescriptionMaxLength = 100;
        public const int BusinessServiceProviderDescriptionMinLength = 1;

        public const int BusinessServiceProviderEmailMaxLength = 50;

        public const int BusinessServiceProviderTownMaxLength = 50;
        public const int BusinessServiceProviderTownMinLength = 2;

        public const int BusinessServiceProviderAddressMaxLength = 50;
        public const int BusinessServiceProviderAddressMinLength = 5;

        public const int BusinessServiceProviderUrlMaxLength = 250;

        public const string WebUserRole = "WebUser";
        public const string BusinessRole = "Business";
        public const string AdminRole = "Admin";
        
    }
}
