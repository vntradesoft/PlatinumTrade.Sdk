namespace Pt.Okx.Sdk.Clients.Trading.Models
{
    /// <summary>
    /// Represents an update to an order in the OKX trading system.
    /// </summary>
    public record OrderUpdate : Order
    {
        /// <summary>The result of the amendment.</summary>
        public string? AmendResult { get; set; }
        /// <summary>The source of the amendment.</summary>
        public string? AmendSource { get; set; }
        /// <summary>The execution type of the order.</summary>
        public string? ExecutionType { get; set; }
        /// <summary>The fee incurred for the fill.</summary>
        public decimal FillFee { get; set; }
        /// <summary>The asset used for the fill fee.</summary>
        public string FillFeeAsset { get; set; } = string.Empty;
        /// <summary>The notional value of the fill in USD.</summary>
        public decimal? FillNotionalUsd { get; set; }
        /// <summary>The profit and loss from the fill.</summary>
        public decimal FillPnl { get; set; }
        /// <summary>The notional value of the update in USD.</summary>
        public decimal? NotionalUsd { get; set; }
        /// <summary>The request identifier associated with the update.</summary>
        public string? RequestId { get; set; }
        /// <summary>The implied volatility of the last trade.</summary>
        public decimal? LastTradeImpliedVolatility { get; set; }
        /// <summary>The USD price of the last trade.</summary>
        public decimal? LastTradeUsdPrice { get; set; }
        /// <summary>The mark volatility of the last trade.</summary>
        public decimal? LastTradeMarkVolatility { get; set; }
        /// <summary>The forward price of the last trade.</summary>
        public decimal? LastTradeForwardPrice { get; set; }
        /// <summary>The mark price of the last trade.</summary>
        public decimal? LastTradeMarkPrice { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OrderUpdate"/> record.</summary>
        public OrderUpdate() { }
    }
}
