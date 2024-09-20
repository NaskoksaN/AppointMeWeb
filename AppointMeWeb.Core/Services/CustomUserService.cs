
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.ApplicationUser;
using AppointMeWeb.Infrastrucure.Data.Common;
using AppointMeWeb.Infrastrucure.Data.Models;

using static AppointMeWeb.Infrastrucure.Constants.DataConstants;
using AppointMeWeb.Core.Models.BusinessProvider;

namespace AppointMeWeb.Core.Services
{
    public class CustomUserService : ICustomUserService
    {
        private readonly RoleManager<IdentityRole<string>> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IFactory factory;
        private readonly IRepository sqlService;
        public CustomUserService(RoleManager<IdentityRole<string>> _roleManager
                , UserManager<ApplicationUser> _userManager
                , SignInManager<ApplicationUser> _signInManager
                , IFactory _factory
                , IRepository _sqlService)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            this.roleManager = _roleManager;
            this.factory = _factory;
            this.sqlService = _sqlService;
        }
        public async Task<IEnumerable<RoleViewModel>> GetRolesAsync()
        {
            var temp = roleManager.Roles;
            return await roleManager
                .Roles
                .Select(r => new RoleViewModel()
                {
                    Id = r.Id,
                    Name =r.Name ?? "",
                })
                .ToListAsync();
        }

        public async Task<IList<string>> GetUserRoleAsync(ApplicationUser user)
        {
            return await userManager.GetRolesAsync(user);
        }

        public async Task<ApplicationUser> LoginUserAsync(LoginFormModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result =
                await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                return await userManager.FindByEmailAsync(model.Email);
            }

            return null;
        }

        public async Task<bool> RegisterUserAsync(RegisterFormModel model)
        {
            if (model == null)
            {
               throw new ArgumentNullException(nameof(model));
            }
            var tempUser = await userManager.FindByEmailAsync(model.Email);
            if (tempUser != null)
            {
                throw new ArgumentException($"Employee with email: {model.Email} already exists!");
            }
            
            var user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DOB,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                UserName = model.Email,
            };
                 
            var userResult = await userManager.CreateAsync(user, model.Password);

            string roleName = model.IsBusinessProvider
                            ? BusinessRole
                            : WebUserRole;
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            var roleResult = await userManager.AddToRoleAsync(user, role.Name );

            if (userResult.Succeeded && roleResult.Succeeded && model.IsBusinessProvider) 
            {
                int businessId = await factory.CreateBusinessUserAndReturnId(model, user.Id);
                try
                {
                    user.BusinessServiceProviderId = businessId;
                    sqlService.Update<ApplicationUser>(user);
                    await sqlService.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Database failed to save info", ex);
                }
                
                return true;
            }
            return false;
        }

        public async Task<int> UpdateBusinessUserDurationDetails(TimeSpan appointmentDuration, string userId)
        {
            try
            {
                var businessUser = await sqlService
                        .All<BusinessServiceProvider>()
                        .Where(b => b.ApplicationUserId == userId)
                        .FirstOrDefaultAsync();
                if(businessUser == null)
                {
                    throw new ArgumentNullException("Not valid business user");
                }
                businessUser.AppointmentDuration = appointmentDuration;
                sqlService.Update<BusinessServiceProvider>(businessUser);
                await sqlService.SaveChangesAsync();
                return businessUser.Id;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database failed to save info", ex);
            }
        }
    }
}
