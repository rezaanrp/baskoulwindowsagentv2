using Application.Features.WeighbridgeOrganization.Commands;
using Application.Features.WeighbridgeOrganization.Queries;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("WeighbridgeOrganization")]
    [Route("CodeMarkazForURL")]
    public class WeighbridgeOrganizationController : Controller
    {
        private const string ListViewPath = "~/Views/WeighbridgeOrganization/CodeURLList.cshtml";
        private const string InsertPartialPath = "~/Views/WeighbridgeOrganization/_InsertPartial.cshtml";
        private const string EditPartialPath = "~/Views/WeighbridgeOrganization/_EditPartial.cshtml";

        private readonly IMediator _mediator;

        public WeighbridgeOrganizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("InsertURLMarkaz")]
        public IActionResult InsertURLMarkaz()
        {
            var model = new CompanyViewModel();
            return PartialView(InsertPartialPath, model);
        }

        [HttpPost("InsertURLMarkaz")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertURLMarkaz(CompanyViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(InsertPartialPath, model);
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

        [HttpGet("CodeURLList")]
        public async Task<IActionResult> CodeURLList(CancellationToken cancellationToken)
        {
            var model = await _mediator.Send(new GetWeighbridgeOrganizationTreeQuery(), cancellationToken);
            return View(ListViewPath, model);
        }

        [HttpPost("SaveCompany")]
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

        [HttpPost("SaveSite")]
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

        [HttpPost("SaveScale")]
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
                    model.UserId,
                    model.Type), cancellationToken);
            }

            return RedirectToAction(nameof(CodeURLList));
        }

        [HttpPost("SaveScaleUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveScaleUser(CompanyScaleUserInputViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new AssignWeighbridgeSiteUserCommand(
                    model.CompanyCode,
                    model.SiteId,
                    model.UserId), cancellationToken);
            }

            return RedirectToAction(nameof(CodeURLList));
        }

        [HttpPost("DeleteScaleUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteScaleUser(CompanyScaleUserDeleteViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new RemoveWeighbridgeSiteUserCommand(model.SiteId, model.UserId), cancellationToken);
            }

            return RedirectToAction(nameof(CodeURLList));
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCompanyCommand(id), cancellationToken);
            return RedirectToAction(nameof(CodeURLList));
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var entity = await _mediator.Send(new GetCompanyForEditQuery(id), cancellationToken);
            if (entity == null)
            {
                return NotFound();
            }

            return PartialView(EditPartialPath, entity);
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(EditPartialPath, model);
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
