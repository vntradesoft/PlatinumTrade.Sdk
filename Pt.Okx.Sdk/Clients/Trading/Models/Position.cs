using Pt.Okx.Sdk.Clients.Trading.Enums;
using Pt.Okx.Sdk.Enums;

namespace Pt.Okx.Sdk.Clients.Trading.Models
{
    /// <summary>
    /// Represents a position in the OKX trading system.
    /// </summary>
    public record Position
    {
        /// <summary>The type of the instrument.</summary>
        public InstrumentType InstrumentType { get; set; }
        /// <summary>The margin mode of the position.</summary>
        public MarginMode MarginMode { get; set; }
        /// <summary>The unique identifier of the position.</summary>
        public long? PositionId { get; set; }
        /// <summary>The position side.</summary>
        public PositionSide PositionSide { get; set; }
        /// <summary>The quantity of the positions.</summary>
        public decimal? PositionsQuantity { get; set; }
        /// <summary>The average price of the positions.</summary>
        public decimal? AveragePrice { get; set; }
        /// <summary>The mark price of the positions.</summary>
        public decimal? MarkPrice { get; set; }
        /// <summary>The unrealized profit and loss of the positions.</summary>
        public decimal? UnrealizedProfitAndLoss { get; set; }
        /// <summary>The ratio of the unrealized profit and loss.</summary>
        public decimal? UnrealizedProfitAndLossRatio { get; set; }
        /// <summary>The unrealized Pnl.</summary>
        public decimal? UnrealizedPnl { get; set; }
        /// <summary>The ratio of the unrealized Pnl.</summary>
        public decimal? UnrealizedPnlRatio { get; set; }
        /// <summary>The trading symbol.</summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>The leverage applied to the position.</summary>
        public decimal? Leverage { get; set; }
        /// <summary>The liquidation price of the position.</summary>
        public decimal? LiquidationPrice { get; set; }
        /// <summary>The initial margin requirement for the position.</summary>
        public decimal? InitialMarginRequirement { get; set; }
        /// <summary>The margin used for the position.</summary>
        public decimal? Margin { get; set; }
        /// <summary>The margin ratio for the position.</summary>
        public decimal? MarginRatio { get; set; }
        /// <summary>The maintenance margin requirement for the position.</summary>
        public decimal? MaintenanceMarginRequirement { get; set; }
        /// <summary>The unique identifier for the trade.</summary>
        public long? TradeId { get; set; }
        /// <summary>The notional value of the position in USD.</summary>
        public decimal? NotionalUsd { get; set; }
        /// <summary>The auto-deleveraging indicator.</summary>
        public decimal? AutoDeleveragingIndicator { get; set; }
        /// <summary>The asset for the position.</summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>The last market price for the position.</summary>
        public decimal? LastPrice { get; set; }
        /// <summary>The index price for the position.</summary>
        public decimal? IndexPrice { get; set; }
        /// <summary>The price of the asset in USD.</summary>
        public decimal? UsdPrice { get; set; }
        /// <summary>The break-even price for the position.</summary>
        public decimal? BreakEvenPrice { get; set; }
        /// <summary>The realized Pnl.</summary>
        public decimal? RealizedPnl { get; set; }
        /// <summary>The profit and loss.</summary>
        public decimal? Pnl { get; set; }
        /// <summary>The fee incurred for the position.</summary>
        public decimal? Fee { get; set; }
        /// <summary>The funding fee incurred for the position.</summary>
        public decimal? FundingFee { get; set; }
        /// <summary>The liquidation penalty incurred for the position.</summary>
        public decimal? LiquidationPenalty { get; set; }
        /// <summary>The time when the position was created.</summary>
        public DateTime CreateTime { get; set; }
        /// <summary>The time when the position was last updated.</summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>The algorithmic orders used to close the position.</summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Performance", "CA1819",
            Justification = "DTO property, array is required for serialization")]
        public PositionCloseOrder[] CloseOrderAlgo { get; set; } = Array.Empty<PositionCloseOrder>();

        /// <summary>Initializes a new instance of the <see cref="Position"/> record.</summary>
        public Position() { }

        // ... (rest of methods)
    }

    /// <summary>
    /// Represents an algorithmic order to close a position.
    /// </summary>
    public class PositionCloseOrder
    {
        /// <summary>The algorithmic order identifier.</summary>
        public string AlgoId { get; set; } = string.Empty;
        /// <summary>The stop-loss trigger price.</summary>
        public decimal StopLossTriggerPrice { get; set; }
        /// <summary>The type of stop-loss trigger.</summary>
        public TriggerPriceType StopLossTriggerType { get; set; }
        /// <summary>The take-profit trigger price.</summary>
        public decimal TakeProfitTriggerPrice { get; set; }
        /// <summary>The type of take-profit trigger.</summary>
        public TriggerPriceType TakeProfitTriggerType { get; set; }
        /// <summary>The fraction of the position to close.</summary>
        public decimal? CloseFraction { get; set; }
    }
}
