using AppointMeWeb.Infrastrucure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppointMeWeb.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserController(UserManager<ApplicationUser> _userManager
                , SignInManager<ApplicationUser> _signInManager)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {

            return View();
        }
    }
}
