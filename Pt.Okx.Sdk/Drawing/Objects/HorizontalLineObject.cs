using Pt.Okx.Sdk.Drawing.Enums;

namespace Pt.Okx.Sdk.Drawing.Objects;

/// <summary>
/// Represents a horizontal line drawing object.
/// </summary>
public class HorizontalLineObject : DrawingObject
{
    /// <inheritdoc/>
    public override DrawingObjectType Type => DrawingObjectType.HorizontalLine;

    /// <summary>Gets or sets the price level of the horizontal line.</summary>
    public decimal Price { get; set; }

    /// <summary>Gets or sets a value indicating whether to extend the line to the left.</summary>
    public bool ExtendLeft { get; set; } = true;

    /// <summary>Gets or sets a value indicating whether to extend the line to the right.</summary>
    public bool ExtendRight { get; set; } = true;

    /// <inheritdoc/>
    public override void Move(decimal priceDelta, TimeSpan timeDelta)
    {
        Price += priceDelta;
    }
}
