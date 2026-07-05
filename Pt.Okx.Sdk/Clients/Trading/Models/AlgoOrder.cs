using Pt.Okx.Sdk.Clients.Trading.Enums;
using Pt.Okx.Sdk.Enums;

namespace Pt.Okx.Sdk.Clients.Trading.Models
{
    /// <summary>
    /// Represents an algorithmic order in the OKX trading system.
    /// </summary>
    public record AlgoOrder
    {
        /// <summary>The type of the instrument.</summary>
        public InstrumentType InstrumentType { get; set; }
        /// <summary>The trading symbol (e.g., BTC-USDT).</summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>The asset or currency base for the order.</summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>The unique identifier for the order, if available.</summary>
        public long? OrderId { get; set; }
        /// <summary>The list of associated order identifiers.</summary>
#pragma warning disable CA1819
        public long[]? OrderIdList { get; set; }
#pragma warning disable CA1819
        /// <summary>The unique identifier for the algorithmic order.</summary>
        public string? AlgoId { get; set; }
        /// <summary>The client-defined unique identifier for the order.</summary>
        public string? ClientOrderId { get; set; }
        /// <summary>The quantity of the asset to be traded.</summary>
        public decimal? Quantity { get; set; }
        /// <summary>The fraction of the position to be closed.</summary>
        public decimal? CloseFraction { get; set; }
        /// <summary>The type of the algorithmic order.</summary>
        public AlgoOrderType OrderType { get; set; }
        /// <summary>The side of the order (Buy/Sell).</summary>
        public OrderSide OrderSide { get; set; }
        /// <summary>The position side (e.g., Long/Short).</summary>
        public PositionSide? PositionSide { get; set; }
        /// <summary>The trade mode (e.g., Cash/Margin).</summary>
        public TradeMode TradeMode { get; set; }
        /// <summary>The current state of the algorithmic order.</summary>
        public AlgoOrderState State { get; set; }
        /// <summary>The leverage to be applied to the order.</summary>
        public decimal? Leverage { get; set; }
        /// <summary>The take-profit trigger price.</summary>
        public decimal? TakeProfitTriggerPrice { get; set; }
        /// <summary>The type of take-profit trigger price.</summary>
        public TriggerPriceType? TakeProfitTriggerPriceType { get; set; }
        /// <summary>The price of the take-profit order.</summary>
        public decimal? TakeProfitOrderPrice { get; set; }
        /// <summary>The stop-loss trigger price.</summary>
        public decimal? StopLossTriggerPrice { get; set; }
        /// <summary>The type of stop-loss trigger price.</summary>
        public TriggerPriceType? StopLossTriggerPriceType { get; set; }
        /// <summary>The price of the stop-loss order.</summary>
        public decimal? StopLossOrderPrice { get; set; }
        /// <summary>The trigger price for the order.</summary>
        public decimal? TriggerPrice { get; set; }
        /// <summary>The type of trigger price.</summary>
        public TriggerPriceType? TriggerPriceType { get; set; }
        /// <summary>The actual quantity executed for the order.</summary>
        public decimal? ActualOrderQuantity { get; set; }
        /// <summary>The actual price executed for the order.</summary>
        public decimal? ActualOrderPrice { get; set; }
        /// <summary>A custom tag for the order.</summary>
        public string? Tag { get; set; }
        /// <summary>The actual side of the order execution.</summary>
        public AlgoActualSide? ActualSide { get; set; }
        /// <summary>The time when the order was triggered.</summary>
        public DateTime? TriggerTime { get; set; }
        /// <summary>Whether the order is for reducing the position only.</summary>
        public bool ReduceOnly { get; set; }
        /// <summary>The last market price of the asset.</summary>
        public decimal? LastPrice { get; set; }
        /// <summary>The failure code, if the order failed.</summary>
        public string? FailCode { get; set; }
        /// <summary>The algorithmic client-defined unique identifier for the order.</summary>
        public string? AlgoClientOrderId { get; set; }
        /// <summary>The trade quote asset used for the order.</summary>
        public string? TradeQuoteAsset { get; set; }
        /// <summary>The time when the order was created.</summary>
        public DateTime CreateTime { get; set; }
        /// <summary>The time when the order was last updated.</summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>Whether the position is linked to this order.</summary>
        public bool? IsLinkPos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AlgoOrder"/> record.</summary>
        public AlgoOrder() { }
    }
}
