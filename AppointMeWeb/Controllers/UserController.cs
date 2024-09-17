
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using AppointMeWeb.Infrastrucure.Data.Models;
using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.ApplicationUser;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace AppointMeWeb.Controllers
{
    public class UserController : Controller
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
        [AllowAnonymous]
        [HttpGet]
        public IActionResult MyProfile()
        {
            MyProfileViewModel model = new()
            {
                LoginFormModel = new LoginFormModel(),
                RegisterUser = new RegisterUser
                {
                    RegisterFormModel = new RegisterFormModel(),
                    RegisterBusinessFormModel = new RegisterBusinessFormModel()
                }
            };
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            
            IEnumerable<RoleViewModel> roles = await customUserService.GetRolesAsync();

            RegisterFormModel formModel = new()
            {
                Roles = roles
            };

            RegisterBusinessFormModel businessForm = new()
            {
                Roles = roles
            };

            MyProfileViewModel model = new()
            {
                RegisterUser = new RegisterUser
                {
                    RegisterFormModel = formModel,
                    RegisterBusinessFormModel = businessForm
                }
            };

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormModel formModel)
        {


            return RedirectToAction("UserProfile", formModel);
        }
    }
}
