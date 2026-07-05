using Pt.Okx.Sdk.Drawing.Enums;
using Pt.Okx.Sdk.Drawing.Extensions;
using Pt.Okx.Sdk.Drawing.Models;
using Pt.Okx.Sdk.Enums;
using Pt.Okx.Sdk.Indicators.Enums;

namespace Pt.Okx.Sdk.Drawing.Objects;

/// <summary>
/// Abstract base class for all drawing objects (lines, shapes, text, etc.) on a chart.
/// </summary>
public abstract class DrawingObject
{
    /// <summary>Gets the unique identifier of the drawing object.</summary>
    public string Id { get; init; } = Guid.NewGuid().ToString("N");
    /// <summary>Gets or sets the display name of the drawing object.</summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>Gets or sets a description for the drawing object.</summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>Gets or sets the symbol associated with the drawing object.</summary>
    public string Symbol { get; set; } = string.Empty;
    /// <summary>Gets or sets the timeframe for which the drawing object is visible.</summary>
    public Timeframe Timeframe { get; set; }
    /// <summary>Gets or sets the visual style of the drawing object.</summary>
    public DrawingStyle Style { get; set; } = new();
    /// <summary>Gets or sets a value indicating whether the drawing object is visible.</summary>
    public bool IsVisible { get; set; } = true;
    /// <summary>Gets or sets a value indicating whether the drawing object is locked from editing.</summary>
    public bool IsLocked { get; set; }
    /// <summary>Gets or sets the ID of the indicator this drawing is associated with, if any.</summary>
    public string? TargetIndicatorId { get; set; }
    /// <summary>Gets or sets the source of the drawing (e.g., User or Strategy).</summary>
    public DrawingSource Source { get; set; } = DrawingSource.User;
    /// <summary>Gets or sets the timeframes during which this object is visible.</summary>
    public TimeFrameOptions VisibleTimeframes { get; set; } = TimeFrameOptions.AllPeriods;
    /// <summary>Gets the type of the drawing object.</summary>
    public abstract DrawingObjectType Type { get; }

    /// <summary>Gets the string representation of the drawing object type.</summary>
    public string GetTypeName() => Type.GetDescription();

    /// <summary>Moves the drawing object by the specified price and time deltas.</summary>
    /// <param name="priceDelta">The amount to change the price by.</param>
    /// <param name="timeDelta">The amount of time to shift the object by.</param>
    public virtual void Move(decimal priceDelta, TimeSpan timeDelta) { }

    /// <summary>Helper method to move an anchor point.</summary>
    protected static DrawingAnchor MoveAnchor(DrawingAnchor anchor, decimal priceDelta, TimeSpan timeDelta)
    {
        return new DrawingAnchor
        {
            Price = anchor.Price + priceDelta,
            Time = anchor.Time + timeDelta
        };
    }
}


