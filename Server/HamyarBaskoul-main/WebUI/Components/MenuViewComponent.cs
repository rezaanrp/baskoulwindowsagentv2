using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Components
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly UsersService _permissionService;
        private readonly UserManager<AppUser> _userManager;

        public MenuViewComponent(UsersService permissionService, UserManager<AppUser> userManager)
        {
            _permissionService = permissionService;
            _userManager = userManager;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User); // دریافت کاربر فعلی
            var userId = user?.Id; // دریافت ID کاربر
            var userPermissions = await _permissionService.GetUserFormAccessListAsync(userId ?? "");
		
            if(userPermissions != null)
			    return View(userPermissions.objectFormViews.Where(c => c.IsAccess).Select(c => c.Name).ToList());
			return View("");

		}
    }

}
