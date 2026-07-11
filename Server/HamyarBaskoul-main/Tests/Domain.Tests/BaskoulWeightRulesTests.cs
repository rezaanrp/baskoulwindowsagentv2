using Domain.Models;
using Domain.Rules;
using Xunit;
using System;

namespace Domain.Tests;

public sealed class BaskoulWeightRulesTests
{
    [Fact]
    public void One_positive_weight_is_incomplete()
    {
        var barge = new BargeBaskoul { VaznPor = 12500, VanKhali = 0 };
        Assert.True(BaskoulWeightRules.IsIncomplete(barge));
    }

    [Fact]
    public void Final_or_cancelled_barge_is_not_incomplete()
    {
        Assert.False(BaskoulWeightRules.IsIncomplete(new BargeBaskoul { VaznPor = 12500, VanKhali = 0, FlgSabt = true }));
        Assert.False(BaskoulWeightRules.IsIncomplete(new BargeBaskoul { VaznPor = 12500, VanKhali = 0, FlgEbtal = true }));
    }

    [Theory]
    [InlineData(8000, 12000, 12000, 8000, 1)]
    [InlineData(12000, 8000, 12000, 8000, 2)]
    public void Calculate_orders_weights_and_infers_type(float current, float previous, float full, float empty, int type)
    {
        var result = BaskoulWeightRules.Calculate(current, previous);
        Assert.Equal(full, result.Full);
        Assert.Equal(empty, result.Empty);
        Assert.Equal(Math.Abs(full - empty), result.Net);
        Assert.Equal(type, result.Type);
    }

    [Fact]
    public void First_weight_has_no_type()
    {
        var result = BaskoulWeightRules.Calculate(9000, 0);
        Assert.Equal(9000, result.Full);
        Assert.Equal(0, result.Empty);
        Assert.Null(result.Type);
    }
}
