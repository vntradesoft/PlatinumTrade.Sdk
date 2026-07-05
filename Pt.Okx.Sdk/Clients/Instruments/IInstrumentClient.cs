using Pt.Okx.Sdk.Clients.Instruments.Models;
using Pt.Okx.Sdk.Common;
using Pt.Okx.Sdk.Enums;

namespace Pt.Okx.Sdk.Clients.Instruments
{
    /// <summary>
    /// Interface for instrument clients, providing methods to query trading instrument information.
    /// </summary>
    public interface IInstrumentClient
    {
        #region Basic Info and Properties

        /// <summary>
        /// Gets the type of trading instrument handled by this client (e.g., Futures, Spot).
        /// </summary>
        InstrumentType InstrumentType { get; }

        /// <summary>
        /// Checks if the specified symbol is a valid and tracked trading symbol.
        /// </summary>
        /// <param name="symbol">The trading symbol to check.</param>
        /// <returns>True if the symbol is valid and tracked; otherwise, false.</returns>
        bool IsSymbol(string symbol);

        /// <summary>
        /// Gets the total number of trading symbols currently tracked.
        /// </summary>
        /// <returns>The total count of tracked symbols.</returns>
        int TotalSymbols();

        /// <summary>
        /// Gets the quote asset for the specified symbol (e.g., "USDT").
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <returns>The quote asset string.</returns>
        string QuoteAsset(string symbol);

        /// <summary>
        /// Gets the base asset for the specified symbol (e.g., "BTC").
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <returns>The base asset string.</returns>
        string BaseAsset(string symbol);

        /// <summary>
        /// Gets the underlying asset for the specified symbol (for derivatives).
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <returns>The underlying asset string.</returns>
        string Underlying(string symbol);

        #endregion

        #region Market Price Data

        /// <summary>
        /// Gets the last traded price for the specified symbol.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The last price wrapped in a WebCallResult.</returns>
        Task<ApiResult<decimal>> GetLastPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the mark price for the specified symbol (important for Futures/Swap).
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The mark price wrapped in a WebCallResult.</returns>
        Task<ApiResult<decimal>> GetMarkPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the current bid price for the specified symbol.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The bid price wrapped in a WebCallResult.</returns>
        Task<ApiResult<decimal>> GetBidAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the current ask price for the specified symbol.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The ask price wrapped in a WebCallResult.</returns>
        Task<ApiResult<decimal>> GetAskAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the spread (difference between ask and bid prices) for the specified symbol.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The spread value wrapped in a WebCallResult.</returns>
        Task<ApiResult<decimal>> GetSpreadAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the bid price, ask price, and spread for the specified symbol in a single call.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A tuple containing bid, ask, and spread values wrapped in a WebCallResult.</returns>
        Task<ApiResult<(decimal Bid, decimal Ask, decimal Spread)>> GetBidAskSpreadAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the highest price within a recent time frame for the specified symbol.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The high price wrapped in a WebCallResult.</returns>
        Task<ApiResult<decimal>> GetHighPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the lowest price within a recent time frame for the specified symbol.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The low price wrapped in a WebCallResult.</returns>
        Task<ApiResult<decimal>> GetLowPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the limit price for the specified symbol (for limit orders).
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The limit price wrapped in a WebCallResult.</returns>
        Task<ApiResult<LimitPrice>> GetLimitPriceAsync(string symbol, CancellationToken ct = default);

        #endregion

        #region Trading Rules and Normalization

        /// <summary>
        /// Gets the price precision (number of decimal places) for the specified symbol.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <returns>The price precision.</returns>
        int GetPrecisionPrice(string symbol);

        /// <summary>
        /// Gets the lot size precision (number of decimal places) for the specified symbol.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <returns>The lot size precision.</returns>
        int GetPrecisionLot(string symbol);

        /// <summary>
        /// Gets the tick size (minimum price increment) for the specified symbol.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <returns>The tick size value.</returns>
        decimal GetTickPrice(string symbol);

        /// <summary>
        /// Gets the minimum, maximum, and step size for lot orders for the specified symbol.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <param name="isMarket">True for market orders; otherwise, false for limit orders.</param>
        /// <returns>A tuple containing min, max, and step size values.</returns>
        (decimal MinQuantity, decimal MaxQuantity, decimal StepSize) GetLimitLotSize(string symbol, bool isMarket);

        /// <summary>
        /// Gets the maximum allowed cost for an order for the specified symbol.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <param name="isMarket">True for market orders; otherwise, false for limit orders.</param>
        /// <returns>The maximum cost value.</returns>
        decimal GetLimitMaxCost(string symbol, bool isMarket);

        /// <summary>
        /// Normalizes the lot size according to exchange rules.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <param name="lot">The lot size to normalize.</param>
        /// <param name="isMarket">True for market orders (may have different rules than limit orders).</param>
        /// <param name="roundUp">True to round up; false to round down.</param>
        /// <returns>The normalized lot size.</returns>
        decimal NormalizeLot(string symbol, decimal lot, bool isMarket, bool roundUp = false);

        /// <summary>
        /// Normalizes the price according to exchange rules (rounded by price precision).
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <param name="price">The price to normalize.</param>
        /// <param name="roundUp">True to round up; false to round down.</param>
        /// <returns>The normalized price.</returns>
        decimal NormalizePrice(string symbol, decimal price, bool roundUp = false);

        #endregion

        #region Fees and Margin

        /// <summary>
        /// Gets the taker trading fee for the specified symbol.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <returns>The taker fee value.</returns>
        decimal GetFeeTaker(string symbol);

        /// <summary>
        /// Gets the maker trading fee for the specified symbol.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <returns>The maker fee value.</returns>
        decimal GetFeeMaker(string symbol);

        /// <summary>
        /// Gets the maintenance margin rate for the specified symbol and position notional.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <param name="positionNotional">The notional value of the position.</param>
        /// <param name="ct">A cancellation token.</param>
        /// <returns>The maintenance margin rate.</returns>
        Task<decimal> GetMaintMarginRateAsync(string symbol, decimal positionNotional, CancellationToken ct = default);

        /// <summary>
        /// Gets the contract size for the specified symbol (for Futures/Swap).
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <returns>The contract size value.</returns>
        decimal ContractSize(string symbol);

        /// <summary>
        /// Gets the contract multiplier for the specified symbol (for Options).
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <returns>The contract multiplier value.</returns>
        decimal ContractMultiplier(string symbol);

        #endregion

        #region System Utilities

        /// <summary>
        /// Gets the current system time.
        /// </summary>
        /// <returns>The current system time as a DateTime object.</returns>
        DateTime GetCurrentTime();

        /// <summary>
        /// Gets the unique symbol code (long integer) for the specified trading symbol, and vice versa.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        long SymbolCode(string symbol);

        /// <summary>
        /// Gets the trading symbol string for the specified unique symbol code (long integer).
        /// </summary>
        /// <param name="symbolCode">The unique symbol code.</param>
        /// <returns>The trading symbol string.</returns>
        string Symbol(long symbolCode);

        #endregion
    }
}
