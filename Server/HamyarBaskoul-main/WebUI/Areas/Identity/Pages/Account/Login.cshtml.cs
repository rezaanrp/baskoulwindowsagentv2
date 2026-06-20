// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Domain.Models;
using Application.Interfaces;
using Application.Services;

namespace WebUI.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUsersService _userservice;
        private readonly ILogger<LoginModel> _logger;
        private readonly ICodeMarkaz _codemarkaz;

        public LoginModel(UserManager<AppUser> UserManager,SignInManager<AppUser> signInManager,
            ILogger<LoginModel> logger, IUsersService usersService, ICodeMarkaz codemarkaz)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = UserManager;
            _userservice = usersService;
            _codemarkaz = codemarkaz;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public string ReturnUrl { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
        public class InputModel
        {
            [Required(ErrorMessage = "ایمیل نامعتبر است")]
            [Display(Name = "ایمیل")]
            public string Email { get; set; }

            [Required(ErrorMessage = "رمز عبور را وارد نمایید")]
            [DataType(DataType.Password)]
            [Display(Name = "رمز عبور")]
            public string Password { get; set; }

            [Display(Name = "مرا به خاطر داشته باش")]
            public bool RememberMe { get; set; } 
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

       
            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password,true /*Input.RememberMe*/, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                    _logger.LogInformation("User logged in.");

					var user = await _userManager.FindByNameAsync(Input.Email);
					var roles = await _userManager.GetRolesAsync(user);
                    var activeSites = _userservice.GetAllActiveSites(user.Id);
                    var isCodmarkazValid = await _codemarkaz.ValidateCodemarkaz(user.CodMarkaz);

                    string fullUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}{Request.Path}{Request.QueryString}";
                    string pathBase = Request.PathBase.Value; // "/irisa"
                    string appname = pathBase.Trim('/');
                    var codemarkaz = _userservice.GetCodMarkazByURL(appname);
                    var isUserPermited = (user.CodMarkaz == codemarkaz);

                    if (roles.Contains("sale_manager") || roles.Contains("sale_user"))
					{
						return Redirect("~/Sale");
					}
                    if(user.Departments == "TARAHI")
                    {
                        return Redirect("~/DesignExecution");

                    }
                    if (!isCodmarkazValid)
                    {
                        await _signInManager.SignOutAsync();
                        TempData["LoginError"] = "کد مرکز برای کاربر تعریف نشده";
                        return RedirectToPage("./Login");
                    }
                    if (!roles.Contains("SuperAdmin"))
                    {
                        if (!isUserPermited)
                        {
                            await _signInManager.SignOutAsync();
                            TempData["LoginError"] = "مجوز ورود به این مرکز ندارید";
                            return RedirectToPage("./Login");
                        }
                    }
                    if (!activeSites.Any())
                    {
                        if (!roles.Contains("SuperAdmin") && !roles.Contains("Administrator"))
                        {
                            await _signInManager.SignOutAsync();
                            TempData["LoginError"] = "کاربر به هیچ سایت فعالی دسترسی ندارد";
                            return RedirectToPage("./Login");
                        }
                    }
					if (user.IsDelete == true)
					{
						await _signInManager.SignOutAsync();
						TempData["LoginError"] = "نام کاربری یا رمز عبور اشتباه است";
						return RedirectToPage("./Login");
					}

					return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
				else
                {
                    ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه است");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
