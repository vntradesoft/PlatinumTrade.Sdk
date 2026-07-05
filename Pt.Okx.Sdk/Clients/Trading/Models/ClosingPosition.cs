using Pt.Okx.Sdk.Clients.Trading.Enums;
using Pt.Okx.Sdk.Enums;

namespace Pt.Okx.Sdk.Clients.Trading.Models
{
    /// <summary>
    /// Represents a closed position in the OKX trading system.
    /// </summary>
    public record ClosingPosition
    {
        /// <summary>The type of the instrument.</summary>
        public InstrumentType InstrumentType { get; set; }
        /// <summary>The trading symbol (e.g., BTC-USDT).</summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>The margin mode of the position.</summary>
        public MarginMode MarginMode { get; set; }
        /// <summary>The type of closing position.</summary>
        public ClosingPositionType Type { get; set; }
        /// <summary>The time when the position was created.</summary>
        public DateTime CreateTime { get; set; }
        /// <summary>The time when the position was last updated.</summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>The average opening price of the position.</summary>
        public decimal? OpenAveragePrice { get; set; }
        /// <summary>The average closing price of the position.</summary>
        public decimal? CloseAveragePrice { get; set; }
        /// <summary>The unique identifier of the position.</summary>
        public long? PositionId { get; set; }
        /// <summary>The maximum position quantity opened.</summary>
        public decimal? OpenMaxPos { get; set; }
        /// <summary>The total quantity closed for the position.</summary>
        public decimal? CloseTotalPos { get; set; }
        /// <summary>The realized profit and loss of the position.</summary>
        public decimal? RealizedPnl { get; set; }
        /// <summary>The profit and loss ratio.</summary>
        public decimal? PnlRatio { get; set; }
        /// <summary>The fee incurred for the position.</summary>
        public decimal? Fee { get; set; }
        /// <summary>The funding fee incurred.</summary>
        public decimal? FundingFee { get; set; }
        /// <summary>The liquidation penalty incurred.</summary>
        public decimal? LiquidationPenalty { get; set; }
        /// <summary>The overall profit and loss of the position.</summary>
        public decimal? ProfitAndLoss { get; set; }
        /// <summary>The position side.</summary>
        public PositionSide PositionSide { get; set; }
        /// <summary>The leverage applied to the position.</summary>
        public decimal? Leverage { get; set; }
        /// <summary>The direction of the position.</summary>
        public PositionSide Direction { get; set; }
        /// <summary>The trigger mark price for the position.</summary>
        public decimal? TriggerMarkPrice { get; set; }
        /// <summary>The underlying asset of the position.</summary>
        public string Underlying { get; set; } = string.Empty;
        /// <summary>The asset used for the position.</summary>
        public string? Asset { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ClosingPosition"/> record.</summary>
        public ClosingPosition() { }
    }
}
