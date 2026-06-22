using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected  UserManager<AppUser> _userManager;
        public BaseController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public string OnGetUserId()
        {
            var user = _userManager.GetUserId(User);
            return user;
        }

    }
}

