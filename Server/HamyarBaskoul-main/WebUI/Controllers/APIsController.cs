using Application.Interfaces;
using Application.Services;
using Application.ViewModels.APIs;
using DataTables;
using Domain.Classes;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebUI.Controllers
{
    [Authorize(Policy = "ExcludeNonHamayars")]
    public class APIsController : BaseController
    {
        private readonly IUsersService _userservice;
        private readonly IAPIService _apiservice;

        public APIsController(UserManager<AppUser> userManager, 
            IUsersService userservice, IAPIService apiservice) : base(userManager)
        {
            _userManager = userManager;
            _userservice = userservice;
            _apiservice = apiservice;
        }

        [HttpPost]
        public async Task<JsonResult> GetToken([FromBody] TokenRequestViewModel model)
        {
            if (string.IsNullOrEmpty(model.HamPassword) || string.IsNullOrEmpty(model.HamUsername))
                return Json(new { success = false, message = "نام کاربری یا رمز عبور وارد نشده است." });

            var codmarkaz = _userservice.GetCodMarkazById(OnGetUserId());
            var returnedMessage = await _apiservice.GetToken(model.HamUsername, model.HamPassword, codmarkaz);
            if (returnedMessage.type == Domain.Classes.Type.Error)
                return Json(new { success = false, message = returnedMessage.message });
            return Json(new { success = true, token = returnedMessage.message });
        }

        [HttpGet]
        public async Task<JsonResult> Tahklie(long idTakhlie)
        {
            var model = await _apiservice.Takhlie(OnGetUserId(), idTakhlie);
            if (model == null) return Json(new { success = false  });
            return Json(new { success = true, data = model});
        }

        [HttpGet]
        public async Task<JsonResult> Bargiri(long idBargiri)
        {
            var model = await _apiservice.Bargiri(OnGetUserId(), idBargiri);
            if (model == null) return Json(new { success = false });
            return Json(new { success = true, data = model });
        }

        public async Task<IActionResult> SyncServer()
        {
            var user = _userservice.GetById(OnGetUserId());
            if (string.IsNullOrEmpty(user.Token))
            {
                TempData["TokenError"] = "توکنی برای کاربر ثبت نشده";
                return RedirectToAction("Index", "Home");
            }
            var model = await _apiservice.GetSyncServerView(user.CodMarkaz, user.Token, user.Id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendToServer([FromBody] long selectedDore)
        {
            var user = _userservice.GetById(OnGetUserId());
            var model = await _apiservice.SendToServer(user.CodMarkaz, user.Id, selectedDore);
            return Json(new { state = model.State, message = model.Message });
        }

        [HttpPost]
        public async Task<IActionResult> GetFromServer([FromBody] GetFromServerViewModel serverViewModel)
        {
            var user = _userservice.GetById(OnGetUserId());
            var response = await _apiservice.GetFromServer(serverViewModel, user.CodMarkaz, user.Id);
            return Json(new { success = response });
        }

        public async Task<IActionResult> AutoUpdate()
        {
            var user = _userservice.GetById(OnGetUserId());
            var result = await _apiservice.AutoUpdate(user.CodMarkaz, user.Id);
            return Json(new { state = result.State, message = result.Message });
        }
    }
}


