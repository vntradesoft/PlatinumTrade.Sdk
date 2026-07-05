namespace Pt.Okx.Sdk.Enums
{
    /// <summary>
    /// OKX instrument types.
    /// </summary>
    public enum InstrumentType
    {
        /// <summary>Any supported instrument type.</summary>
        Any,

        /// <summary>Spot instrument.</summary>
        Spot,

        /// <summary>Margin instrument.</summary>
        Margin,

        /// <summary>Perpetual swap instrument.</summary>
        Swap,

        /// <summary>Futures instrument.</summary>
        Futures,

        /// <summary>Option instrument.</summary>
        Option,

        /// <summary>Generic contract instrument.</summary>
        Contract
    }
}
