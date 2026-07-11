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

        return new BargeDto(
            item.ID,
            item.GhabzBaskolID,
            item.ShomareMashin ?? string.Empty,
            item.IDRanande,
            driverName,
            entry,
            exit,
            hasTwo ? Math.Abs(item.VaznPor!.Value - item.VanKhali!.Value) : null,
            type,
            status,
            item.Tozihat);
    }
}
