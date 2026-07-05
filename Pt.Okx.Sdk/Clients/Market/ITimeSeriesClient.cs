using Pt.Okx.Sdk.Clients.Market.Models;
using Pt.Okx.Sdk.Enums;
using Pt.Okx.Sdk.Indicators.Base;
using Pt.Okx.Sdk.Indicators.BuiltIn;
using Pt.Okx.Sdk.Indicators.Enums;
using Pt.Okx.Sdk.Indicators.Models;
using Pt.Okx.Sdk.Indicators.Services;

namespace Pt.Okx.Sdk.Clients.Market
{
    /// <summary>
    /// Client interface providing methods for querying time series (OHLCV) data and initializing technical indicators.
    /// </summary>
    public interface ITimeSeriesClient
    {
        #region Configuration and Status

        /// <summary>
        /// Gets the current timeframe (Kline interval) in use.
        /// </summary>
        Timeframe PeriodCurrent { get; }

        /// <summary>
        /// Gets the start time of the data in the system.
        /// </summary>
        DateTime BeginTime { get; }

        /// <summary>
        /// Gets the end time of the data (null if running in real-time).
        /// </summary>
        DateTime? EndTime { get; }

        /// <summary>
        /// Gets the system start time.
        /// </summary>
        DateTime? StartTime { get; }

        /// <summary>
        /// Gets the maximum number of bars (candles) stored in the cache.
        /// </summary>
        int MaxBars { get; }

        /// <summary>
        /// Gets the manager for initialized technical indicators.
        /// </summary>
        IIndicatorManager Indicator { get; }

        /// <summary>
        /// Gets the distinct symbol/timeframe pairs scoped to this strategy instance
        /// (primary pair + indicator pairs). Not the global singleton.
        /// </summary>
        HashSet<(string Symbol, Timeframe Timeframe)> SymbolsTimes { get; }
        /// <summary>
        /// Gets the most recent tick price of the asset as received from the market data feed.
        /// </summary>
        decimal CurrentTickPrice { get; }
        /// <summary>
        /// Gets the most recent tick data representing the current state of the system.
        /// </summary>
        TickData CurrentTick { get; }

        /// <summary>
        /// Gets the current time based on the specified timeframe.
        /// </summary>
        /// <param name="timeframe">The timeframe to check.</param>
        /// <returns>The current candle time for the specified timeframe.</returns>
        DateTime GetCurrentTime(Timeframe timeframe = Timeframe.OneMinute);

        /// <summary>
        /// Counts the number of bars calculated for a specific indicator.
        /// </summary>
        /// <param name="indicatorId">The unique ID of the indicator.</param>
        /// <returns>The number of bars calculated.</returns>
        int BarsCalculated(string indicatorId);

        /// <summary>
        /// Gets the total number of bars available for a trading pair and timeframe.
        /// </summary>
        /// <param name="symbol">The trading symbol (e.g., BTC-USDT).</param>
        /// <param name="timeframe">The timeframe.</param>
        /// <returns>The total number of available bars.</returns>
        int Bars(string? symbol, Timeframe? timeframe);

        #endregion

        #region OHLCV Data Access

        /// <summary>
        /// Gets the OHLCV data at a specific index (0 = current forming candle, 1 = previous closed candle, etc.).
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <param name="timeframe">The timeframe.</param>
        /// <param name="shift">The candle index to retrieve.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>The corresponding candle data.</returns>
        Task<CandleData> GetOHCLVAsync(string? symbol = null, Timeframe? timeframe = null, int shift = 0, CancellationToken ct = default);

        /// <summary>
        /// Gets the data for the currently forming candle.
        /// </summary>
        /// <param name="symbol">The trading symbol.</param>
        /// <param name="timeframe">The timeframe.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>The current candle data.</returns>
        /// <remarks>Parameters <c>symbol</c> and <c>timeframe</c> are optional and default to the current settings if not provided.</remarks>
        Task<CandleData> GetCurrentCandleAsync(string? symbol = null, Timeframe? timeframe = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the timestamp of the candle at the specified shift for a given timeframe. Shift 0 corresponds to the current forming candle, shift 1 is the last closed candle, and so on.
        /// </summary>
        /// <param name="timeframe">The timeframe for the candle.</param>
        /// <param name="shift">The shift index (0 = current forming candle, 1 = last closed candle, etc.).</param>
        /// <returns>The timestamp of the candle at the specified shift.</returns>
        DateTime GetTime(Timeframe timeframe, int shift);

        /// <summary>
        /// Retrieves the open candle data for the specified trading symbol and timeframe.
        /// </summary>
        /// <remarks>Use this method to access the initial price data for a trading symbol within a
        /// specific timeframe. Ensure that the provided symbol and timeframe are valid to avoid unexpected
        /// results.</remarks>
        /// <param name="symbol">The trading symbol for which to obtain the open candle data. If null, the default symbol is used.</param>
        /// <param name="timeframe">The interval representing the timeframe for the candle data. If null, the default timeframe is applied.</param>
        /// <returns>A CandleData object representing the open candle for the specified symbol and timeframe, or null if no data
        /// is available.</returns>
        CandleData GetOpenCandle(string? symbol = null, Timeframe? timeframe = null);

        /// <summary>
        /// Gets the most recent closed candle data for the specified trading symbol and timeframe.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe (optional).</param>
        /// <returns>The last closed candle data.</returns>
        /// <remarks>Parameters <c>symbol</c> and <c>timeframe</c> are optional and default to the current settings if not provided.</remarks>
        CandleData GetLastClosedCandle(string? symbol = null, Timeframe? timeframe = null);


        #endregion

        #region Copy Series Data


        /// <summary>
        /// Copies an array of OHLCV data within a specified time range.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the data (optional).</param>
        /// <param name="startTime">The start time of the range.</param>
        /// <param name="endTime">The end time of the range.</param>
        /// <returns>An array of OHLCV candle data within the specified time range.</returns>
        Task<CandleData[]> CopySeries(string? symbol, Timeframe? timeframe, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Copies an array of OHLCV data by start position and number of candles.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the data (optional).</param>
        /// <param name="startPos">The starting position (index) in the series.</param>
        /// <param name="count">The number of candles to copy.</param>
        /// <returns>An array of OHLCV candle data starting from the specified position.</returns>
        /// <remarks>Parameters <c>symbol</c> and <c>timeframe</c> are optional and default to the current settings if not provided.</remarks>
        Task<CandleData[]> CopySeries(string? symbol, Timeframe? timeframe, int startPos, int count);

        /// <summary>
        /// Copies an array of OHLCV data from a specific time with a defined number of candles.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the data (optional).</param>
        /// <param name="startTime">The start time for copying data.</param>
        /// <param name="count">The number of candles to copy.</param>
        /// <returns>An array of OHLCV candle data starting from the specified time.</returns>
        Task<CandleData[]> CopySeries(string? symbol, Timeframe? timeframe, DateTime startTime, int count);

        #endregion

        #region Copy Component Data


        /// <summary>
        /// Copies an array of times by position and count.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="tf">The timeframe (optional).</param>
        /// <param name="startPos">The starting position (index) in the series.</param>
        /// <param name="count">The number of times to copy.</param>
        /// <returns>An array of DateTime values representing candle times.</returns>
        /// <remarks>Parameters <c>symbol</c> and <c>tf</c> are optional and default to the current settings if not provided.</remarks>
        Task<DateTime[]> CopyTimes(string? symbol, Timeframe? tf, int startPos, int count);
        /// <summary>
        /// Copies an array of times from a start time with a defined count.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="tf">The timeframe (optional).</param>
        /// <param name="startTime">The start time for copying data.</param>
        /// <param name="count">The number of times to copy.</param>
        /// <returns>An array of DateTime values representing candle times.</returns>
        Task<DateTime[]> CopyTimes(string? symbol, Timeframe? tf, DateTime startTime, int count);
        /// <summary>
        /// Copies an array of times within a specified time range.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="tf">The timeframe (optional).</param>
        /// <param name="startTime">The start time of the range.</param>
        /// <param name="endTime">The end time of the range.</param>
        /// <returns>An array of DateTime values representing candle times within the specified range.</returns>
        /// <remarks>Parameters <c>symbol</c> and <c>tf</c> are optional and default to the current settings if not provided.</remarks>
        Task<DateTime[]> CopyTimes(string? symbol, Timeframe? tf, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Copies an array of open prices by position and count.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="tf">The timeframe (optional).</param>
        /// <param name="startPos">The starting position (index) in the series.</param>
        /// <param name="count">The number of open prices to copy.</param>
        /// <returns>An array of open prices.</returns>
        Task<decimal[]> CopyOpens(string? symbol, Timeframe? tf, int startPos, int count);
        /// <summary>
        /// Copies an array of open prices from a start time with a defined count.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="tf">The timeframe (optional).</param>
        /// <param name="startTime">The start time for copying data.</param>
        /// <param name="count">The number of open prices to copy.</param>
        /// <returns>An array of open prices.</returns>
        /// <remarks>Parameters <c>symbol</c> and <c>tf</c> are optional and default to the current settings if not provided.</remarks>
        Task<decimal[]> CopyOpens(string? symbol, Timeframe? tf, DateTime startTime, int count);
        /// <summary>
        /// Copies an array of open prices within a specified time range.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="tf">The timeframe (optional).</param>
        /// <param name="startTime">The start time of the range.</param>
        /// <param name="endTime">The end time of the range.</param>
        /// <returns>An array of open prices within the specified range.</returns>
        Task<decimal[]> CopyOpens(string? symbol, Timeframe? tf, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Copies an array of high prices by position and count.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="tf">The timeframe (optional).</param>
        /// <param name="startPos">The starting position (index) in the series.</param>
        /// <param name="count">The number of high prices to copy.</param>
        /// <returns>An array of high prices.</returns>
        /// <remarks>Parameters <c>symbol</c> and <c>tf</c> are optional and default to the current settings if not provided.</remarks>
        Task<decimal[]> CopyHighs(string? symbol, Timeframe? tf, int startPos, int count);
        /// <summary>
        /// Copies an array of high prices from a start time with a defined count.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="tf">The timeframe (optional).</param>
        /// <param name="startTime">The start time for copying data.</param>
        /// <param name="count">The number of high prices to copy.</param>
        /// <returns>An array of high prices.</returns>
        Task<decimal[]> CopyHighs(string? symbol, Timeframe? tf, DateTime startTime, int count);
        /// <summary>
        /// Copies an array of high prices within a specified time range.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="tf">The timeframe (optional).</param>
        /// <param name="startTime">The start time of the range.</param>
        /// <param name="endTime">The end time of the range.</param>
        /// <returns>An array of high prices within the specified range.</returns>
        /// <remarks>Parameters <c>symbol</c> and <c>tf</c> are optional and default to the current settings if not provided.</remarks>
        Task<decimal[]> CopyHighs(string? symbol, Timeframe? tf, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Copies an array of low prices by position and count.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="tf">The timeframe (optional).</param>
        /// <param name="startPos">The starting position (index) in the series.</param>
        /// <param name="count">The number of low prices to copy.</param>
        /// <returns>An array of low prices.</returns>
        Task<decimal[]> CopyLows(string? symbol, Timeframe? tf, int startPos, int count);
        /// <summary>
        /// Copies an array of low prices from a start time with a defined count.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="tf">The timeframe (optional).</param>
        /// <param name="startTime">The start time for copying data.</param>
        /// <param name="count">The number of low prices to copy.</param>
        /// <returns>An array of low prices.</returns>
        /// <remarks>Parameters <c>symbol</c> and <c>tf</c> are optional and default to the current settings if not provided.</remarks>
        Task<decimal[]> CopyLows(string? symbol, Timeframe? tf, DateTime startTime, int count);
        /// <summary>
        /// Copies an array of low prices within a specified time range.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="tf">The timeframe (optional).</param>
        /// <param name="startTime">The start time of the range.</param>
        /// <param name="endTime">The end time of the range.</param>
        /// <returns>An array of low prices within the specified range.</returns>
        Task<decimal[]> CopyLows(string? symbol, Timeframe? tf, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Copies an array of close prices by position and count.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="tf">The timeframe (optional).</param>
        /// <param name="startPos">The starting position (index) in the series.</param>
        /// <param name="count">The number of close prices to copy.</param>
        /// <returns>An array of close prices.</returns>
        /// <remarks>Parameters <c>symbol</c> and <c>tf</c> are optional and default to the current settings if not provided.</remarks>
        Task<decimal[]> CopyCloses(string? symbol, Timeframe? tf, int startPos, int count);
        /// <summary>
        /// Copies an array of close prices from a start time with a defined count.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="tf">The timeframe (optional).</param>
        /// <param name="startTime">The start time for copying data.</param>
        /// <param name="count">The number of close prices to copy.</param>
        /// <returns>An array of close prices.</returns>
        Task<decimal[]> CopyCloses(string? symbol, Timeframe? tf, DateTime startTime, int count);
        /// <summary>
        /// Copies an array of close prices within a specified time range.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="tf">The timeframe (optional).</param>
        /// <param name="startTime">The start time of the range.</param>
        /// <param name="endTime">The end time of the range.</param>
        /// <returns>An array of close prices within the specified range.</returns>
        /// <remarks>Parameters <c>symbol</c> and <c>tf</c> are optional and default to the current settings if not provided.</remarks>
        Task<decimal[]> CopyCloses(string? symbol, Timeframe? tf, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Copies an array of volumes by position and count.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="tf">The timeframe (optional).</param>
        /// <param name="startPos">The starting position (index) in the series.</param>
        /// <param name="count">The number of volumes to copy.</param>
        /// <returns>An array of volume values.</returns>
        Task<decimal[]> CopyVolumes(string? symbol, Timeframe? tf, int startPos, int count);
        /// <summary>
        /// Copies an array of volumes from a start time with a defined count.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="tf">The timeframe (optional).</param>
        /// <param name="startTime">The start time for copying data.</param>
        /// <param name="count">The number of volumes to copy.</param>
        /// <returns>An array of volume values.</returns>
        /// <remarks>Parameters <c>symbol</c> and <c>tf</c> are optional and default to the current settings if not provided.</remarks>
        Task<decimal[]> CopyVolumes(string? symbol, Timeframe? tf, DateTime startTime, int count);
        /// <summary>
        /// Copies an array of volumes within a specified time range.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="tf">The timeframe (optional).</param>
        /// <param name="startTime">The start time of the range.</param>
        /// <param name="endTime">The end time of the range.</param>
        /// <returns>An array of volume values within the specified range.</returns>
        /// <remarks>Parameters <c>symbol</c> and <c>tf</c> are optional and default to the current settings if not provided.</remarks>
        Task<decimal[]> CopyVolumes(string? symbol, Timeframe? tf, DateTime startTime, DateTime endTime);

        #endregion

        #region Copy Prices by Type

        /// <summary>
        /// Copies prices by AppliedPrice type within a specified time range.
        /// </summary>
        /// <param name="appliedPrice">The type of price to extract (e.g., close, open, high, low).</param>
        /// <remarks>Parameters <c>symbol</c> and <c>tf</c> are optional and default to the current settings if not provided.</remarks>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the data (optional).</param>
        /// <param name="start">The start time of the range.</param>
        /// <param name="endTime">The end time of the range.</param>
        /// <returns>An enumerable of (DateTime, price) tuples within the specified time range.</returns>
        Task<IEnumerable<(DateTime, decimal)>> CopyPrices(AppliedPrice appliedPrice, string? symbol, Timeframe? timeframe, DateTime start, DateTime endTime);
        /// <summary>
        /// Copies prices by AppliedPrice type from a start time with a defined count.
        /// </summary>
        /// <param name="appliedPrice">The type of price to extract (e.g., close, open, high, low).</param>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the data (optional).</param>
        /// <param name="start">The start time for copying data.</param>
        /// <param name="count">The number of prices to copy.</param>
        /// <returns>An enumerable of (DateTime, price) tuples starting from the specified time.</returns>
        /// <remarks>Parameters <c>symbol</c> and <c>timeframe</c> are optional and default to the current settings if not provided.</remarks>
        Task<IEnumerable<(DateTime, decimal)>> CopyPrices(AppliedPrice appliedPrice, string? symbol, Timeframe? timeframe, DateTime start, int count);
        /// <summary>
        /// Copies prices by AppliedPrice type by index and count.
        /// </summary>
        /// <param name="appliedPrice">The type of price to extract (e.g., close, open, high, low).</param>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the data (optional).</param>
        /// <param name="startPos">The starting position (index) in the series.</param>
        /// <param name="count">The number of prices to copy.</param>
        /// <returns>An enumerable of (DateTime, price) tuples starting from the specified position.</returns>
        Task<IEnumerable<(DateTime, decimal)>> CopyPrices(AppliedPrice appliedPrice, string? symbol, Timeframe? timeframe, int startPos, int count);
        /// <summary>
        /// Extracts a list of prices from an existing OHLCV data series.
        /// </summary>
        /// <param name="appliedPrice">The type of price to extract (e.g., close, open, high, low).</param>
        /// <param name="ohclvs">The OHLCV data series.</param>
        /// <returns>An enumerable of price values extracted from the OHLCV series.</returns>
        /// <remarks>Parameters <c>symbol</c> and <c>timeframe</c> are optional and default to the current settings if not provided, if applicable.</remarks>
        IEnumerable<decimal> CopyPrices(AppliedPrice appliedPrice, IEnumerable<CandleData> ohclvs);
        /// <summary>
        /// Extracts a specific price value from a candle based on AppliedPrice.
        /// </summary>
        /// <param name="appliedPrice">The type of price to extract (e.g., close, open, high, low).</param>
        /// <param name="ohclv">The candle data to extract the price from.</param>
        /// <returns>The extracted price value from the candle.</returns>
        decimal CopyPrice(AppliedPrice appliedPrice, CandleData ohclv);

        #endregion

        #region Indicator Buffer Management

        /// <summary>
        /// Copies indicator buffer values by index and count.
        /// </summary>
        /// <param name="indicatorHandle">The unique handle of the indicator.</param>
        /// <param name="bufferNumber">The buffer number to copy from.</param>
        /// <param name="startIndex">The starting index in the buffer.</param>
        /// <param name="count">The number of values to copy.</param>
        /// <param name="buffers">The output enumerable of copied indicator values.</param>
        /// <remarks>Parameters <c>symbol</c> and <c>tf</c> are optional and default to the current settings if not provided.</remarks>
        /// <returns>The number of values copied.</returns>
        int CopyBuffer(string indicatorHandle, int bufferNumber, int startIndex, int count, out IEnumerable<IndicatorValue> buffers);
        /// <summary>
        /// Copies indicator buffer values from a start time with a defined count.
        /// </summary>
        /// <param name="indicatorHandle">The unique handle of the indicator.</param>
        /// <param name="bufferNumber">The buffer number to copy from.</param>
        /// <param name="startTime">The start time for copying data.</param>
        /// <param name="count">The number of values to copy.</param>
        /// <param name="buffers">The output enumerable of copied indicator values.</param>
        /// <remarks>Parameters <c>symbol</c> and <c>tf</c> are optional and default to the current settings if not provided.</remarks>
        /// <returns>The number of values copied.</returns>
        int CopyBuffer(string indicatorHandle, int bufferNumber, DateTime startTime, int count, out IEnumerable<IndicatorValue> buffers);
        /// <summary>
        /// Copies indicator buffer values within a specified time range.
        /// </summary>
        /// <param name="indicatorHandle">The unique handle of the indicator.</param>
        /// <param name="bufferNumber">The buffer number to copy from.</param>
        /// <param name="startTime">The start time of the range.</param>
        /// <param name="endTime">The end time of the range.</param>
        /// <param name="buffers">The output enumerable of copied indicator values.</param>
        /// <remarks>Parameters <c>symbol</c> and <c>tf</c> are optional and default to the current settings if not provided.</remarks>
        /// <returns>The number of values copied.</returns>
        int CopyBuffer(string indicatorHandle, int bufferNumber, DateTime startTime, DateTime endTime, out IEnumerable<IndicatorValue> buffers);

        #endregion

        #region Specific Technical Indicators

        /// <summary>
        /// Creates a Moving Average (MA) indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the moving average (optional).</param>
        /// <param name="method">The moving average calculation method (optional).</param>
        /// <param name="appliedPrice">The price type to apply the calculation to (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created Moving Average indicator instance.</returns>
        IIndicatorMA CreateIndicatorMA(string? symbol = null, Timeframe? timeframe = null, int? period = null, MaMethod? method = null, AppliedPrice? appliedPrice = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Relative Strength Index (RSI) indicator.
        /// </summary>
        /// <remarks>Parameters <c>symbol</c> and <c>timeframe</c> are optional and default to the current settings if not provided.</remarks>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the RSI calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created RSI indicator instance.</returns>
        IIndicatorRSI CreateIndicatorRSI(string? symbol = null, Timeframe? timeframe = null, int? period = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Stochastic Oscillator indicator.
        /// </summary>
        /// <remarks>Parameters <c>symbol</c> and <c>timeframe</c> are optional and default to the current settings if not provided.</remarks>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="kPeriod">The %K period (optional).</param>
        /// <param name="dPeriod">The %D period (optional).</param>
        /// <param name="kSlow">The slowing factor for %K (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created Stochastic Oscillator indicator instance.</returns>
        IIndicatorStochastic CreateIndicatorStochastic(string? symbol = null, Timeframe? timeframe = null, int? kPeriod = null, int? dPeriod = null, int? kSlow = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Moving Average Convergence Divergence (MACD) indicator.
        /// </summary>
        /// <remarks>Parameters <c>symbol</c> and <c>timeframe</c> are optional and default to the current settings if not provided.</remarks>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="fastPeriod">The fast EMA period (optional).</param>
        /// <param name="slowPeriod">The slow EMA period (optional).</param>
        /// <param name="signalPeriod">The signal line EMA period (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created MACD indicator instance.</returns>
        IIndicatorMACD CreateIndicatorMACD(string? symbol = null, Timeframe? timeframe = null, int? fastPeriod = null, int? slowPeriod = null, int? signalPeriod = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates an Average True Range (ATR) indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the ATR calculation (optional).</param>
        /// <param name="method">The moving average method for smoothing the ATR (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created ATR indicator instance.</returns>
        IIndicatorATR CreateIndicatorATR(string? symbol = null, Timeframe? timeframe = null, int? period = null, MaMethod? method = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a SuperTrend indicator for trend identification.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the ATR calculation used in SuperTrend (optional).</param>
        /// <param name="multiplier">The multiplier for the ATR in SuperTrend calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created SuperTrend indicator instance.</returns>
        IIndicatorSuperTrend CreateIndicatorSuperTrend(string? symbol = null, Timeframe? timeframe = null, int? period = null, double? multiplier = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates an Ichimoku indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="tenkanPeriod">Tenkan-sen period (optional).</param>
        /// <param name="kijunPeriod">Kijun-sen period (optional).</param>
        /// <param name="senkouBPeriod">Senkou Span B period (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created Ichimoku indicator instance.</returns>
        IIndicatorIchimoku CreateIndicatorIchimoku(string? symbol = null, Timeframe? timeframe = null, int? tenkanPeriod = null, int? kijunPeriod = null, int? senkouBPeriod = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Volume Weighted Average Price (VWAP) indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="resetDaily">Whether to reset the VWAP calculation daily (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created VWAP indicator instance.</returns>
        IIndicatorVWAP CreateIndicatorVWAP(string? symbol = null, Timeframe? timeframe = null, bool? resetDaily = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);
        /// <summary>
        /// Creates a Volume Spike indicator for detecting volume surges.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the volume spike calculation (optional).</param>
        /// <param name="spikeThreshold">The threshold multiplier to detect a spike (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created Volume Spike indicator instance.</returns>
        IIndicatorVolumeSpike CreateIndicatorVolumeSpike(string? symbol = null, Timeframe? timeframe = null, int? period = null, double? spikeThreshold = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates an Accumulation/Distribution (AD) indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created AD indicator instance.</returns>
        IIndicatorAD CreateIndicatorAD(string? symbol = null, Timeframe? timeframe = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates an Average Directional Index Wilder (ADXW) indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the ADXW calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created ADXW indicator instance.</returns>
        IIndicatorADXW CreateIndicatorADXW(string? symbol = null, Timeframe? timeframe = null, int? period = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Bollinger Bands Width/Money Flow Index (BWMFI) indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="multiplier">The multiplier for the BWMFI calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created BWMFI indicator instance.</returns>
        IIndicatorBWMFI CreateIndicatorBWMFI(string? symbol = null, Timeframe? timeframe = null, double? multiplier = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a DeMarker indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the DeMarker calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created DeMarker indicator instance.</returns>
        IIndicatorDeMarker CreateIndicatorDeMarker(string? symbol = null, Timeframe? timeframe = null, int? period = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates an On Balance Volume (OBV) indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created OBV indicator instance.</returns>
        IIndicatorOBV CreateIndicatorOBV(string? symbol = null, Timeframe? timeframe = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates an Oscillator of Moving Average (OsMA) indicator from two source indicator IDs.
        /// </summary>
        /// <param name="sourceId1">The ID of the first source indicator.</param>
        /// <param name="sourceId2">The ID of the second source indicator.</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created OsMA indicator instance.</returns>
        IIndicatorOsMA CreateIndicatorOsMA(string sourceId1, string sourceId2, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Standard Deviation (StdDev) indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the StdDev calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created StdDev indicator instance.</returns>
        IIndicatorStdDev CreateIndicatorStdDev(string? symbol = null, Timeframe? timeframe = null, int? period = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a TRIX indicator for momentum analysis.
        /// </summary>
        /// <param name="sourceId">The source indicator ID for TRIX input.</param>
        /// <param name="period">The period for the TRIX calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created TRIX indicator instance.</returns>
        IIndicatorTRIX CreateIndicatorTRIX(string sourceId, int? period = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Williams %R (WPR) indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the WPR calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created WPR indicator instance.</returns>
        IIndicatorWPR CreateIndicatorWPR(string? symbol = null, Timeframe? timeframe = null, int? period = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates an Accelerator Oscillator (AC) indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created AC indicator instance.</returns>
        IIndicatorAC CreateIndicatorAC(string? symbol = null, Timeframe? timeframe = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates an Awesome Oscillator (AO) indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created AO indicator instance.</returns>
        IIndicatorAO CreateIndicatorAO(string? symbol = null, Timeframe? timeframe = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates an Alligator indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created Alligator indicator instance.</returns>
        IIndicatorAlligator CreateIndicatorAlligator(string? symbol = null, Timeframe? timeframe = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Gator Oscillator indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created Gator indicator instance.</returns>
        IIndicatorGator CreateIndicatorGator(string? symbol = null, Timeframe? timeframe = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates an Envelopes indicator for upper/lower band trading envelopes.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the envelopes calculation (optional).</param>
        /// <param name="deviation">The band deviation multiplier (optional).</param>
        /// <param name="method">The moving average method for smoothing (optional).</param>
        /// <param name="appliedPrice">The source price type to use (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created Envelopes indicator instance.</returns>
        IIndicatorEnvelopes CreateIndicatorEnvelopes(string? symbol = null, Timeframe? timeframe = null, int? period = null, double? deviation = null, MaMethod? method = null, AppliedPrice? appliedPrice = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Force Index indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the Force Index calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created Force indicator instance.</returns>
        IIndicatorForce CreateIndicatorForce(string? symbol = null, Timeframe? timeframe = null, int? period = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Fractals indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created Fractals indicator instance.</returns>
        IIndicatorFractals CreateIndicatorFractals(string? symbol = null, Timeframe? timeframe = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Bears Power indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the Bears Power calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created Bears Power indicator instance.</returns>
        IIndicatorBears CreateIndicatorBears(string? symbol = null, Timeframe? timeframe = null, int? period = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Bulls Power indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the Bulls Power calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created Bulls Power indicator instance.</returns>
        IIndicatorBulls CreateIndicatorBulls(string? symbol = null, Timeframe? timeframe = null, int? period = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Chaikin Oscillator indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="fast">The fast moving average period (optional).</param>
        /// <param name="slow">The slow moving average period (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created Chaikin indicator instance.</returns>
        IIndicatorChaikin CreateIndicatorChaikin(string? symbol = null, Timeframe? timeframe = null, int? fast = null, int? slow = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Money Flow Index (MFI) indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the MFI calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created MFI indicator instance.</returns>
        IIndicatorMFI CreateIndicatorMFI(string? symbol = null, Timeframe? timeframe = null, int? period = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Relative Vigor Index (RVI) indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the RVI calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created RVI indicator instance.</returns>
        IIndicatorRVI CreateIndicatorRVI(string? symbol = null, Timeframe? timeframe = null, int? period = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

#pragma warning disable CA1716
        /// <summary>
        /// Creates a Parabolic SAR indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="step">The step increment used by the SAR calculation (optional).</param>
        /// <param name="maximum">The maximum step used by the SAR calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created SAR indicator instance.</returns>
        IIndicatorSAR CreateIndicatorSAR(string? symbol = null, Timeframe? timeframe = null, double? step = null, double? maximum = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);
#pragma warning restore CA1716

        /// <summary>
        /// Creates a Commodity Channel Index (CCI) indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the CCI calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created CCI indicator instance.</returns>
        IIndicatorCCI CreateIndicatorCCI(string? symbol = null, Timeframe? timeframe = null, int? period = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Triple Exponential Moving Average (TEMA) indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the TEMA calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created TEMA indicator instance.</returns>
        IIndicatorTEMA CreateIndicatorTEMA(string? symbol = null, Timeframe? timeframe = null, int? period = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates an Adaptive Moving Average (AMA) indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the AMA calculation (optional).</param>
        /// <param name="fast">The fast smoothing period (optional).</param>
        /// <param name="slow">The slow smoothing period (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created AMA indicator instance.</returns>
        IIndicatorAMA CreateIndicatorAMA(string? symbol = null, Timeframe? timeframe = null, int? period = null, int? fast = null, int? slow = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Double Exponential Moving Average (DEMA) indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the DEMA calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created DEMA indicator instance.</returns>
        IIndicatorDEMA CreateIndicatorDEMA(string? symbol = null, Timeframe? timeframe = null, int? period = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Momentum indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the Momentum calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created Momentum indicator instance.</returns>
        IIndicatorMomentum CreateIndicatorMomentum(string? symbol = null, Timeframe? timeframe = null, int? period = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates an Average Directional Index (ADX) indicator for measuring trend strength.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe for the indicator (optional).</param>
        /// <param name="period">The period for the ADX calculation (optional).</param>
        /// <param name="indicatorAlias">An alias for the indicator instance (optional).</param>
        /// <param name="propertyOptions">Optional action to configure indicator properties.</param>
        /// <returns>The created ADX indicator instance.</returns>
        IIndicatorADX CreateIndicatorADX(string? symbol = null, Timeframe? timeframe = null, int? period = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);
        /// <summary>
        /// Creates a Bollinger Bands indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe (optional).</param>
        /// <param name="period">The period for the bands (optional).</param>
        /// <param name="multiplier">The multiplier for the bands (optional).</param>
        /// <param name="indicatorAlias">The alias for the indicator (optional).</param>
        /// <param name="propertyOptions">Optional property configuration action.</param>
        /// <returns>The created Bollinger Bands indicator.</returns>
        IIndicatorBollingerBands CreateIndicatorBollingerBands(string? symbol = null, Timeframe? timeframe = null, int? period = null, double? multiplier = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Bollinger Bands %B indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe (optional).</param>
        /// <param name="period">The period for the bands (optional).</param>
        /// <param name="multiplier">The multiplier for the bands (optional).</param>
        /// <param name="indicatorAlias">The alias for the indicator (optional).</param>
        /// <param name="propertyOptions">Optional property configuration action.</param>
        /// <returns>The created Bollinger Bands %B indicator.</returns>
        IIndicatorBollingerPercentB CreateIndicatorBollingerPercentB(string? symbol = null, Timeframe? timeframe = null, int? period = null, double? multiplier = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Bollinger Band Width indicator.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe (optional).</param>
        /// <param name="period">The period for the bands (optional).</param>
        /// <param name="multiplier">The multiplier for the bands (optional).</param>
        /// <param name="indicatorAlias">The alias for the indicator (optional).</param>
        /// <param name="propertyOptions">Optional property configuration action.</param>
        /// <returns>The created Bollinger Band Width indicator.</returns>
        IIndicatorBollingerBandWidth CreateIndicatorBollingerBandWidth(string? symbol = null, Timeframe? timeframe = null, int? period = null, double? multiplier = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Smoothed RSI indicator (RSI smoothed by MA).
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe (optional).</param>
        /// <param name="rsiPeriod">The period for the RSI calculation (optional).</param>
        /// <param name="maPeriod">The period for the smoothing moving average (optional).</param>
        /// <param name="maMethod">The moving average method for smoothing (optional).</param>
        /// <param name="indicatorAlias">The alias for the indicator (optional).</param>
        /// <param name="propertyOptions">Optional property configuration action.</param>
        /// <returns>The created Smoothed RSI indicator.</returns>
        IIndicator CreateIndicatorSmoothedRSI(string? symbol = null, Timeframe? timeframe = null, int? rsiPeriod = null, int? maPeriod = null, MaMethod? maMethod = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates a Difference indicator between two data sources.
        /// </summary>
        /// <param name="sourceId1">The ID of the first data source.</param>
        /// <param name="sourceId2">The ID of the second data source.</param>
        /// <param name="indicatorAlias">The alias for the indicator (optional).</param>
        /// <param name="propertyOptions">Optional property configuration action.</param>
        /// <returns>The created Difference indicator.</returns>
        IIndicatorDiff CreateIndicatorDiff(string sourceId1, string sourceId2, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);

        /// <summary>
        /// Creates an MA Spread indicator (difference between two MAs).
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe (optional).</param>
        /// <param name="fastPeriod">The period for the fast MA (default: 10).</param>
        /// <param name="slowPeriod">The period for the slow MA (default: 20).</param>
        /// <param name="method">The moving average method (default: SMA).</param>
        /// <param name="indicatorAlias">The alias for the indicator (optional).</param>
        /// <returns>The created MA Spread indicator.</returns>
        IIndicator CreateIndicatorMASpread(string? symbol = null, Timeframe? timeframe = null, int fastPeriod = 10, int slowPeriod = 20, MaMethod method = MaMethod.SMA, string? indicatorAlias = null);

        /// <summary>
        /// Creates an indicator for the distance between the current price and a higher timeframe MA.
        /// </summary>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="htf">The higher timeframe (optional).</param>
        /// <param name="htfPeriod">The period for the higher timeframe MA (default: 200).</param>
        /// <param name="indicatorAlias">The alias for the indicator (optional).</param>
        /// <returns>The created price-to-HTF-MA distance indicator.</returns>
        IIndicator CreateIndicatorPriceHTFDiff(string? symbol = null, Timeframe? htf = null, int htfPeriod = 200, string? indicatorAlias = null);

        /// <summary>
        /// Creates a custom indicator loaded from an external plugin DLL.
        /// </summary>
        /// <param name="customName">The registered name of the custom indicator.</param>
        /// <param name="symbol">The trading symbol (optional).</param>
        /// <param name="timeframe">The timeframe (optional).</param>
        /// <param name="parameters">Dictionary of parameters (key-value pairs).</param>
        /// <param name="indicatorAlias">The alias for the indicator (optional).</param>
        /// <param name="propertyOptions">Optional property configuration action.</param>
        /// <returns>The created custom indicator.</returns>
        IIndicator CreateCustomIndicator(string customName, string? symbol = null, Timeframe? timeframe = null, Dictionary<string, object>? parameters = null, string? indicatorAlias = null, Action<IndicatorProperty>? propertyOptions = null);
        /// <summary>
        /// Gets the current system time.
        /// </summary>
        /// <returns>The current system time.</returns>
        DateTime GetCurrentTime();
        #endregion
    }
}
