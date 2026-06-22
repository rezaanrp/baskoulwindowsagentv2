using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Tools
{
    public class UserNameViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected UserManager<AppUser> _userManager;
        protected IUsersService _userservice;

        public UserNameViewComponent(ICodeMarkaz codeMarkaz, IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager, IUsersService usersService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _userservice = usersService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

            if (string.IsNullOrEmpty(userId))
            {
                return View("Default", "نام کاربر"); // fallback if not logged in
            }

            var user = _userservice.GetById(userId);
            var username = user.Name + " " + user.Family;
            return View("Default", username);
        }
    }
}

