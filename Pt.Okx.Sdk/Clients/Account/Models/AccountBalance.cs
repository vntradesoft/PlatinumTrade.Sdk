using System.Text.Json.Serialization;

namespace Pt.Okx.Sdk.Clients.Account.Model
{
    /// <summary>
    /// Represents the total trading account balance information.
    /// </summary>
    public record AccountBalance
    {
        /// <summary>Gets or sets the update time of the balance.</summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>Gets or sets the total equity of the account.</summary>
        public decimal TotalEquity { get; set; }

        /// <summary>Gets or sets the available equity.</summary>
        public decimal? AvailableEquity { get; set; }

        /// <summary>Gets or sets the isolated margin equity.</summary>
        public decimal? IsolatedMarginEquity { get; set; }

        /// <summary>Gets or sets the adjusted equity.</summary>
        public decimal? AdjustedEquity { get; set; }

        /// <summary>Gets or sets the frozen borrow quantity.</summary>
        public decimal? BorrowFrozen { get; set; }

        /// <summary>Gets or sets the order frozen amount.</summary>
        public decimal? OrderFrozen { get; set; }

        /// <summary>Gets or sets the initial margin requirement.</summary>
        public decimal? InitialMarginRequirement { get; set; }

        /// <summary>Gets or sets the maintenance margin requirement.</summary>
        public decimal? MaintenanceMarginRequirement { get; set; }

        /// <summary>Gets or sets the margin ratio.</summary>
        public decimal? MarginRatio { get; set; }

        /// <summary>Gets or sets the total notional value in USD.</summary>
        public decimal? NotionalUsd { get; set; }

        /// <summary>Gets or sets the unrealized profit and loss (PnL) in USD at the account level.</summary>
        public decimal? UnrealizedPnl { get; set; }

        /// <summary>Gets or sets the list of detailed asset balances.</summary>
        public IReadOnlyList<AccountBalanceDetail> Details { get; set; } = Array.Empty<AccountBalanceDetail>();
    }

}
