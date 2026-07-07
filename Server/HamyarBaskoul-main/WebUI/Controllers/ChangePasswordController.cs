using Application.Features.Identity.Commands;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class ChangePasswordController : BaseController
    {
        private readonly IMediator _mediator;

        public ChangePasswordController(
            UserManager<AppUser> userManager,
            IMediator mediator) : base(userManager)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] ChangePasswordRequest request)
        {
            if (request == null)
            {
                return Json(new { success = false, message = "اطلاعات فرم ارسال نشده است." });
            }

            if (string.IsNullOrWhiteSpace(request.CurrentPassword) ||
                string.IsNullOrWhiteSpace(request.NewPassword) ||
                string.IsNullOrWhiteSpace(request.RepeatPassword))
            {
                return Json(new { success = false, message = "همه فیلدها را کامل کنید." });
            }

            if (request.NewPassword != request.RepeatPassword)
            {
                return Json(new { success = false, message = "تکرار پسورد با پسورد جدید برابر نیست." });
            }

            var result = await _mediator.Send(new ChangeCurrentUserPasswordCommand(
                OnGetUserId(),
                request.CurrentPassword,
                request.NewPassword));

            if (!result.Succeeded)
            {
                return Json(new
                {
                    success = false,
                    message = result.Errors.Count > 0
                        ? string.Join(" - ", result.Errors)
                        : "تغییر پسورد انجام نشد."
                });
            }

            return Json(new { success = true, message = "پسورد با موفقیت تغییر کرد." });
        }

        public sealed class ChangePasswordRequest
        {
            public string CurrentPassword { get; set; } = string.Empty;
            public string NewPassword { get; set; } = string.Empty;
            public string RepeatPassword { get; set; } = string.Empty;
        }
    }
}
