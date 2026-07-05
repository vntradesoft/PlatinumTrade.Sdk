namespace Pt.Okx.Sdk.Indicators.Enums
{
    /// <summary>
    /// Indicator category used for grouping in UI and registration metadata.
    /// </summary>
    public enum IndicatorGroup
    {
        /// <summary>Time-series indicator.</summary>
        TimeSeries,

        /// <summary>Trend indicator.</summary>
        Trend,

        /// <summary>Oscillator indicator.</summary>
        Oscillator,

        /// <summary>Volume indicator.</summary>
        Volume,

        /// <summary>Bill Williams indicator.</summary>
        BillWilliams,

        /// <summary>Deprecated misspelling of <see cref="BillWilliams"/>.</summary>
        [Obsolete("Use BillWilliams.")]
        BillWilliam = BillWilliams,

        /// <summary>Custom indicator group.</summary>
        Custom,

        /// <summary>Unknown indicator group.</summary>
        Unknown
    }
}
