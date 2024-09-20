
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.ApplicationUser;
using AppointMeWeb.Infrastrucure.Data.Common;
using AppointMeWeb.Infrastrucure.Data.Models;
using static AppointMeWeb.Infrastrucure.Constants.DataConstants;

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
                , IRepository _sqlService
                )
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            this.roleManager = _roleManager;
            this.factory = _factory;
            this.sqlService = _sqlService;
        }

        /// <summary>
        /// Asynchronously retrieves a list of roles from the role manager.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains an IEnumerable of RoleViewModel, which includes the role Id and Name.
        /// </returns>
        /// <exception cref="Exception">
        /// Thrown when an error occurs while fetching roles from the role manager.
        /// The inner exception provides more details on the error.
        /// </exception>
        public async Task<IEnumerable<RoleViewModel>> GetRolesAsync()
        {
            try
            {
                var temp = roleManager.Roles;
                return await roleManager
                    .Roles
                    .Select(r => new RoleViewModel()
                    {
                        Id = r.Id,
                        Name = r.Name ?? "",
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
               throw new Exception("An error occurred while fetching roles.", ex);
            }
        }

        /// <summary>
        /// Asynchronously retrieves the roles associated with the specified user.
        /// </summary>
        /// <param name="user">The user for whom to retrieve roles.</param>
        /// <returns>A list of role names associated with the user.</returns>
        /// <exception cref="ApplicationException">Thrown when an error occurs while fetching roles.</exception>
        public async Task<IList<string>> GetUserRoleAsync(ApplicationUser user)
        {
            try
            {
                return await userManager.GetRolesAsync(user);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching the user's roles.", ex);
            }
        }

        /// <summary>
        /// Asynchronously logs in a user using the provided login model.
        /// </summary>
        /// <param name="model">The login model containing email and password.</param>
        /// <returns>The logged-in user if successful; otherwise, null.</returns>
        public async Task<ApplicationUser?> LoginUserAsync(LoginFormModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await signInManager
                                    .PasswordSignInAsync(model.Email, model.Password, false, false);

            return result.Succeeded
                    ? await userManager.FindByEmailAsync(model.Email)
                    : null;
        }

        /// <summary>
        /// Registers a new user in the system based on the provided model, assigns the appropriate role,
        /// and if the user is a business provider, associates the user with a business entity.
        /// </summary>
        /// <param name="model">The registration form data containing user details.</param>
        /// <returns>A task representing the asynchronous operation. The task result is true if the registration 
        /// was successful, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the registration model or the role is null.</exception>
        /// <exception cref="ArgumentException">Thrown when a user with the provided email already exists.</exception>
        /// <exception cref="ApplicationException">Thrown when the database operation fails while saving the business ID.</exception>
        public async Task<bool> RegisterUserAsync(RegisterFormModel model)
        {
            ArgumentNullException.ThrowIfNull(model);

            var tempUser = await userManager.FindByEmailAsync(model.Email);
            if (tempUser != null)
            {
                throw new ArgumentException($"Employee with email: {model.Email} already exists!");
            }
            
            ApplicationUser user = factory.CreateApplicationUser( model);
            var userResult = await userManager.CreateAsync(user, model.Password);
            if (!userResult.Succeeded)
            {
                return false; 
            }

            string roleName = model.IsBusinessProvider
                            ? BusinessRole
                            : WebUserRole;
            var role = await roleManager.FindByNameAsync(roleName);
            
            ArgumentNullException.ThrowIfNull(role);

            var roleResult = await userManager.AddToRoleAsync(user, role.Name );
            if (!roleResult.Succeeded)
            {
                return false; 
            }

            if (model.IsBusinessProvider) 
            {
                try
                {
                    int businessId = await factory.CreateBusinessUserAndReturnId(model, user.Id);
                    user.BusinessServiceProviderId = businessId;
                    sqlService.Update<ApplicationUser>(user);
                    await sqlService.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Database failed to save info", ex);
                }
                
            }
            return true;
        }

        /// <summary>
        /// Updates the appointment duration details for a specified business user.
        /// </summary>
        /// <param name="appointmentDuration">The new appointment duration as a TimeSpan.</param>
        /// <param name="userId">The ID of the business user whose details are to be updated.</param>
        /// <returns>The ID of the updated business user.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the business user is not found.</exception>
        /// <exception cref="ApplicationException">Thrown when the database operation fails.</exception>
        public async Task<int> UpdateBusinessUserDurationDetails(TimeSpan appointmentDuration, string userId)
        {
            try
            {
                var businessUser = await sqlService
                        .All<BusinessServiceProvider>()
                        .Where(b => b.ApplicationUserId == userId)
                        .FirstOrDefaultAsync();

                ArgumentNullException.ThrowIfNull(businessUser);

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
