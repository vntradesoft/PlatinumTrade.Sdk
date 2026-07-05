namespace Pt.Okx.Sdk.Strategy.Settings.Enums
{
    /// <summary>
    /// Preset date ranges used by backtests and data queries.
    /// </summary>
    public enum DateOption
    {
        /// <summary>Use the configured entry date.</summary>
        Entry,

        /// <summary>Use the last year.</summary>
        LastYear,

        /// <summary>Use the last three months.</summary>
        ThreeMonth,

        /// <summary>Use the last six months.</summary>
        SixMonth,

        /// <summary>Use the last month.</summary>
        LastMonth,

        /// <summary>Use a custom date range.</summary>
        Custom
    }
}
