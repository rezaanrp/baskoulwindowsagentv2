using Application.Interfaces;
using Application.Services;
using Application.ViewModels.APIs;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebUI.Controllers
{
    [Authorize]
    public class WindowsAPIsController : BaseController
    {
        private readonly IUsersService _userservice;
        private readonly IAPIService _apiservice;

        public WindowsAPIsController(UserManager<AppUser> userManager,
            IUsersService userservice, IAPIService apiservice) : base(userManager)
        {
            _userManager = userManager;
            _userservice = userservice;
            _apiservice = apiservice;
        }

        [HttpPost]
        public async Task<JsonResult> GetToken([FromBody] WindowsTokenViewModel model)
        {
            if (string.IsNullOrEmpty(model.WinPassword))
                return Json(new { success = false, message = "نام کاربری یا رمز عبور وارد نشده است." });

            var user = _userservice.GetById(OnGetUserId());
            bool isValidPassword = await _userManager.CheckPasswordAsync(user, model.WinPassword);

            if (!isValidPassword)
                return Json(new { success = false, message = "نام کاربری یا رمز عبور وارد نشده است." });

            // Create JWT token
            string token = _apiservice.CreateWindowsToken(user);

            if (string.IsNullOrEmpty(token))
                return Json(new { success = false, message = "خطایی رخ داده است؛ دوباره امتحان فرمایید." });

            return Json(new { success = true, token });
        }

        [HttpGet]
        public IActionResult Index()
        {
            var user = _userservice.GetById(OnGetUserId());
            var model = new WindowsTokenViewModel
            {
                WindowsToken = user?.WindowsToken
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(WindowsTokenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        ViewBag.ErrorMessage = error.ErrorMessage;
                    }
                }
                return View(model);
            }

            if (string.IsNullOrEmpty(model.WindowsToken))
            {
                ViewBag.ErrorMessage = "ذخیره توکن با مشکل مواجه شد!";
                return View(model);
            }

            await _userservice.UpdateUserWindowsToken(model.WindowsToken, OnGetUserId());

            ViewBag.Success = "ذخیره توکن با موفقیت انجام شد!";
            return View(model);
        }
    }
}

