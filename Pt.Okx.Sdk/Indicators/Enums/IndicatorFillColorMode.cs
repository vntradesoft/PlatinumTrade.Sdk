using Pt.Okx.Sdk.Indicators.Models;

namespace Pt.Okx.Sdk.Indicators.Enums
{
    /// <summary>
    /// Specifies how the fill color of an <see cref="IndicatorFillRegion"/> is determined.
    /// </summary>
    public enum IndicatorFillColorMode
    {
        /// <summary>
        /// Always uses a single fixed color, regardless of buffer values.
        /// </summary>
        Fixed,

        /// <summary>
        /// Selects the color based on the relationship between buffer values.
        /// Uses <see cref="IndicatorFillRegion.BullishColor"/> when the upper buffer
        /// value is greater than or equal to the lower buffer value; otherwise uses
        /// <see cref="IndicatorFillRegion.BearishColor"/>.
        /// </summary>
        BullishBearish
    }
}
