namespace Pt.Okx.Sdk.Indicators.Enums
{
    /// <summary>
    /// Type of data stored by an indicator buffer.
    /// </summary>
    public enum IndicatorBufferType
    {
        /// <summary>Main data buffer used for rendering or public output.</summary>
        Data,

        /// <summary>Buffer that stores per-point color indexes.</summary>
        ColorIndex,

        /// <summary>Internal calculation buffer that is not rendered directly.</summary>
        Calculations
    }
}
