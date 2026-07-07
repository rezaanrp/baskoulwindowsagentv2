using Application.Interfaces;
using Application.Services;
using Application.ViewModels;
using Application.ViewModels.Weighbridge;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class WeighbridgeSiteController : BaseController
    {
        private readonly IUsersService _userservice;
        private readonly IWeighbridgeSiteService _siteservice;
        private readonly IWeighbridgeService _baskoulservice;

        public WeighbridgeSiteController(UserManager<AppUser> userManager, IUsersService userservice, 
            IWeighbridgeSiteService siteservice, IWeighbridgeService baskoulservice) : base(userManager)
        {
            _userManager = userManager;
            _userservice = userservice;
            _siteservice = siteservice;
            _baskoulservice = baskoulservice;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SiteList()
        {
            var user = _userservice.GetById(OnGetUserId());
            var model = new ManageWeighbridgeSiteViewModel
            {
                siteViews = _siteservice.GetAllAsync(user.CodMarkaz) ?? new List<WeighbridgeSiteViewModel>(),
                baskoulViews = await _baskoulservice.GetBySiteAsync(user.SelectedSiteId ?? 0, user.CodMarkaz) ?? new List<WeighbridgeViewModel>(),
            };
            return View("ManageSite", model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSite()
        {
            var model = new WeighbridgeSiteViewModel
            {
                Company = _userservice.GetCodMarkazById(OnGetUserId()),
            };
            return PartialView("_AddSitePartial", model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSite(WeighbridgeSiteViewModel entity)
        {
            if (ModelState.IsValid)
            {
                await _siteservice.AddSite(entity);
                return Json(new { success = true });
            }

            // Send error message if ModelState is not valid
            ViewBag.ErrorMessage = "ذخیره با خطا مواجه شد. دوباره سعی کنید!";

            return PartialView("_AddSitePartial", entity);
        }

        public async Task<IActionResult> GetSiteListPartial()
        {
            var user = _userservice.GetById(OnGetUserId());
            var model = new ManageWeighbridgeSiteViewModel
            {
                siteViews = _siteservice.GetAllAsync(user.CodMarkaz) ?? new List<WeighbridgeSiteViewModel>(),
                baskoulViews = await _baskoulservice.GetBySiteAsync(user.SelectedSiteId ?? 0, user.CodMarkaz) ?? new List<WeighbridgeViewModel>(),
            };
            return PartialView("_SiteListPartial", model);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditSite(int id)
        {
            var entity = await _siteservice.GetByIdAsync(id);
            return PartialView("_EditSitePartial", entity);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditSite(WeighbridgeSiteViewModel entity)
        {
            if (ModelState.IsValid)
            {

                await _siteservice.UpdateSite(entity);
                return RedirectToAction("SiteList", "WeighbridgeSite");
            }

            // Send error message if ModelState is not valid
            ViewBag.ErrorMessage = "ویرایش با خطا مواجه شد. دوباره سعی کنید!";

            return PartialView("_EditSitePartial", entity);
        }
        /// <summary>
        /// type 1 == all user active sites
        /// type 2 == all markaz active sites
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SelectSite(int type)
        {
            var user = _userservice.GetById(OnGetUserId());
            ViewBag.SelectedSiteId = user.SelectedSiteId;
            var activeSites = _userservice.GetAllActiveSites(user.Id).ToList();
            if (!activeSites.Any())
            {
                TempData["NullSelectedSite"] = "سایت فعالی برای کاربر انتخاب نشده است";
                return RedirectToAction("Index", "Home");
            }

            return View(activeSites);
        }
        [HttpPost]
        public async Task<IActionResult> SaveSelectedSite(int selectedSiteId)
        {
            var userId = OnGetUserId();
            var saved = await _userservice.SaveSelectedSiteAsync(selectedSiteId, userId);
            var wantsJson = string.Equals(Request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.OrdinalIgnoreCase);
            if (!saved)
            {
                if (wantsJson)
                {
                    return Json(new { success = false, message = "سایت انتخاب شده در دسترسی‌های شما وجود ندارد." });
                }

                TempData["ErrorMessage"] = "سایت انتخاب شده در دسترسی‌های شما وجود ندارد.";
                return RedirectToAction("SelectSite", "WeighbridgeSite");
            }

            if (wantsJson)
            {
                return Json(new { success = true, message = "سایت پیش‌فرض ذخیره شد." });
            }

            return RedirectToAction("Index", "Home");
        }
    }
}

