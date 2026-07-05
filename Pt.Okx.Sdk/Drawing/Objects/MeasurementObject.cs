using Pt.Okx.Sdk.Drawing.Enums;
using Pt.Okx.Sdk.Drawing.Models;

namespace Pt.Okx.Sdk.Drawing.Objects;

/// <summary>
/// Represents a measurement drawing object.
/// </summary>
public class MeasurementObject : DrawingObject
{
    /// <inheritdoc/>
    public override DrawingObjectType Type => DrawingObjectType.Measurement;

    /// <summary>Gets or sets the start anchor point.</summary>
    public DrawingAnchor Start { get; set; }

    /// <summary>Gets or sets the end anchor point.</summary>
    public DrawingAnchor End { get; set; }

    /// <summary>Gets the price difference between the end and start points.</summary>
    public decimal PriceDelta => End.Price - Start.Price;

    /// <summary>Gets the price percentage change between the end and start points.</summary>
    public double PricePercent => (double)(PriceDelta / Start.Price * 100);

    /// <summary>Gets the time duration between the end and start points.</summary>
    public TimeSpan TimeDelta => End.Time - Start.Time;

    /// <inheritdoc/>
    public override void Move(decimal priceDelta, TimeSpan timeDelta)
    {
        Start = MoveAnchor(Start, priceDelta, timeDelta);
        End = MoveAnchor(End, priceDelta, timeDelta);
    }
}
