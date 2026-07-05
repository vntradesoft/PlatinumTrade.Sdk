namespace Pt.Okx.Sdk.Indicators.Enums
{
    /// <summary>
    /// Bit flags for the timeframes where an indicator is available.
    /// </summary>
    [Flags]
    public enum TimeFrameOptions : int
    {
        /// <summary>All supported periods.</summary>
        AllPeriods =
                    SixMonths |
                    ThreeMonths |
                    OneMonth |
                    OneWeek |
                    OneDay |
                    TwelveHours |
                    SixHours |
                    FourHours |
                    TwoHours |
                    OneHour |
                    ThirtyMinutes |
                    FifteenMinutes |
                    FiveMinutes |
                    ThreeMinutes |
                    OneMinute,

        /// <summary>No periods are enabled.</summary>
        None = 0x0,

        /// <summary>Six-month period.</summary>
        SixMonths = 1 << 0,

        /// <summary>Three-month period.</summary>
        ThreeMonths = 1 << 1,

        /// <summary>One-month period.</summary>
        OneMonth = 1 << 2,

        /// <summary>One-week period.</summary>
        OneWeek = 1 << 3,

        /// <summary>One-day period.</summary>
        OneDay = 1 << 4,

        /// <summary>Twelve-hour period.</summary>
        TwelveHours = 1 << 5,

        /// <summary>Six-hour period.</summary>
        SixHours = 1 << 6,

        /// <summary>Four-hour period.</summary>
        FourHours = 1 << 7,

        /// <summary>Two-hour period.</summary>
        TwoHours = 1 << 8,

        /// <summary>One-hour period.</summary>
        OneHour = 1 << 9,

        /// <summary>Thirty-minute period.</summary>
        ThirtyMinutes = 1 << 10,

        /// <summary>Fifteen-minute period.</summary>
        FifteenMinutes = 1 << 11,

        /// <summary>Five-minute period.</summary>
        FiveMinutes = 1 << 12,

        /// <summary>Three-minute period.</summary>
        ThreeMinutes = 1 << 13,

        /// <summary>One-minute period.</summary>
        OneMinute = 1 << 14,
    }
}
