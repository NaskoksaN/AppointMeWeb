
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using AppointMeWeb.Infrastrucure.Data.Models;
using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.ApplicationUser;

namespace AppointMeWeb.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ICustomUserService customUserService;

        public UserController(UserManager<ApplicationUser> _userManager
                , SignInManager<ApplicationUser> _signInManager
                ,ICustomUserService _customUserService )
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            this.customUserService = _customUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            IEnumerable<RoleViewModel> roles = await customUserService.GetRolesAsync();
            RegisterFormModel formModel = new ()
            {
                Roles = roles
            };

            return View(formModel);
        }
    }
}
