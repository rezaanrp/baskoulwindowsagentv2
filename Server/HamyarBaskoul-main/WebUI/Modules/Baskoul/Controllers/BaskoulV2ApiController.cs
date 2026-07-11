using Application.Features.BaskoulV2;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[Authorize]
[ApiController]
[Route("api/baskoul")]
public sealed class BaskoulV2ApiController(ISender sender, IAntiforgery antiforgery) : ControllerBase
{
    [HttpGet("antiforgery")]
    public IActionResult Antiforgery()
    {
        var tokens = antiforgery.GetAndStoreTokens(HttpContext);
        return Ok(new { token = tokens.RequestToken });
    }

    [HttpGet("form")]
    public Task<BaskoulFormDto> Form(CancellationToken ct) => sender.Send(new GetBaskoulFormQuery(), ct);

    [HttpGet("incomplete-by-plate")]
    public Task<BargeDto?> IncompleteByPlate([FromQuery] string plate, CancellationToken ct) =>
        sender.Send(new GetIncompleteBargeByPlateQuery(plate), ct);

    [HttpGet("active")]
    public Task<PagedBargesDto> Active([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default) =>
        sender.Send(new GetActiveBargesQuery(search, page, pageSize), ct);

    [HttpGet("drivers")]
    public Task<IReadOnlyList<LookupItemDto>> Drivers(CancellationToken ct) => sender.Send(new GetDriversQuery(), ct);

    [HttpGet("weighbridges")]
    public Task<IReadOnlyList<WeighbridgeDto>> Weighbridges(CancellationToken ct) => sender.Send(new GetWeighbridgesQuery(), ct);

    [HttpGet("{id:long}")]
    public async Task<IActionResult> ById(long id, CancellationToken ct) => await Execute(() => sender.Send(new GetBargeByIdQuery(id), ct));

    [HttpPost("first-weight")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FirstWeight([FromBody] RegisterFirstWeightCommand command, CancellationToken ct) =>
        await Execute(() => sender.Send(command, ct), created: true);

    [HttpPut("{id:long}/second-weight")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SecondWeight(long id, [FromBody] CompleteSecondWeightCommand command, CancellationToken ct) =>
        await Execute(() => sender.Send(command with { Id = id }, ct));

    [HttpPut("{id:long}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateBargeCommand command, CancellationToken ct) =>
        await Execute(() => sender.Send(command with { Id = id }, ct));

    [HttpPost("{id:long}/finalize")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Finalize(long id, CancellationToken ct) =>
        await Execute(() => sender.Send(new FinalizeBargeCommand(id), ct));

    [HttpPost("{id:long}/cancel")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(long id, CancellationToken ct) =>
        await Execute(() => sender.Send(new CancelBargeCommand(id), ct));

    private async Task<IActionResult> Execute<T>(Func<Task<T>> action, bool created = false)
    {
        try
        {
            var result = await action();
            return created ? StatusCode(StatusCodes.Status201Created, result) : Ok(result);
        }
        catch (ValidationException ex)
        {
            return ValidationProblem(new ValidationProblemDetails(ex.Errors
                .GroupBy(x => x.PropertyName).ToDictionary(x => x.Key, x => x.Select(y => y.ErrorMessage).ToArray())));
        }
        catch (BaskoulNotFoundException ex) { return Problem(ex.Message, statusCode: 404); }
        catch (BaskoulConflictException ex) { return Problem(ex.Message, statusCode: 409); }
    }
}
