using Pt.Okx.Sdk.Drawing.Enums;

namespace Pt.Okx.Sdk.Drawing.Objects;

/// <summary>
/// Represents a vertical line drawing object.
/// </summary>
public class VerticalLineObject : DrawingObject
{
    /// <inheritdoc/>
    public override DrawingObjectType Type => DrawingObjectType.VerticalLine;

    /// <summary>Gets or sets the time of the vertical line.</summary>
    public DateTime Time { get; set; }

    /// <inheritdoc/>
    public override void Move(decimal priceDelta, TimeSpan timeDelta)
    {
        Time += timeDelta;
    }
}
