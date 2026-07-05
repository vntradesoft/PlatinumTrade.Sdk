using Pt.Okx.Sdk.Drawing.Enums;
using Pt.Okx.Sdk.Drawing.Models;

namespace Pt.Okx.Sdk.Drawing.Objects;

/// <summary>
/// Represents a trend line drawing object.
/// </summary>
public class TrendLineObject : DrawingObject
{
    /// <inheritdoc/>
    public override DrawingObjectType Type => DrawingObjectType.TrendLine;

    /// <summary>Gets or sets the start anchor point.</summary>
    public DrawingAnchor Start { get; set; }

    /// <summary>Gets or sets the end anchor point.</summary>
    public DrawingAnchor End { get; set; }

    /// <summary>Gets or sets a value indicating whether to extend the line to the right.</summary>
    public bool ExtendRight { get; set; }

    /// <summary>Gets or sets a value indicating whether to extend the line to the left.</summary>
    public bool ExtendLeft { get; set; }

    /// <inheritdoc/>
    public override void Move(decimal priceDelta, TimeSpan timeDelta)
    {
        Start = MoveAnchor(Start, priceDelta, timeDelta);
        End = MoveAnchor(End, priceDelta, timeDelta);
    }
}
