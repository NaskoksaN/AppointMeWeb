using AppointMeWeb.Core.Models.ApplicationUser;
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
        Task<bool> RegisterUserAsync(RegisterFormModel model);
    }
}
