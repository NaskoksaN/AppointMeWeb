using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.ApplicationUser;
using AppointMeWeb.Extensions;
using AppointMeWeb.Infrastrucure.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace AppointMeWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> logger;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ICustomUserService customUserService;
        private readonly IBusinessService businessService;

        public UserController(ICustomUserService _customUserService
            , ILogger<UserController> _logger
            , SignInManager<ApplicationUser> _signInManager
            , IBusinessService _businessService)
        {
            this.customUserService = _customUserService;
            this.logger = _logger;
            this.signInManager = _signInManager;
            this.businessService = _businessService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult MyProfile()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsBusinessProvider())
                {
                    return RedirectToAction("BusinessHomeIndex", "Home", new { area = "BusinessArea" });
                }
                else if (User.IsAdmin())
                {
                    return RedirectToAction("AdminHomeIndex", "Home", new { area = "AdminArea" });
                }
                else if (User.IsUser())
                {
                    return RedirectToAction("UserHomeIndex", "Home", new { area = "UserArea" });
                }
            }

            MyProfileViewModel model = new();
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {

            try
            {
                IEnumerable<RoleViewModel> roles = await customUserService.GetRolesAsync();

                RegisterFormModel registerFormModel = new()
                {
                    Roles = roles
                };

                return View(registerFormModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during user registration.");
                return View();
            }
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
                model.Roles = await customUserService.GetRolesAsync();
                return RedirectToAction("MyProfile", new { tab = "register" });
            }

            bool registrationStatus;
            try
            {
                registrationStatus = await customUserService.RegisterUserAsync(model);
            }
            catch (Exception ex)
            {
                logger.LogError("Error in register controller", ex);
                ModelState.AddModelError("", "An error occurred during registration. Please try again.");
                return RedirectToAction("MyProfile", new { tab = "register" });
            }

            if (registrationStatus)
            {
                return RedirectToAction("MyProfile", new { tab = "login" });
            }

            ModelState.AddModelError("", "Registration failed. Please try again.");
            return RedirectToAction("MyProfile", new { tab = "register" });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            try
            {
                if (User?.Identity?.IsAuthenticated ?? false)
                {
                    return RedirectToAction("Index", "Home");
                }

                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

                LoginFormModel model = new()
                {
                    ReturnUrl = returnUrl,
                };
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during Login GET request");

                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View(new LoginFormModel { ReturnUrl = returnUrl });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginFormModel model)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(MyProfile), model);
            }
            try
            {
                ApplicationUser? user = await customUserService.LoginUserAsync(model);
                if (user != null)
                {
                    var role = await customUserService.GetUserRoleAsync(user);
                    var roleClaims = role.Select(role => new Claim(ClaimTypes.Role, role));
                    var identity = (ClaimsIdentity?)User.Identity;
                    identity?.AddClaims(roleClaims);

                    return user switch
                    {
                        _ when User.IsAdmin() 
                            => RedirectToAction("AdminHomeIndex", "Home", new { area = "AdminArea" }),
                        _ when User.IsUser() 
                            => RedirectToAction("UserHomeIndex", "Home", new { area = "UserArea" }),
                        _ when User.IsBusinessProvider() 
                            => RedirectToAction("BusinessHomeIndex", "Home", new { area = "BusinessArea" }),
                        _ => RedirectToAction(nameof(Index)),
                    };
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
                LoginFormModel modelForm = new();
                return RedirectToAction(nameof(MyProfile));
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during logout");
            }

            return RedirectToAction("Index", "Home");
        }


    }
}
