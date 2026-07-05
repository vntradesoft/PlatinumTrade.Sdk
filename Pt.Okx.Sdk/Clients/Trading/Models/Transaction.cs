using Pt.Okx.Sdk.Clients.Trading.Enums;
using Pt.Okx.Sdk.Enums;

namespace Pt.Okx.Sdk.Clients.Trading.Models
{
    /// <summary>
    /// Represents a transaction in the OKX trading system.
    /// </summary>
    public class Transaction
    {
        /// <summary>The type of the instrument.</summary>
        public InstrumentType InstrumentType { get; set; }
        /// <summary>The trading symbol (e.g., BTC-USDT).</summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>The unique identifier for the trade.</summary>
        public long? TradeId { get; set; }
        /// <summary>The unique identifier for the order.</summary>
        public long? OrderId { get; set; }
        /// <summary>The client-defined unique identifier for the order.</summary>
        public string? ClientOrderId { get; set; }
        /// <summary>The unique identifier for the bill.</summary>
        public long? BillId { get; set; }
        /// <summary>The type of the transaction.</summary>
        public string TransactionType { get; set; } = string.Empty;
        /// <summary>The sub-type of the transaction.</summary>
        public TransactionType SubType { get; set; }
        /// <summary>A custom tag for the transaction.</summary>
        public string Tag { get; set; } = string.Empty;
        /// <summary>The price of the fill.</summary>
        public decimal? FillPrice { get; set; }
        /// <summary>The quantity filled for the transaction.</summary>
        public decimal? QuantityFilled { get; set; }
        /// <summary>The index price at the time of fill.</summary>
        public decimal? FillIndexPrice { get; set; }
        /// <summary>The profit and loss from the fill.</summary>
        public decimal? FillProfitAndLoss { get; set; }
        /// <summary>The mark price at the time of fill.</summary>
        public decimal? FillMarkPrice { get; set; }
        /// <summary>The side of the order (Buy/Sell).</summary>
        public OrderSide OrderSide { get; set; }
        /// <summary>The position side (e.g., Long/Short).</summary>
        public PositionSide PositionSide { get; set; }
        /// <summary>The flow type of the order.</summary>
        public OrderFlowType OrderFlowType { get; set; }
        /// <summary>The asset used for the fee.</summary>
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>The fee incurred for the transaction.</summary>
        public decimal? Fee { get; set; }
        /// <summary>The time of the transaction.</summary>
        public DateTime Time { get; set; }
        /// <summary>The time of the fill.</summary>
        public DateTime FillTime { get; set; }
        /// <summary>The trade quote asset used for the transaction.</summary>
        public string TradeQuoteAsset { get; set; } = string.Empty;

        /// <summary>Initializes a new instance of the <see cref="Transaction"/> class.</summary>
        public Transaction() { }
    }
}
