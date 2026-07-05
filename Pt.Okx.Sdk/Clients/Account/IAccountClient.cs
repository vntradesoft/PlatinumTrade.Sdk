using Pt.Okx.Sdk.Clients.Account.Enums;
using Pt.Okx.Sdk.Clients.Account.Model;
using Pt.Okx.Sdk.Common;

namespace Pt.Okx.Sdk.Clients.Account
{
    /// <summary>
    /// Interface for account clients, providing methods to query trading account information.
    /// </summary>
    public interface IAccountClient
    {
        #region Balances & Assets

        /// <summary>
        /// Gets detailed information about the trading account balance.
        /// </summary>
        /// <returns>The current account balance, or null if unavailable.</returns>
        AccountBalance? GetBalances();

        /// <summary>
        /// Selects and retrieves detailed information for a specific asset (e.g., "USDT").
        /// </summary>
        /// <param name="currency">The asset currency (default is "USDT").</param>
        /// <returns>The balance information for the specified asset, or null if not found.</returns>
        AccountBalanceDetail? AccountSelect(string currency = "USDT");

        /// <summary>
        /// Asynchronously refreshes the account balance information from the exchange.
        /// </summary>
        /// <returns>The refreshed account balance wrapped in a WebCallResult.</returns>
        Task<ApiResult<AccountBalance>> LoadBalanceAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the total actual wallet balance (excluding unrealized PnL).
        /// </summary>
        /// <returns>The wallet balance value.</returns>
        decimal WalletBalance { get; }

        /// <summary>
        /// Gets the available balance that can be used to open new positions.
        /// </summary>
        /// <returns>The available balance value.</returns>
        decimal AvailableBalance { get; }

        /// <summary>
        /// Gets the account equity (Wallet Balance + Unrealized PnL).
        /// </summary>
        /// <returns>The equity value.</returns>
        decimal Equity { get; }

        /// <summary>
        /// Gets the total unrealized profit and loss (PnL) from open positions.
        /// </summary>
        /// <returns>The unrealized PnL value.</returns>
        decimal UnrealizedPnL { get; }

        /// <summary>
        /// Gets the total initial margin currently in use.
        /// </summary>
        /// <returns>The initial margin value.</returns>
        decimal InitialMargin { get; }

        #endregion

        #region Trading Configuration

        /// <summary>
        /// Sets the initial leverage for a specific contract.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <param name="leverage">The leverage value to set.</param>
        /// <param name="ct">A cancellation token.</param>
        /// <returns>True if successful; otherwise, false.</returns>
        Task<bool> SetInitialLeverageAsync(string symbol, int leverage, CancellationToken ct = default);

        /// <summary>
        /// Gets the current leverage for a specific contract.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <returns>The leverage value.</returns>
        decimal GetLeverage(string symbol);

        /// <summary>
        /// Sets the position mode (Hedge Mode or Netting Mode).
        /// </summary>
        /// <param name="hedge">True for Hedge Mode; false for Netting Mode.</param>
        /// <param name="ct">A cancellation token.</param>
        /// <returns>A tuple indicating success and an optional error message.</returns>
        Task<(bool Success, string? Error)> SetHedgeModeAsync(bool hedge, CancellationToken ct = default);

        /// <summary>
        /// Checks if the account is currently in Hedge Mode.
        /// </summary>
        /// <returns>True if in Hedge Mode; otherwise, false.</returns>
        bool IsHedgeMode();

        /// <summary>
        /// Gets information about the account's trading fee VIP level.
        /// </summary>
        /// <returns>The fee VIP level wrapped in a WebCallResult.</returns>
        Task<ApiResult<FeeVipLevel>> GetFeeLevelAsync();

        #endregion

        #region Performance & Risk Analytics

        /// <summary>
        /// Gets the current equity value of the account.
        /// </summary>
        /// <returns>The current equity value.</returns>
        decimal GetCurrentEquity();

        /// <summary>
        /// Calculates the percentage change in equity compared to a reference point.
        /// </summary>
        /// <returns>The equity change percentage.</returns>
        decimal GetEquityChangePercentage();

        /// <summary>
        /// Calculates the margin ratio. Positions may be liquidated if this reaches 100%.
        /// </summary>
        /// <returns>The margin ratio value.</returns>
        decimal GetMarginRatio();

        /// <summary>
        /// Calculates the current account drawdown compared to the equity peak.
        /// </summary>
        /// <returns>The current drawdown value.</returns>
        decimal GetCurrentDrawdown();

        #endregion
    }


}
