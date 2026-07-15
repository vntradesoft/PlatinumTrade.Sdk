namespace Pt.Okx.Sdk.Clients.Account.Model
{

    /// <summary>
    /// Represents detailed information for a specific asset within the trading account.
    /// </summary>
    public record AccountBalance
    {
        /// <summary>Gets or sets the asset ticker (e.g., "BTC").</summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>Gets or sets the available balance for this asset.</summary>
        public decimal? Available { get; set; }
        /// <summary>Gets or sets the initial margin for this asset.</summary>
        public decimal? InitialMargin { get; set; }
        /// <summary>Gets or sets the equity value of this asset.</summary>
        public decimal? Equity { get; set; }
        /// <summary>Gets or sets the unrealized PnL for this asset.</summary>
        public decimal? UnrealizedPnl { get; set; }
        /// <summary>Gets or sets the update time for this asset balance.</summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>Gets or sets the cash balance for this asset.</summary>
        public decimal? CashBalance { get; set; }
        /// <summary>Gets or sets the maintenance margin requirement for this asset.</summary>
        public decimal? MaintenanceMarginRequirement { get; set; }
        /// <summary>Gets or sets the margin ratio for this asset.</summary>
        public decimal? MarginRatio { get; set; }
    }
}
