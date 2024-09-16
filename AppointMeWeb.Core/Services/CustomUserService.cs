using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointMeWeb.Core.Services
{
    public class CustomUserService : ICustomUserService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public CustomUserService(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;
        }
        public async Task<IEnumerable<RoleViewModel>> GetRolesAsync()
        {
            return await roleManager
                .Roles
                .Select(r => new RoleViewModel()
                {
                    Id = r.Id,
                    Name =r.Name != null ? r.Name : "",
                })
                .ToListAsync();
        }
    }
}
