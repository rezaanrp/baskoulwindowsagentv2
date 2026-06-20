using Application.Interfaces;
using Application.Security;
using Application.Services;
using Application.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private IUsersService _usersService;
        public HomeController(IUsersService usersService)
        {
                _usersService = usersService;
        }
        public IActionResult Index()
        {
            return View(_usersService.GetUserInformation(User.Identity.GetEmail()));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(UsersListViewModel model)
        {
            _usersService.UpdateUser(model);
            return RedirectToAction("Index", "UserPanel");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var usm = _usersService.Get(id);
            return View(usm);
        }
    }
}
