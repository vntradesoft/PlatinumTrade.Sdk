namespace Pt.Okx.Sdk.Indicators.Enums
{
    /// <summary>
    /// Drawing primitives supported by indicator outputs.
    /// </summary>
    public enum IndicatorDrawType
    {
        /// <summary>Continuous line.</summary>
        Line = 0,

        /// <summary>Histogram bars.</summary>
        Histogram = 1,

        /// <summary>Point markers.</summary>
        Dots = 2,

        /// <summary>Arrow markers.</summary>
        Arrow = 3,

        /// <summary>Vertical bars, such as OHLC or volume bars.</summary>
        Bar = 4,

        /// <summary>Filled area under a line.</summary>
        Area = 5,

        /// <summary>Scatter plot points.</summary>
        Scatter = 6,

        /// <summary>Pie chart segments.</summary>
        Pie = 7,

        /// <summary>Candlestick rendering.</summary>
        Candle = 8,

        /// <summary>Custom icon rendering.</summary>
        Icon = 9,

        /// <summary>Custom geometric shape rendering.</summary>
        Shape = 10,

        /// <summary>Text labels on the chart.</summary>
        Text = 11,

        /// <summary>Custom rendering handled by the host application.</summary>
        Custom = 100,
    }
}
