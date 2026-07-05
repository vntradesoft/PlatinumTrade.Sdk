namespace Pt.Okx.Sdk.Indicators.Enums
{
    /// <summary>
    /// Direction classification returned by moving-average helper methods.
    /// </summary>
    public enum MATrendDirection
    {
        /// <summary>The trend direction is not known.</summary>
        Unknown,

        /// <summary>The moving average is trending upward.</summary>
        Upward,

        /// <summary>The moving average is trending downward.</summary>
        Downward,

        /// <summary>The moving average is moving sideways.</summary>
        Sideways
    }
}
