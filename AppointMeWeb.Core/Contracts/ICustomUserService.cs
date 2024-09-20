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
        Task<IEnumerable<RoleViewModel>> GetRolesAsync();

        /// <summary>
        /// Asynchronously retrieves the roles associated with the specified user.
        /// </summary>
        /// <param name="user">The user for whom to retrieve roles.</param>
        /// <returns>A list of role names associated with the user.</returns>
        /// <exception cref="ApplicationException">Thrown when an error occurs while fetching roles.</exception>
        Task<IList<string>> GetUserRoleAsync(ApplicationUser user);

        /// <summary>
        /// Asynchronously logs in a user using the provided login model.
        /// </summary>
        /// <param name="model">The login model containing email and password.</param>
        /// <returns>The logged-in user if successful; otherwise, null.</returns>
        Task<ApplicationUser?> LoginUserAsync(LoginFormModel model);

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
        Task<bool> RegisterUserAsync(RegisterFormModel model);

        /// <summary>
        /// Updates the appointment duration details for a specified business user.
        /// </summary>
        /// <param name="appointmentDuration">The new appointment duration as a TimeSpan.</param>
        /// <param name="userId">The ID of the business user whose details are to be updated.</param>
        /// <returns>The ID of the updated business user.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the business user is not found.</exception>
        /// <exception cref="ApplicationException">Thrown when the database operation fails.</exception>
        Task<int> UpdateBusinessUserDurationDetails(TimeSpan appointmentDuration, string userId);
    }
}
