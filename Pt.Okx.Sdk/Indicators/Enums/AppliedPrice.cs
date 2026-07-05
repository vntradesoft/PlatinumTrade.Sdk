using System.ComponentModel;

namespace Pt.Okx.Sdk.Indicators.Enums
{
    /// <summary>
    /// Price field used as indicator input.
    /// </summary>
    public enum AppliedPrice
    {
        /// <summary>Close price.</summary>
        [Description("Close")]
        Close,

        /// <summary>Open price.</summary>
        [Description("Open")]
        Open,

        /// <summary>High price.</summary>
        [Description("High")]
        High,

        /// <summary>Low price.</summary>
        [Description("Low")]
        Low,

        /// <summary>Median price, typically (High + Low) / 2.</summary>
        [Description("Median")]
        Median,

        /// <summary>Typical price, typically (High + Low + Close) / 3.</summary>
        [Description("Typical")]
        Typical,

        /// <summary>Weighted price, typically (High + Low + Close + Close) / 4.</summary>
        [Description("Weighted")]
        Weighted
    }
}
