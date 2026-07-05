using System.ComponentModel;

namespace Pt.Okx.Sdk.Indicators.Enums
{
    /// <summary>
    /// Moving average calculation method.
    /// </summary>
    public enum MaMethod
    {
        /// <summary>Simple Moving Average.</summary>
        [Description("SMA")]
        SMA,

        /// <summary>Exponential Moving Average.</summary>
        [Description("EMA")]
        EMA,

        /// <summary>Smoothed Moving Average.</summary>
        [Description("SMMA")]
        SMMA,

        /// <summary>Linear Weighted Moving Average.</summary>
        [Description("LWWMA")]
        LWWMA
    }
}
