namespace Pt.Okx.Sdk.Indicators.Enums
{
    /// <summary>
    /// Specifies the calculation and state management behavior of an indicator.
    /// Controls history warm-up, sequential vs parallel calculation strategy, and buffer reset policies.
    /// </summary>
    public enum IndicatorCalculationMode
    {
        /// <summary>
        /// Stateless indicator. Each bar's value depends strictly on a fixed window of recent bars (e.g. Simple Moving Average, Donchian Channel).
        /// Can be calculated independently without deep historical warm-up.
        /// </summary>
        Stateless = 0,

        /// <summary>
        /// Stateful or recursive indicator. Each bar's value depends on previous calculated states (e.g. SuperTrend, RSI, Wilder's ATR, Parabolic SAR, Ichimoku).
        /// Requires sequential calculation, historical warm-up bars, and buffer resets upon timeframe switches.
        /// </summary>
        Stateful = 1,

        /// <summary>
        /// Cumulative or session-based indicator. Accumulates state over trading sessions and resets at session boundaries (e.g. VWAP, Anchored Volume Profile).
        /// </summary>
        Cumulative = 2
    }
}
