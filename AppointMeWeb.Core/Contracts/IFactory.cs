using AppointMeWeb.Core.Models.ApplicationUser;
using AppointMeWeb.Core.Models.BusinessProvider;

namespace AppointMeWeb.Core.Contracts
{
    public interface IFactory
    {
        public Task<int> CreateBusinessUserAndReturnId(RegisterFormModel model, string userId);
        Task<bool> CreateWorkSchedule(List<DailyScheduleViewModel> dailySchedules, int businessUserId);
    }
}
