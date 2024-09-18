using AppointMeWeb.Core.Models.ApplicationUser;

namespace AppointMeWeb.Core.Contracts
{
    public interface IFactory
    {
        public Task<int> CreateBusinessUserAndReturnId(RegisterFormModel model, string userId);
    }
}
