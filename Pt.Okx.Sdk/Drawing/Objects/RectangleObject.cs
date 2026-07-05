using Pt.Okx.Sdk.Drawing.Enums;
using Pt.Okx.Sdk.Drawing.Models;

namespace Pt.Okx.Sdk.Drawing.Objects;

/// <summary>
/// Represents a rectangle drawing object.
/// </summary>
public class RectangleObject : DrawingObject
{
    /// <inheritdoc/>
    public override DrawingObjectType Type => DrawingObjectType.Rectangle;

    /// <summary>Gets or sets the top-left anchor point.</summary>
    public DrawingAnchor TopLeft { get; set; }

    /// <summary>Gets or sets the bottom-right anchor point.</summary>
    public DrawingAnchor BottomRight { get; set; }

    /// <inheritdoc/>
    public override void Move(decimal priceDelta, TimeSpan timeDelta)
    {
        TopLeft = MoveAnchor(TopLeft, priceDelta, timeDelta);
        BottomRight = MoveAnchor(BottomRight, priceDelta, timeDelta);
    }
}
