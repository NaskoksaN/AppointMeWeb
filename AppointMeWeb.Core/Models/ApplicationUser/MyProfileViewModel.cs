namespace AppointMeWeb.Core.Models.ApplicationUser
{
    /// <summary>
    /// Represents the view model for user profile operations, including both login and registration forms.
    /// </summary>
    /// <remarks>
    /// This view model contains instances of <see cref="LoginFormModel"/> and <see cref="RegisterFormModel"/>.
    /// It is used to support operations that involve user authentication and registration within the profile section.
    /// </remarks>
    public class MyProfileViewModel
    {
        public LoginFormModel LoginFormModel { get; set; } = new LoginFormModel();
        public RegisterFormModel RegisterFormModel { get; set; } = new RegisterFormModel();

        public string ActiveTab { get; set; } = "login";
    }
}
