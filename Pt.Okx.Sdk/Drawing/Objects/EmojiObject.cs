using Pt.Okx.Sdk.Drawing.Enums;
using Pt.Okx.Sdk.Drawing.Models;

namespace Pt.Okx.Sdk.Drawing.Objects;

/// <summary>
/// Represents an emoji drawing object.
/// </summary>
public class EmojiObject : DrawingObject
{
    /// <inheritdoc/>
    public override DrawingObjectType Type => DrawingObjectType.Emoji;

    /// <summary>Gets or sets the anchor point for the emoji.</summary>
    public DrawingAnchor Anchor { get; set; }

    /// <summary>Gets or sets the emoji character.</summary>
    public string Emoji { get; set; } = string.Empty;

    /// <summary>Gets or sets the font size of the emoji.</summary>
    public double FontSize { get; set; } = 24;

    /// <inheritdoc/>
    public override void Move(decimal priceDelta, TimeSpan timeDelta)
    {
        Anchor = MoveAnchor(Anchor, priceDelta, timeDelta);
    }
}
