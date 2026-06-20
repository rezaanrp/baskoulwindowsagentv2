using Application.Interfaces;
using Application.Services;
using Application.ViewModels.BaseData;
using DocumentFormat.OpenXml.Office2010.Excel;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WebUI.Controllers
{
    [Authorize(Policy = "ExcludeHamayars")]
    public class BaseDataController : BaseController
    {
        private readonly IUsersService _userservice;
        private readonly IBaseService _baseDataService;
        public BaseDataController(UserManager<AppUser> userManager, IUsersService userservice,
            IBaseService baseDataService) : base(userManager)
        {
            _userManager = userManager;
            _userservice = userservice;
            _baseDataService = baseDataService;
        }

        public IActionResult DataList()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ShowList(int type)
        {
            if (type == null)
                return RedirectToAction("DataList", "BaseData");
            var user = _userservice.GetById(OnGetUserId());
            var list = await _baseDataService.GetDataAsync(type, user.CodMarkaz, user.Id);
            var model = new ListPageViewModel
            {
                data = list,
                type = type
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddData(ListPageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid model data." });
            }

            if (model?.type == null)
                return RedirectToAction("DataList", "BaseData");

            var user = _userservice.GetById(OnGetUserId());
            await _baseDataService.AddAsync(model, user.CodMarkaz, user.Id);

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> EditBaseData(int id)
        {
            var entity = await _baseDataService.GetByIdAsync(id);
            if (entity == null)
                return PartialView("_ErrorPartial", "رکوردی یافت نشد.");

            var model = new BaseDataViewModel
            {
                ID = entity.ID,
                Onvan = entity.Onvan,
                Tozihat = entity.Tozihat,
            };

            return PartialView("_EditBaseDataPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBaseData(BaseDataViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "اطلاعات معتبر نیست." });

            var entity = await _baseDataService.GetByIdAsync(model.ID);
            if (entity == null)
                return Json(new { success = false, message = "یافت نشد." });

            entity.Onvan = model.Onvan;
            entity.Tozihat = model.Tozihat;

            var user = _userservice.GetById(OnGetUserId());
            await _baseDataService.UpdateAsync(entity, user.CodMarkaz);
            return Json(new { success = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBaseData(int Id)
        {
            var entity = await _baseDataService.GetByIdAsync(Id);
            if (entity == null)
            {
                return Json(new { success = false, message = "موردی برای حذف یافت نشد." });
            }

            await _baseDataService.DeleteAsync(entity);

            return Json(new { success = true, message = "حذف با موفقیت انجام شد." });
        }
    }
}
