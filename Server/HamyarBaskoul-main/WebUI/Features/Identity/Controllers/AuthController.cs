using Application.Interfaces;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Features.Identity.ViewModels;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUsersService _usersService;
        private readonly ICompanyService _codeMarkazService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IUsersService usersService,
            ICompanyService codeMarkazService,
            ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _usersService = usersService;
            _codeMarkazService = codeMarkazService;
            _logger = logger;
        }

        [HttpGet("/Identity/Account/Login")]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            var model = new LoginViewModel
            {
                ReturnUrl = GetSafeReturnUrl(returnUrl)
            };

            return View("~/Features/Identity/Views/Auth/Login.cshtml", model);
        }

        [HttpPost("/Identity/Account/Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            model.ReturnUrl = GetSafeReturnUrl(model.ReturnUrl);

            if (!ModelState.IsValid)
            {
                return View("~/Features/Identity/Views/Auth/Login.cshtml", model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    await _signInManager.SignOutAsync();
                    ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه است");
                    return View("~/Features/Identity/Views/Auth/Login.cshtml", model);
                }

                var roles = await _userManager.GetRolesAsync(user);
                var activeSites = _usersService.GetAllActiveSites(user.Id);
                var isCodemarkazValid = await _codeMarkazService.ValidateCodemarkaz(user.CodMarkaz);
                var appName = GetAppName();
                var codemarkaz = _usersService.GetCodMarkazByURL(appName);
                var shouldCheckUrlCodemarkaz = !string.IsNullOrWhiteSpace(appName);
                var isUserPermited = !shouldCheckUrlCodemarkaz || user.CodMarkaz == codemarkaz;

                if (roles.Contains("sale_manager") || roles.Contains("sale_user"))
                {
                    return Redirect("~/Sale");
                }

                if (!isCodemarkazValid)
                {
                    await _signInManager.SignOutAsync();
                    ModelState.AddModelError(string.Empty, "کد مرکز برای کاربر تعریف نشده");
                    return View("~/Features/Identity/Views/Auth/Login.cshtml", model);
                }

                if (!roles.Contains("Admin") && !isUserPermited)
                {
                    await _signInManager.SignOutAsync();
                    ModelState.AddModelError(string.Empty, "مجوز ورود به این مرکز ندارید");
                    return View("~/Features/Identity/Views/Auth/Login.cshtml", model);
                }

                if (!activeSites.Any() && !roles.Contains("Admin"))
                {
                    await _signInManager.SignOutAsync();
                    ModelState.AddModelError(string.Empty, "کاربر به هیچ سایت فعالی دسترسی ندارد");
                    return View("~/Features/Identity/Views/Auth/Login.cshtml", model);
                }

                if (user.IsDelete)
                {
                    await _signInManager.SignOutAsync();
                    ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه است");
                    return View("~/Features/Identity/Views/Auth/Login.cshtml", model);
                }

                _logger.LogInformation("User logged in.");
                return LocalRedirect(model.ReturnUrl);
            }

            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("/Account/LoginWith2fa", new
                {
                    area = "Identity",
                    ReturnUrl = model.ReturnUrl,
                    RememberMe = model.RememberMe
                });
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return RedirectToPage("/Account/Lockout", new { area = "Identity" });
            }

            ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه است");
            return View("~/Features/Identity/Views/Auth/Login.cshtml", model);
        }

        private string GetSafeReturnUrl(string? returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return returnUrl;
            }

            return Url.Content("~/");
        }

        private string GetAppName()
        {
            if (HttpContext.Items.TryGetValue("AppName", out var appNameValue) &&
                appNameValue is string appNameFromMiddleware &&
                !string.Equals(appNameFromMiddleware, "Identity", StringComparison.OrdinalIgnoreCase))
            {
                return appNameFromMiddleware;
            }

            return Request.PathBase.Value?.Trim('/') ?? string.Empty;
        }
    }
}
