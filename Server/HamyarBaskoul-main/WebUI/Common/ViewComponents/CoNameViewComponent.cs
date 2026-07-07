using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Tools
{
    public class CoNameViewComponent : ViewComponent
    {
        private readonly ICompanyService _codemarkaz;
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected UserManager<AppUser> _userManager;

        public CoNameViewComponent(ICompanyService codeMarkaz, IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager)
        {
            _codemarkaz = codeMarkaz;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

            // Extract URL from request
            string fullUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}{Request.Path}{Request.QueryString}";
            string pathBase = Request.PathBase.Value; // "/irisa"
            string appname = pathBase.Trim('/');

            if (string.IsNullOrEmpty(userId))
            {
                return View("Default", "سیستم باسکول همیار"); // fallback if not logged in
            }

            var brandText = await _codemarkaz.GetCoNameAsync(appname);
            return View("Default", brandText);
        }
    }
}

