using Application.Interfaces;
using Application.Services;
using Application.ViewModels;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class CodeMarkazForURLController : Controller
    {
        private readonly ICodeMarkaz _codemarkazService;

        public CodeMarkazForURLController(ICodeMarkaz codemarkazService)
        {
            _codemarkazService = codemarkazService;
        }
        [HttpGet]
        public IActionResult InsertURLMarkaz()
        {
            var model = new CodeMarkazViewModel();
            return PartialView("_InsertPartial", model);
        }
        [HttpPost]
        public async Task<IActionResult> InsertURLMarkaz(CodeMarkazViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        ViewBag.ErrorMessagee = error.ErrorMessage;
                    }
                }
                return PartialView("_InsertPartial", model);
            }
            await _codemarkazService.Insert(model);
            return RedirectToAction("CodeURLList");
        }
        public async Task<IActionResult> CodeURLList(int page = 1, int pageSize = 10)
        {
            ViewBag.currentPageMarkaz = page;
            var entity = await _codemarkazService.GetAllAsync(page, pageSize);
            var model = new PagedResultCodeMarkazList
            {
                Markazes = entity.Markazes,
                TotalCount = entity.TotalCount
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _codemarkazService.Delete(id);

            return RedirectToAction("CodeURLList");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _codemarkazService.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return PartialView("_EditPartial", entity);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CodeMarkazViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        ViewBag.ErrorMessagee = error.ErrorMessage;
                    }
                }
                return PartialView("_EditPartial", model);
            }
            await _codemarkazService.Update(model);
            return RedirectToAction("CodeURLList");
        }
    }
}

