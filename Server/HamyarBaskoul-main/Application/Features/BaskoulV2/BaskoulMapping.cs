using Domain.Models;
using Domain.Rules;

namespace Application.Features.BaskoulV2;

internal static class BaskoulMapping
{
    public static BargeDto ToDto(BargeBaskoul item, string driverName)
    {
        var hasTwo = BaskoulWeightRules.HasTwoWeights(item.VaznPor, item.VanKhali);
        var hasOne = BaskoulWeightRules.HasOneWeight(item.VaznPor, item.VanKhali);
        float? entry = null;
        float? exit = null;

        if (hasOne)
        {
            entry = BaskoulWeightRules.IsPositive(item.VaznPor) ? item.VaznPor : item.VanKhali;
        }
        else if (hasTwo)
        {
            entry = item.TypeBarge == 2 ? item.VanKhali : item.VaznPor;
            exit = item.TypeBarge == 2 ? item.VaznPor : item.VanKhali;
        }

        var type = hasOne ? "در انتظار وزن دوم"
            : !hasTwo || entry == exit ? "نامشخص"
            : entry > exit ? "ورود" : "خروج";
        var status = item.FlgEbtal == true ? "باطل شده"
            : item.FlgSabt == true ? "نهایی شده"
            : hasTwo ? "تکمیل شده"
            : hasOne ? "در حال توزین" : "نامشخص";
        var wasSent = item.IDWebBarge.HasValue && item.DateInsToWeb.HasValue;
        var needsUpdate = item.Date_Up.HasValue &&
            (!item.DateUpToWeb.HasValue || item.Date_Up.Value > item.DateUpToWeb.Value);
        var syncStatus = item.FlgEbtal == true
            ? !wasSent ? "ارسال نشده" : needsUpdate ? "ابطال در انتظار ارسال" : "ابطال ارسال شد"
            : !wasSent ? item.FlgSabt == true ? "در انتظار ارسال" : "آماده ارسال نیست"
            : needsUpdate ? "در انتظار همگام‌سازی" : "ارسال شده";

        return new BargeDto(
            item.ID,
            item.GhabzBaskolID,
            item.DateBarge,
            item.TimeBarge,
            item.ShomareMashin ?? string.Empty,
            item.IDRanande,
            driverName,
            entry,
            exit,
            hasTwo ? Math.Abs(item.VaznPor!.Value - item.VanKhali!.Value) : null,
            item.IDBaskul,
            type,
            status,
            syncStatus,
            item.Tozihat);
    }
}
