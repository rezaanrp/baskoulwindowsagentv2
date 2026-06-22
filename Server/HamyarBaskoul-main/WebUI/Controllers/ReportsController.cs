using Application.Interfaces;
using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Base;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class ReportsController : BaseController
    {
        private readonly IUsersService _userservice;
        private readonly IReports _reportservice;
        public ReportsController(UserManager<AppUser> userManager, IReports reports, IUsersService usersService) : base(userManager)
        {
            _reportservice = reports;
            _userservice = usersService;
            _userManager = userManager;
        }

        public async Task<IActionResult> ReportSetting()
        {
            var user = _userservice.GetById(OnGetUserId());
            var model = await _reportservice.GetReportSettingByCodMarkaz(user.CodMarkaz); 
            if (model == null)
                model = new ReportSettingViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ReportSetting(ReportSettingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userservice.GetById(OnGetUserId());
                model.KarbarIns = user.Id;
                model.KarbarUp = user.Id;
                model.CodeMarkaz = user.CodMarkaz;
                var result = await _reportservice.SaveReportSettings(model);
                if(result) 
                    ViewBag.Message = "تنظیمات با موفقیت ذخیره شد."; 
                return View(model); 
            }
            foreach (var state in ModelState)
            {
                var errors = state.Value.Errors;
                foreach (var error in errors)
                {
                    ViewBag.ErrorMessage = error.ErrorMessage;
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> PrintSimpleBarge(int id)
        {
            var barge = await _reportservice.GetSimpleReportViewModel(id);
            return View(barge);
        }

        [HttpGet]
        public async Task<IActionResult> PrintTripleBarge(int id)
        {
            var user = _userservice.GetById(OnGetUserId());
            var username = user.Name + " " + user.Family;
            var barge = await _reportservice.GetTripleleReportViewModel(id, username);
            if(barge == null)
            {
                return RedirectToAction("ListBargeAnbar", "BargeBaskoul", new { type = 4});
            }
            return View(barge);
        }
    }
}

