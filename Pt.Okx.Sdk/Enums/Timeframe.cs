using System.ComponentModel;

namespace Pt.Okx.Sdk.Enums
{
    /// <summary>
    /// Represents supported candlestick timeframes in seconds.
    /// The underlying integer value corresponds to the duration in seconds.
    /// </summary>
    /// <remarks>
    /// This enum is designed for both:
    /// <list type="bullet">
    /// <item>Time-based calculations (using numeric seconds).</item>
    /// <item>External API mapping (via <see cref="DescriptionAttribute"/> values such as "1m", "1h").</item>
    /// </list>
    /// </remarks>
    public enum Timeframe
    {
        /// <summary>Unknown or unspecified timeframe.</summary>
        [Description("unknown")]
        Unknown = 0,

        /// <summary>One second (1 second).</summary>
        [Description("1s")]
        OneSecond = 1,

        /// <summary>One minute (60 seconds).</summary>
        [Description("1m")]
        OneMinute = 60,

        /// <summary>Three minutes (180 seconds).</summary>
        [Description("3m")]
        ThreeMinutes = 180,

        /// <summary>Five minutes (300 seconds).</summary>
        [Description("5m")]
        FiveMinutes = 300,

        /// <summary>Fifteen minutes (900 seconds).</summary>
        [Description("15m")]
        FifteenMinutes = 900,

        /// <summary>Thirty minutes (1800 seconds).</summary>
        [Description("30m")]
        ThirtyMinutes = 1800,

        /// <summary>One hour (3600 seconds).</summary>
        [Description("1h")]
        OneHour = 3600,

        /// <summary>Two hours (7200 seconds).</summary>
        [Description("2h")]
        TwoHours = 7200,

        /// <summary>Four hours (14400 seconds).</summary>
        [Description("4h")]
        FourHours = 14400,

        /// <summary>Six hours (21600 seconds).</summary>
        [Description("6h")]
        SixHours = 21600,

        /// <summary>Twelve hours (43200 seconds).</summary>
        [Description("12h")]
        TwelveHours = 43200,

        /// <summary>One day (86400 seconds).</summary>
        [Description("1d")]
        OneDay = 86400,

        /// <summary>One week (604800 seconds).</summary>
        [Description("1w")]
        OneWeek = 604800,

        /// <summary>
        /// One month (approximated as 30 days = 2592000 seconds).
        /// </summary>
        [Description("1M")]
        OneMonth = 2592000,

        /// <summary>
        /// Three months (approximated as 90 days).
        /// </summary>
        [Description("3M")]
        ThreeMonths = 7776000,

        /// <summary>
        /// Six months (approximated as 180 days).
        /// </summary>
        [Description("6M")]
        SixMonths = 15552000,

        /// <summary>
        /// One year (approximated as 365 days).
        /// </summary>
        [Description("1y")]
        OneYear = 31536000
    }
}
