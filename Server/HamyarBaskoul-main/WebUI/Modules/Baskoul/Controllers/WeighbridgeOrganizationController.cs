using Application.Features.WeighbridgeOrganization.Commands;
using Application.Features.WeighbridgeOrganization.Queries;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WeighbridgeOrganizationController : Controller
    {
        private readonly IMediator _mediator;

        public WeighbridgeOrganizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult InsertURLMarkaz()
        {
            var model = new CompanyViewModel();
            return PartialView("_InsertPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertURLMarkaz(CompanyViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_InsertPartial", model);
            }

            await _mediator.Send(new SaveCompanyCommand(
                model.Id,
                model.CoName,
                model.CodMarkaz,
                model.MarkazURL,
                model.APIURL,
                model.AutoAsync), cancellationToken);

            return RedirectToAction(nameof(CodeURLList));
        }

        [HttpGet]
        public async Task<IActionResult> CodeURLList(CancellationToken cancellationToken)
        {
            var model = await _mediator.Send(new GetWeighbridgeOrganizationTreeQuery(), cancellationToken);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCompany(CompanyInputViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new SaveCompanyCommand(
                    model.Id,
                    model.CompanyName,
                    model.CompanyCode,
                    model.ConnectionUrl,
                    model.ApiUrl,
                    model.AutoSync), cancellationToken);
            }

            return RedirectToAction(nameof(CodeURLList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSite(CompanySiteInputViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new SaveWeighbridgeSiteCommand(
                    model.Id,
                    model.CompanyCode,
                    model.SiteName,
                    model.IsActive), cancellationToken);
            }

            return RedirectToAction(nameof(CodeURLList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveScale(CompanyScaleInputViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new SaveWeighbridgeCommand(
                    model.Id,
                    model.CompanyCode,
                    model.SiteId,
                    model.Name,
                    model.ScaleCode,
                    model.Type), cancellationToken);
            }

            return RedirectToAction(nameof(CodeURLList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveScaleUser(CompanyScaleUserInputViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new AssignSiteUserCommand(
                    model.CompanyCode,
                    model.SiteId,
                    model.UserId), cancellationToken);
            }

            return RedirectToAction(nameof(CodeURLList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteScaleUser(CompanyScaleUserDeleteViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new RemoveSiteUserCommand(model.SiteId, model.UserId), cancellationToken);
            }

            return RedirectToAction(nameof(CodeURLList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCompanyCommand(id), cancellationToken);
            return RedirectToAction(nameof(CodeURLList));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var entity = await _mediator.Send(new GetCompanyForEditQuery(id), cancellationToken);
            if (entity == null)
            {
                return NotFound();
            }

            return PartialView("_EditPartial", entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_EditPartial", model);
            }

            await _mediator.Send(new SaveCompanyCommand(
                model.Id,
                model.CoName,
                model.CodMarkaz,
                model.MarkazURL,
                model.APIURL,
                model.AutoAsync), cancellationToken);

            return RedirectToAction(nameof(CodeURLList));
        }
    }
}
