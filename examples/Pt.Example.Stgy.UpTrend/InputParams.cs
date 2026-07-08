using Pt.Okx.Sdk.Strategy.Parameters;

namespace Pt.Example.Stgy.UpTrend;

internal sealed class InputParams
{
    [InputParam(Section = 1, SectionTitle = "Signal", Order = 1, Min = 1, Max = 100, Description = "SuperTrend ATR period.")]
    public int SuperTrendPeriod { get; set; } = 14;

    [InputParam(Section = 1, Order = 2, Min = 0.1, Max = 10, Description = "SuperTrend ATR multiplier.")]
    public double SuperTrendMultiplier { get; set; } = 3.0;

    [InputParam(Section = 2, SectionTitle = "Trading", Order = 1, Description = "Optional symbol override. Leave empty to use StrategySettings.Symbol.")]
    public string Symbol { get; set; } = string.Empty;

    [InputParam(Section = 2, Order = 2, Min = 0.0001, Max = 1000000, Description = "Market order quantity for entry.")]
    public decimal OrderQuantity { get; set; } = 0.01m;
}
