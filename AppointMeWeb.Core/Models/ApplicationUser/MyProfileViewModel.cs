namespace AppointMeWeb.Core.Models.ApplicationUser
{
    public class MyProfileViewModel
    {
        public LoginFormModel? LoginFormModel { get; set; }
        public RegisterFormModel? RegisterFormModel { get; set; } = null!;
    }
}
