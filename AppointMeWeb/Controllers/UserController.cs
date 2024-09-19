using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.ApplicationUser;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using AppointMeWeb.Infrastrucure.Data.Models;
using System.Security.Claims;
using AppointMeWeb.Extensions;

namespace AppointMeWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger logger;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ICustomUserService customUserService;

        public UserController(ICustomUserService _customUserService
            , ILogger<IFactory> _logger
            , SignInManager<ApplicationUser> _signInManager)
        {
            this.customUserService = _customUserService;
            this.logger = _logger;
            this.signInManager = _signInManager;
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
        
        [HttpPost]
        [AllowAnonymous]
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
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                model.Roles = await customUserService.GetRolesAsync();
                return RedirectToAction("MyProfile", new { tab = "register" });
            }

            try
            {
                bool registrationStatus = await customUserService.RegisterUserAsync(model);
                if (registrationStatus)
                {
                    return RedirectToAction("MyProfile", new { tab = "login" }); 
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            LoginFormModel model = new ()
            {
                ReturnUrl = returnUrl,
            };
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login (LoginFormModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction(nameof(MyProfile), model);
                }

                ApplicationUser user = await customUserService.LoginUserAsync(model);
                if (user != null)
                {
                    var role = await customUserService.GetUserRoleAsync(user);
                    var roleClaims = role.Select(role => new Claim(ClaimTypes.Role, role));
                    var identity = (ClaimsIdentity?)User.Identity;
                    identity?.AddClaims(roleClaims);

                    if (user != null && User.IsAdmin())
                    {
                        return RedirectToAction("AdminHomeIndex", "Home", new { area = "AdminArea" });
                    }
                    else if (user != null && User.IsUser())
                    {
                        return RedirectToAction("UserHomeIndex", "Home", new { area = "UserArea" });
                    }
                    else if (user != null && User.IsBusinessProvider())
                    {
                        return RedirectToAction("BusinessHomeIndex", "Home", new { area = "BusinessArea" });
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your email and password.");
                    return View(model); 
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during login");
                LoginFormModel modelForm = new ();
                return RedirectToAction(nameof(MyProfile));
            }
            
        }

        [HttpPost]
        [AllowAnonymous]  
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");  
        }


    }
}
