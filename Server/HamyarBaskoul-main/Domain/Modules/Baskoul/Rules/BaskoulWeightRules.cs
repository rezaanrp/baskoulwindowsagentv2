using Domain.Models;

namespace Domain.Rules;

public static class BaskoulWeightRules
{
    public static bool IsPositive(float? weight) => weight.HasValue && weight.Value > 0;

    public static bool HasOneWeight(float? fullWeight, float? emptyWeight) =>
        IsPositive(fullWeight) != IsPositive(emptyWeight);

    public static bool HasTwoWeights(float? fullWeight, float? emptyWeight) =>
        IsPositive(fullWeight) && IsPositive(emptyWeight);

    public static bool IsIncomplete(BargeBaskoul barge) =>
        barge.FlgSabt != true && barge.FlgEbtal != true && HasOneWeight(barge.VaznPor, barge.VanKhali);

    public static (float Full, float Empty, float Net, int? Type) Calculate(float current, float previous)
    {
        if (current <= 0) throw new ArgumentOutOfRangeException(nameof(current));
        if (previous <= 0) return (current, 0, 0, null);

        var full = Math.Max(current, previous);
        var empty = Math.Min(current, previous);
        int? type = current == previous ? null : current < previous ? 1 : 2;
        return (full, empty, Math.Abs(full - empty), type);
    }
}
