using Pt.Okx.Sdk.Clients.Trading.Enums;
using Pt.Okx.Sdk.Enums;

namespace Pt.Okx.Sdk.Clients.Trading.Models
{
    /// <summary>
    /// Represents an order in the OKX trading system.
    /// </summary>
    public record Order
    {
        /// <summary>The type of the instrument.</summary>
        public InstrumentType InstrumentType { get; set; }
        /// <summary>The trading symbol (e.g., BTC-USDT).</summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>The asset or currency base for the order.</summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>The unique identifier for the order.</summary>
        public long? OrderId { get; set; }
        /// <summary>The client-defined unique identifier for the order.</summary>
        public string? ClientOrderId { get; set; }
        /// <summary>A custom tag for the order.</summary>
        public string Tag { get; set; } = string.Empty;
        /// <summary>The price of the order.</summary>
        public decimal? Price { get; set; }
        /// <summary>The quantity of the asset to be traded.</summary>
        public decimal? Quantity { get; set; }
        /// <summary>The profit and loss of the order.</summary>
        public decimal? ProfitAndLoss { get; set; }
        /// <summary>The type of the order.</summary>
        public OrderType OrderType { get; set; }
        /// <summary>The side of the order (Buy/Sell).</summary>
        public OrderSide OrderSide { get; set; }
        /// <summary>The position side (e.g., Long/Short).</summary>
        public PositionSide? PositionSide { get; set; }
        /// <summary>The trade mode (e.g., Cash/Margin).</summary>
        public TradeMode TradeMode { get; set; }
        /// <summary>The accumulated filled quantity for the order.</summary>
        public decimal? AccumulatedFillQuantity { get; set; }
        /// <summary>The price at which the order was filled.</summary>
        public decimal? FillPrice { get; set; }
        /// <summary>The unique identifier of the trade.</summary>
        public long? TradeId { get; set; }
        /// <summary>The quantity filled for the order.</summary>
        public decimal? QuantityFilled { get; set; }
        /// <summary>The time when the order was filled.</summary>
        public DateTime? FillTime { get; set; }
        /// <summary>The average price of the filled order.</summary>
        public decimal? AveragePrice { get; set; }
        /// <summary>The status of the order.</summary>
        public OrderStatus OrderState { get; set; }
        /// <summary>The self-trade prevention mode.</summary>
        public SelfTradePreventionMode? SelfTradePreventionMode { get; set; }
        /// <summary>The leverage applied to the order.</summary>
        public decimal? Leverage { get; set; }
        /// <summary>The client-defined unique identifier of the attached algorithmic order.</summary>
        public string? AttachAlgoCllientOrderId { get; set; }
        /// <summary>The take-profit trigger price.</summary>
        public decimal? TakeProfitTriggerPrice { get; set; }
        /// <summary>The price of the take-profit order.</summary>
        public decimal? TakeProfitOrderPrice { get; set; }
        /// <summary>The type of take-profit trigger price.</summary>
        public TriggerPriceType? TakeProfitTriggerPriceType { get; set; }
        /// <summary>The stop-loss trigger price.</summary>
        public decimal? StopLossTriggerPrice { get; set; }
        /// <summary>The type of stop-loss trigger price.</summary>
        public TriggerPriceType? StopLossTriggerPriceType { get; set; }
        /// <summary>The price of the stop-loss order.</summary>
        public decimal? StopLossOrderPrice { get; set; }
        /// <summary>The source of the order.</summary>
        public string Source { get; set; } = string.Empty;
        /// <summary>Whether the order is for reducing the position only.</summary>
        public bool ReduceOnly { get; set; }
        /// <summary>Whether the order is a take-profit limit order.</summary>
        public bool? IsTakeProfitLimit { get; set; }
        /// <summary>The algorithmic client-defined unique identifier for the order.</summary>
        public string? AlgoClientOrderId { get; set; }
        /// <summary>The unique identifier for the algorithmic order.</summary>
        public string? AlgoId { get; set; }
        /// <summary>The asset used for the fee.</summary>
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>The fee incurred for the order.</summary>
        public decimal? Fee { get; set; }
        /// <summary>The last market price of the asset.</summary>
        public decimal? LastPrice { get; set; }
        /// <summary>The trade quote asset used for the order.</summary>
        public string TradeQuoteAsset { get; set; } = string.Empty;
        /// <summary>The time when the order was created.</summary>
        public DateTime CreateTime { get; set; }
        /// <summary>The time when the order was last updated.</summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>Initializes a new instance of the <see cref="Order"/> record.</summary>
        public Order() { }
    }

}
