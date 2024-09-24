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
        /// Asynchronously retrieves the ID of a BusinessServiceProvider based on the provided user ID.
        /// The user ID can be either an integer (business ID) or a string (application user ID).
        /// Throws an ArgumentException for unsupported user ID types and an ArgumentNullException if no matching business provider is found.
        /// </summary>
        /// <typeparam name="T">The type of the user ID, which can be either int or string.</typeparam>
        /// <param name="userId">The user ID used to query for the BusinessServiceProvider.</param>
        /// <returns>The ID of the matching BusinessServiceProvider.</returns>
        /// <exception cref="ArgumentException">Thrown when an unsupported user ID type is provided.</exception>
        /// <exception cref="ApplicationException">Thrown when the database fails to retrieve the business user info.</exception>
        public Task<int> GetBusinessUserIdAsync<T>(T userId);
    }
}
