using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BaskoulV2;

public sealed record GetBaskoulFormQuery : IRequest<BaskoulFormDto>;
public sealed record GetIncompleteBargeByPlateQuery(string Plate) : IRequest<BargeDto?>;
public sealed record GetBargeByIdQuery(long Id) : IRequest<BargeDto>;
public sealed record GetActiveBargesQuery(string? Search, int Page = 1, int PageSize = 10) : IRequest<PagedBargesDto>;
public sealed record GetDriversQuery : IRequest<IReadOnlyList<LookupItemDto>>;
public sealed record GetWeighbridgesQuery : IRequest<IReadOnlyList<WeighbridgeDto>>;

public sealed class BaskoulQueryHandler(IReadDbContext db, ICurrentBaskoulUser currentUser) :
    IRequestHandler<GetBaskoulFormQuery, BaskoulFormDto>,
    IRequestHandler<GetIncompleteBargeByPlateQuery, BargeDto?>,
    IRequestHandler<GetBargeByIdQuery, BargeDto>,
    IRequestHandler<GetActiveBargesQuery, PagedBargesDto>,
    IRequestHandler<GetDriversQuery, IReadOnlyList<LookupItemDto>>,
    IRequestHandler<GetWeighbridgesQuery, IReadOnlyList<WeighbridgeDto>>
{
    public async Task<BaskoulFormDto> Handle(GetBaskoulFormQuery request, CancellationToken ct)
    {
        var scope = await currentUser.GetScopeAsync(ct);
        return new BaskoulFormDto(await GetDrivers(scope, ct), await GetScales(scope, ct), scope.CodeMarkaz, scope.SiteId);
    }

    public async Task<BargeDto?> Handle(GetIncompleteBargeByPlateQuery request, CancellationToken ct)
    {
        var scope = await currentUser.GetScopeAsync(ct);
        var plate = request.Plate.Trim();
        var item = await db.BargeBaskouls.AsNoTracking()
            .Where(x => x.CodMarkaz == scope.CodeMarkaz && x.siteId == scope.SiteId &&
                        x.ShomareMashin != null && x.ShomareMashin.Trim() == plate &&
                        x.FlgSabt != true && x.FlgEbtal != true &&
                        ((x.VaznPor > 0 && (!x.VanKhali.HasValue || x.VanKhali <= 0)) ||
                         (x.VanKhali > 0 && (!x.VaznPor.HasValue || x.VaznPor <= 0))))
            .OrderByDescending(x => x.ID).FirstOrDefaultAsync(ct);
        return item == null ? null : await Map(item, scope.CodeMarkaz, ct);
    }

    public async Task<BargeDto> Handle(GetBargeByIdQuery request, CancellationToken ct)
    {
        var scope = await currentUser.GetScopeAsync(ct);
        var item = await db.BargeBaskouls.AsNoTracking()
            .FirstOrDefaultAsync(x => x.ID == request.Id && x.CodMarkaz == scope.CodeMarkaz && x.siteId == scope.SiteId, ct)
            ?? throw new BaskoulNotFoundException("برگه پیدا نشد.");
        return await Map(item, scope.CodeMarkaz, ct);
    }

    public async Task<PagedBargesDto> Handle(GetActiveBargesQuery request, CancellationToken ct)
    {
        var scope = await currentUser.GetScopeAsync(ct);
        var page = Math.Max(1, request.Page);
        var size = Math.Clamp(request.PageSize, 1, 100);
        var query = db.BargeBaskouls.AsNoTracking()
            .Where(x => x.CodMarkaz == scope.CodeMarkaz && x.siteId == scope.SiteId);
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();
            query = query.Where(x => (x.ShomareMashin != null && x.ShomareMashin.Contains(search)) ||
                                     (x.GhabzBaskolID.HasValue && x.GhabzBaskolID.ToString()!.Contains(search)));
        }

        var count = await query.CountAsync(ct);
        var items = await query.OrderByDescending(x => x.ID).Skip((page - 1) * size).Take(size).ToListAsync(ct);
        var drivers = await DriverMap(scope.CodeMarkaz, ct);
        return new PagedBargesDto(items.Select(x => BaskoulMapping.ToDto(x, DriverName(x, drivers))).ToList(), page, size, count);
    }

    public async Task<IReadOnlyList<LookupItemDto>> Handle(GetDriversQuery request, CancellationToken ct) =>
        await GetDrivers(await currentUser.GetScopeAsync(ct), ct);

    public async Task<IReadOnlyList<WeighbridgeDto>> Handle(GetWeighbridgesQuery request, CancellationToken ct) =>
        await GetScales(await currentUser.GetScopeAsync(ct), ct);

    private async Task<IReadOnlyList<LookupItemDto>> GetDrivers(CurrentBaskoulScope scope, CancellationToken ct) =>
        await db.Mabanis.AsNoTracking().Where(x => x.CodMarkaz == scope.CodeMarkaz && x.TableName == "Ranande")
            .OrderBy(x => x.Onvan).Select(x => new LookupItemDto(x.ID, x.Onvan ?? "بدون نام")).ToListAsync(ct);

    private async Task<IReadOnlyList<WeighbridgeDto>> GetScales(CurrentBaskoulScope scope, CancellationToken ct) =>
        await db.Weighbridges.AsNoTracking().Where(x => x.CodMarkaz == scope.CodeMarkaz && x.WeighbridgeSiteId == scope.SiteId)
            .OrderBy(x => x.Name).Select(x => new WeighbridgeDto(x.Id, x.Name, x.ScaleCode, x.Type, 0)).ToListAsync(ct);

    private async Task<BargeDto> Map(Domain.Models.BargeBaskoul item, string code, CancellationToken ct)
    {
        var drivers = await DriverMap(code, ct);
        return BaskoulMapping.ToDto(item, DriverName(item, drivers));
    }

    private async Task<Dictionary<long, string>> DriverMap(string code, CancellationToken ct) =>
        await db.Mabanis.AsNoTracking().Where(x => x.CodMarkaz == code && x.TableName == "Ranande" && x.IDLinq.HasValue)
            .GroupBy(x => x.IDLinq!.Value).ToDictionaryAsync(x => x.Key, x => x.Select(y => y.Onvan).FirstOrDefault() ?? "ثبت نشده", ct);

    private static string DriverName(Domain.Models.BargeBaskoul item, IReadOnlyDictionary<long, string> drivers) =>
        !string.IsNullOrWhiteSpace(item.OnvanRanandeh) ? item.OnvanRanandeh :
        item.IDRanande.HasValue && drivers.TryGetValue(item.IDRanande.Value, out var name) ? name : "ثبت نشده";
}
