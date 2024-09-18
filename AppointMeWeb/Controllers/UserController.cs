using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.ApplicationUser;

namespace AppointMeWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger logger;
        private readonly ICustomUserService customUserService;

        public UserController(ICustomUserService _customUserService
            , ILogger<IFactory> _logger)
        {
            this.customUserService = _customUserService;
            this.logger = _logger;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult MyProfile()
        {
            MyProfileViewModel model = new ();
            return View(model);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            
            IEnumerable<RoleViewModel> roles = await customUserService.GetRolesAsync();

            RegisterFormModel registerFormModel = new()
            {
                Roles = roles
            }; 

            return View(registerFormModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if (model.IsBusinessProvider)
            {
                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    ModelState.AddModelError(nameof(model.Name), "Business Name is required.");
                }
                if (string.IsNullOrWhiteSpace(model.Description))
                {
                    ModelState.AddModelError(nameof(model.Description), "Business Description is required.");
                }
                if (string.IsNullOrWhiteSpace(model.Town))
                {
                    ModelState.AddModelError(nameof(model.Town), "Business Town is required.");
                }
                if (string.IsNullOrWhiteSpace(model.Address))
                {
                    ModelState.AddModelError(nameof(model.Address), "Business Address is required.");
                }
                if (string.IsNullOrWhiteSpace(model.Url))
                {
                    ModelState.AddModelError(nameof(model.Url), "Business URL is required.");
                }
            }

            if (!ModelState.IsValid)
            {
                model.Roles = await customUserService.GetRolesAsync();
                return RedirectToAction("MyProfile", new { tab = "register" });
            }

            try
            {
                bool registrationStatus = await customUserService.RegisterUserAsync(model);
                if (registrationStatus)
                {
                    return RedirectToAction(nameof(HomeController)); 
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed. Please try again.");
                    model.Roles = await customUserService.GetRolesAsync();
                    return RedirectToAction("MyProfile", new { tab = "register" });
                }
            }
            catch(Exception ex) 
            {
                logger.LogError(nameof(Register), ex);
                ModelState.AddModelError("", "An error occurred during registration. Please try again.");
                model.Roles = await customUserService.GetRolesAsync();
                return RedirectToAction("MyProfile", new { tab = "register" });
            }
            
        }
    }
}
