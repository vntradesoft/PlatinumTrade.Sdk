using Pt.Okx.Sdk.Enums;

namespace Pt.Okx.Sdk.Clients.Trading.Models;

/// <summary>
/// Represents the response received after attempting to close a position in the OKX trading system.
/// </summary>
public record ClosePositionResponse
{
    /// <summary>The trading symbol (e.g., BTC-USDT).</summary>
    public string Symbol { get; set; } = string.Empty;

    /// <summary>The side of the position to close (e.g., Long/Short).</summary>
    public PositionSide PositionSide { get; set; }

    /// <summary>The client-defined unique identifier for the order.</summary>
    public string? ClientOrderId { get; set; }

    /// <summary>A custom tag associated with the close position request.</summary>
    public string? Tag { get; set; }
}
