namespace Pt.Okx.Sdk.Drawing.Models;

/// <summary>
/// Represents an anchor point for a drawing object, defined by time and price coordinates.
/// </summary>
/// <param name="Time">
/// The time coordinate of the anchor point.
/// </param>
/// <param name="Price">
/// The price coordinate of the anchor point.
/// </param>
public readonly record struct DrawingAnchor(DateTime Time, decimal Price);
