using System.ComponentModel;

namespace Pt.Okx.Sdk.Drawing.Enums;

/// <summary>
/// Defines the types of drawing objects that can be placed on a chart.
/// </summary>
public enum DrawingObjectType
{
    /// <summary>
    /// Horizontal line drawn across the chart.
    /// </summary>
    [Description("Horizontal Line")]
    HorizontalLine,

    /// <summary>
    /// Vertical line drawn across the chart.
    /// </summary>
    [Description("Vertical Line")]
    VerticalLine,

    /// <summary>
    /// Line drawn between two points to indicate a trend.
    /// </summary>
    [Description("Trend Line")]
    TrendLine,

    /// <summary>
    /// Rectangle shape used for highlighting areas.
    /// </summary>
    [Description("Rectangle")]
    Rectangle,

    /// <summary>
    /// Ellipse (oval) shape.
    /// </summary>
    [Description("Ellipse")]
    Ellipse,

    /// <summary>
    /// Triangle shape defined by three points.
    /// </summary>
    [Description("Triangle")]
    Triangle,

    /// <summary>
    /// Fibonacci retracement tool for identifying support and resistance levels.
    /// </summary>
    [Description("Fibonacci Retracement")]
    FibRetracement,

    /// <summary>
    /// Fibonacci extension tool for projecting price targets.
    /// </summary>
    [Description("Fibonacci Extension")]
    FibExtension,

    /// <summary>
    /// Measurement tool for price and time distance.
    /// </summary>
    [Description("Measurement")]
    Measurement,

    /// <summary>
    /// Text annotation.
    /// </summary>
    [Description("Text")]
    Text,

    /// <summary>
    /// Arrow marker.
    /// </summary>
    [Description("Arrow")]
    Arrow,

    /// <summary>
    /// Emoji or icon marker.
    /// </summary>
    [Description("Emoji")]
    Emoji
}
