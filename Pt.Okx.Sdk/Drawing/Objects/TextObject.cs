using Pt.Okx.Sdk.Drawing.Enums;
using Pt.Okx.Sdk.Drawing.Models;

namespace Pt.Okx.Sdk.Drawing.Objects;

/// <summary>
/// Represents a text drawing object.
/// </summary>
public class TextObject : DrawingObject
{
    /// <inheritdoc/>
    public override DrawingObjectType Type => DrawingObjectType.Text;

    /// <summary>Gets or sets the anchor point for the text.</summary>
    public DrawingAnchor Anchor { get; set; }

    /// <summary>Gets or sets the font size of the text.</summary>
    public double FontSize { get; set; } = 12;

    /// <summary>Gets or sets the font family of the text.</summary>
    public string FontFamily { get; set; } = "Segoe UI";

    /// <inheritdoc/>
    public override void Move(decimal priceDelta, TimeSpan timeDelta)
    {
        Anchor = MoveAnchor(Anchor, priceDelta, timeDelta);
    }
}
