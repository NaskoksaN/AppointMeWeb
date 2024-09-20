using AppointMeWeb.Core.Models.ApplicationUser;
using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Infrastrucure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointMeWeb.Core.Contracts
{
    public interface ICustomUserService
    {
        Task<IEnumerable<RoleViewModel>> GetRolesAsync();
        Task<IList<string>> GetUserRoleAsync(ApplicationUser user);
        Task<ApplicationUser> LoginUserAsync(LoginFormModel model);
        Task<bool> RegisterUserAsync(RegisterFormModel model);
        Task<int> UpdateBusinessUserDurationDetails(TimeSpan appointmentDuration, string userId);
    }
}
