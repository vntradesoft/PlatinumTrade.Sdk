using Pt.Okx.Sdk.Drawing.Enums;

namespace Pt.Okx.Sdk.Drawing.Models;

/// <summary>
/// Defines the styling properties for a drawing object.
/// </summary>
public class DrawingStyle
{
    /// <summary>Gets or sets the stroke color in hex format (e.g., "#FFFFFF").</summary>
    public string Color { get; set; } = "#FFFFFF";
    /// <summary>Gets or sets the stroke width.</summary>
    public double Width { get; set; } = 1.0;
    /// <summary>Gets or sets the line style.</summary>
    public DrawingLineStyle LineStyle { get; set; } = DrawingLineStyle.Solid;
    /// <summary>Gets or sets the opacity (0.0 to 1.0).</summary>
    public double Opacity { get; set; } = 1.0;
    /// <summary>Gets or sets a value indicating whether to fill the shape.</summary>
    public bool Fill { get; set; }
    /// <summary>Gets or sets the fill color in hex format (e.g., "#44FFFFFF").</summary>
    public string FillColor { get; set; } = "#44FFFFFF";
}
