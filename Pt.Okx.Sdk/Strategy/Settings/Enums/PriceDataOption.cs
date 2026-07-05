using System.ComponentModel;

namespace Pt.Okx.Sdk.Strategy.Settings.Enums
{
    /// <summary>
    /// Price data granularity used by simulation and replay.
    /// </summary>
    public enum PriceDataOption
    {
        /// <summary>Use every tick.</summary>
        [Description("EveryTick")]
        EveryTick,

        /// <summary>Use one-minute OHLC candles.</summary>
        [Description("1 minute OHLC")]
        OneMinuteOHLC
    }
}
