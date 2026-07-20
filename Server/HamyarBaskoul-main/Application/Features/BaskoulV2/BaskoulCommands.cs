using Application.Common.Interfaces;
using Domain.Models;
using Domain.Rules;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Application.Features.BaskoulV2;

public sealed record RegisterFirstWeightCommand(
    string Plate, float Weight, long? DriverId, string? DriverName, string? Description, long? WeighbridgeId) : IRequest<WeightCommandResult>;
public sealed record CompleteSecondWeightCommand(
    long Id, float Weight, long? DriverId, string? DriverName, string? Description, long? WeighbridgeId) : IRequest<WeightCommandResult>;
public sealed record UpdateBargeCommand(long Id, string Plate, long? DriverId, string? DriverName, string? Description) : IRequest<BargeDto>;
public sealed record FinalizeBargeCommand(long Id) : IRequest<WeightCommandResult>;
public sealed record CancelBargeCommand(long Id) : IRequest<WeightCommandResult>;

public sealed class RegisterFirstWeightValidator : AbstractValidator<RegisterFirstWeightCommand>
{
    public RegisterFirstWeightValidator()
    {
        RuleFor(x => x.Plate).NotEmpty().MaximumLength(30);
        RuleFor(x => x.Weight).GreaterThan(0);
        RuleFor(x => x.WeighbridgeId).NotNull().GreaterThan(0);
        RuleFor(x => x).Must(x => x.DriverId.HasValue || !string.IsNullOrWhiteSpace(x.DriverName))
            .WithMessage("راننده را انتخاب یا وارد کنید.");
    }
}

public sealed class CompleteSecondWeightValidator : AbstractValidator<CompleteSecondWeightCommand>
{
    public CompleteSecondWeightValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Weight).GreaterThan(0);
        RuleFor(x => x.WeighbridgeId).NotNull().GreaterThan(0);
    }
}

public sealed class BaskoulCommandHandler(IWriteDbContext db, ICurrentBaskoulUser currentUser) :
    IRequestHandler<RegisterFirstWeightCommand, WeightCommandResult>,
    IRequestHandler<CompleteSecondWeightCommand, WeightCommandResult>,
    IRequestHandler<UpdateBargeCommand, BargeDto>,
    IRequestHandler<FinalizeBargeCommand, WeightCommandResult>,
    IRequestHandler<CancelBargeCommand, WeightCommandResult>
{
    public async Task<WeightCommandResult> Handle(RegisterFirstWeightCommand request, CancellationToken ct)
    {
        var scope = await currentUser.GetScopeAsync(ct);
        var weighbridgeName = await ValidateWeighbridge(request.WeighbridgeId, scope, ct);
        return await db.ExecuteInTransactionAsync(async token =>
        {
            var plate = request.Plate.Trim();
            var duplicate = await db.BargeBaskouls.AnyAsync(x =>
                x.CodMarkaz == scope.CodeMarkaz && x.siteId == scope.SiteId && x.ShomareMashin != null &&
                x.ShomareMashin.Trim() == plate && x.FlgSabt != true && x.FlgEbtal != true &&
                ((x.VaznPor > 0 && (!x.VanKhali.HasValue || x.VanKhali <= 0)) ||
                 (x.VanKhali > 0 && (!x.VaznPor.HasValue || x.VaznPor <= 0))), token);
            if (duplicate) throw new BaskoulConflictException("این پلاک یک برگه ناقص دارد؛ وزن دوم را روی همان برگه ثبت کنید.");

            var now = DateTime.Now;
            var driver = await ResolveDriver(request.DriverId, scope.CodeMarkaz, token);
            var item = new BargeBaskoul
            {
                ShomareMashin = plate,
                VaznPor = request.Weight,
                VanKhali = 0,
                IDRanande = driver,
                OnvanRanandeh = driver.HasValue ? null : request.DriverName?.Trim(),
                Tozihat = request.Description?.Trim(),
                IDBaskul = request.WeighbridgeId,
                CodMarkaz = scope.CodeMarkaz,
                siteId = scope.SiteId,
                DateBarge = PersianDate(now),
                DateBaskol = PersianDate(now),
                TimeBarge = now.ToString("HH:mm:ss"),
                TimeBaskol = now.ToString("HH:mm:ss"),
                TimeVaznPor = now.ToString("HH:mm:ss"),
                DateTimeBarge = now,
                Karbar_Ins = scope.UserId,
                GhabzBaskolID = await NextReceipt(scope.CodeMarkaz, now, token)
            };
            db.BargeBaskouls.Add(item);
            await db.SaveChangesAsync(token);
            return new WeightCommandResult(item.ID, item.GhabzBaskolID, "در حال توزین", $"وزن اول با {weighbridgeName} ثبت شد؛ آماده خودروی بعدی");
        }, cancellationToken: ct);
    }

    public async Task<WeightCommandResult> Handle(CompleteSecondWeightCommand request, CancellationToken ct)
    {
        var scope = await currentUser.GetScopeAsync(ct);
        var weighbridgeName = await ValidateWeighbridge(request.WeighbridgeId, scope, ct);
        return await db.ExecuteInTransactionAsync(async token =>
        {
            var item = await Scoped(request.Id, scope, token);
            if (!BaskoulWeightRules.IsIncomplete(item)) throw new BaskoulConflictException("این برگه ناقص نیست یا قبلاً تکمیل شده است.");
            var previous = BaskoulWeightRules.IsPositive(item.VaznPor) ? item.VaznPor!.Value : item.VanKhali!.Value;
            if (Math.Abs(previous - request.Weight) < 0.001f) throw new BaskoulConflictException("وزن دوم نباید با وزن اول برابر باشد.");

            var calculated = BaskoulWeightRules.Calculate(request.Weight, previous);
            item.VaznPor = calculated.Full;
            item.VanKhali = calculated.Empty;
            item.TypeBarge = calculated.Type;
            item.Tozihat = request.Description?.Trim() ?? item.Tozihat;
            item.IDBaskul = request.WeighbridgeId ?? item.IDBaskul;
            item.TimeVaznKhali = DateTime.Now.ToString("HH:mm:ss");
            item.Date_Up = DateTime.Now;
            item.Karbar_Up = scope.UserId;
            item.FlgSabt = true;
            item.Karbar_Sabt = scope.UserId;
            item.Date_Sabt = DateTime.Now;
            var driver = await ResolveDriver(request.DriverId, scope.CodeMarkaz, token);
            if (driver.HasValue) { item.IDRanande = driver; item.OnvanRanandeh = null; }
            else if (!string.IsNullOrWhiteSpace(request.DriverName)) item.OnvanRanandeh = request.DriverName.Trim();
            await db.SaveChangesAsync(token);
            return new WeightCommandResult(item.ID, item.GhabzBaskolID, "تکمیل شده", $"وزن دوم با {weighbridgeName} ثبت و برگه تکمیل شد");
        }, cancellationToken: ct);
    }

    public async Task<BargeDto> Handle(UpdateBargeCommand request, CancellationToken ct)
    {
        var scope = await currentUser.GetScopeAsync(ct);
        var item = await Scoped(request.Id, scope, ct);
        if (item.FlgSabt == true || item.FlgEbtal == true) throw new BaskoulConflictException("برگه نهایی یا باطل‌شده قابل ویرایش نیست.");
        item.ShomareMashin = request.Plate.Trim();
        item.Tozihat = request.Description?.Trim();
        item.IDRanande = await ResolveDriver(request.DriverId, scope.CodeMarkaz, ct);
        item.OnvanRanandeh = item.IDRanande.HasValue ? null : request.DriverName?.Trim();
        item.Date_Up = DateTime.Now;
        item.Karbar_Up = scope.UserId;
        await db.SaveChangesAsync(ct);
        return BaskoulMapping.ToDto(item, item.OnvanRanandeh ?? "ثبت شده");
    }

    public async Task<WeightCommandResult> Handle(FinalizeBargeCommand request, CancellationToken ct)
    {
        var scope = await currentUser.GetScopeAsync(ct);
        var item = await Scoped(request.Id, scope, ct);
        if (!BaskoulWeightRules.HasTwoWeights(item.VaznPor, item.VanKhali)) throw new BaskoulConflictException("ثبت نهایی قبل از وزن دوم امکان‌پذیر نیست.");
        item.FlgSabt = true;
        item.Karbar_Sabt = scope.UserId;
        item.Date_Sabt = DateTime.Now;
        await db.SaveChangesAsync(ct);
        return new WeightCommandResult(item.ID, item.GhabzBaskolID, "نهایی شده", "برگه نهایی شد");
    }

    public async Task<WeightCommandResult> Handle(CancelBargeCommand request, CancellationToken ct)
    {
        var scope = await currentUser.GetScopeAsync(ct);
        var item = await Scoped(request.Id, scope, ct);
        item.FlgEbtal = true;
        item.Karbar_Ebtal = scope.UserId;
        item.Date_Ebtal = DateTime.Now;
        await db.SaveChangesAsync(ct);
        return new WeightCommandResult(item.ID, item.GhabzBaskolID, "باطل شده", "برگه باطل شد");
    }

    private async Task<BargeBaskoul> Scoped(long id, CurrentBaskoulScope scope, CancellationToken ct) =>
        await db.BargeBaskouls.FirstOrDefaultAsync(x => x.ID == id && x.CodMarkaz == scope.CodeMarkaz && x.siteId == scope.SiteId, ct)
        ?? throw new BaskoulNotFoundException("برگه پیدا نشد.");

    private async Task<long?> ResolveDriver(long? id, string code, CancellationToken ct)
    {
        if (!id.HasValue) return null;
        return await db.Mabanis.Where(x => x.ID == id.Value && x.CodMarkaz == code && x.TableName == "Ranande")
            .Select(x => x.IDLinq).FirstOrDefaultAsync(ct);
    }

    private async Task<string> ValidateWeighbridge(long? id, CurrentBaskoulScope scope, CancellationToken ct)
    {
        if (!id.HasValue) throw new BaskoulConflictException("باسکول فعال را انتخاب کنید.");
        return await db.Weighbridges
            .Where(x => x.Id == id.Value && x.CodMarkaz == scope.CodeMarkaz && x.WeighbridgeSiteId == scope.SiteId)
            .Select(x => x.Name)
            .FirstOrDefaultAsync(ct)
            ?? throw new BaskoulConflictException("باسکول انتخاب‌شده متعلق به سایت جاری نیست.");
    }

    private async Task<long> NextReceipt(string code, DateTime now, CancellationToken ct)
    {
        var year = new PersianCalendar().GetYear(now);
        var tracker = await db.GhabzSerialTrackers.FirstOrDefaultAsync(x => x.CodMarkaz == code && x.Year == year, ct);
        if (tracker == null)
        {
            tracker = new GhabzSerialTracker { CodMarkaz = code, Year = year, Serial = 1 };
            db.GhabzSerialTrackers.Add(tracker);
        }
        else tracker.Serial++;
        await db.SaveChangesAsync(ct);
        return long.Parse($"{code}{year % 100:D2}{tracker.Serial:D4}");
    }

    private static string PersianDate(DateTime value)
    {
        var pc = new PersianCalendar();
        return $"{pc.GetYear(value):0000}/{pc.GetMonth(value):00}/{pc.GetDayOfMonth(value):00}";
    }
}
