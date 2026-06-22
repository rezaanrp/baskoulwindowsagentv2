using Application.Interfaces;
using Application.Services;
using Application.ViewModels;
using Application.ViewModels.Baskoul;
using DocumentFormat.OpenXml.Office2010.Excel;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [Authorize]
    public class BaskoulListController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBaskoulService _baskoulservice;
        private readonly IUsersService _userservice;
        private readonly ISite _siteservice;

        public BaskoulListController(ILogger<BaskoulListController> logger, IBaskoulService baskoulservice,
            IUsersService userservice, UserManager<AppUser> userManager, ISite siteservice) : base(userManager)
        {
            _baskoulservice = baskoulservice;
            _userManager = userManager;
            _userservice = userservice;
            _siteservice = siteservice;
        }
        public async Task<IActionResult> BaskoulList()
        {
            return RedirectToAction("SiteList", "Site");
        }
        [HttpGet]
        public async Task<IActionResult> AddBaskoul(int siteid)
        {
            var user = _userservice.GetById(OnGetUserId());
            var model = new BaskoulViewModel
            {
                CodMarkaz = _userservice.GetCodMarkazById(OnGetUserId()),
                UserID= user.Id,
                Site = siteid
            };
            return PartialView("_AddBaskoulPartial", model);
        }
        [HttpPost]
        public async Task<IActionResult> AddBaskoul(BaskoulViewModel entity)
        {
            if (ModelState.IsValid)
            {
                await _baskoulservice.AddAsync(entity);
                return Json(new { success = true });
            }

            // Send error message if ModelState is not valid
            ViewBag.ErrorMessage = "ذخیره با خطا مواجه شد. دوباره سعی کنید!";

            return PartialView("_AddBaskoulPartial", entity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBaskoul(int Id)
        {
            var entity = await _baskoulservice.GetByIdAsync(Id);
            if (entity == null)
            {
                return Json(new { success = false, message = "موردی برای حذف یافت نشد." });
            }

            await _baskoulservice.DeleteAsync(entity);

            return Json(new { success = true, message = "حذف با موفقیت انجام شد." });
        }

        [HttpGet]
        public async Task<IActionResult> EditBaskoul(int id)
        {
            var entity = await _baskoulservice.GetByIdAsync(id);

            return PartialView("_EditBaskoulPartial", entity);
        }
        [HttpPost]
        public async Task<IActionResult> EditBaskoul(BaskoulViewModel entity)
        {
            if (ModelState.IsValid)
            {
                await _baskoulservice.UpdateAsync(entity);
                return Json(new { success = true });
            }
            // Send error message if ModelState is not valid
            ViewBag.ErrorMessage = "ویرایش با خطا مواجه شد. دوباره سعی کنید!";

            return PartialView("_EditBaskoulPartial", entity);
        }

        [HttpGet]
        public async Task<IActionResult> GetBaskoulsBySite(int siteid)
        {
            var user = _userservice.GetById(OnGetUserId());
            if (user == null) return null;
            // Fetch baskoul data from DB
            var baskouls = await _baskoulservice.GetBySiteAsync(siteid, user.CodMarkaz);
            //ViewBag.SiteName = await _siteservice.GetNameById(siteid);

            return PartialView("_BaskoulTablePartial", baskouls);
        }
    }
}

