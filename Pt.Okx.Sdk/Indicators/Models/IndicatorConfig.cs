using Pt.Okx.Sdk.Enums;
using Pt.Okx.Sdk.Indicators.Enums;

namespace Pt.Okx.Sdk.Indicators.Models
{
    /// <summary>
    /// Describes one data source used by an indicator instance.
    /// The source can be either candle price data or another indicator buffer.
    /// </summary>
    public class IndicatorSource
    {
        /// <summary>
        /// Gets or sets the parent indicator ID.
        /// When <c>null</c>, the source is interpreted as candle price data.
        /// </summary>
        public string? IndicatorId { get; set; }

        /// <summary>
        /// Gets or sets the parent indicator timeframe when it differs from the current indicator timeframe.
        /// </summary>
        public Timeframe? TimeFrame { get; set; }

        /// <summary>
        /// Gets or sets the parent indicator buffer index to read from.
        /// </summary>
        public int BufferIndex { get; set; }

        /// <summary>
        /// Gets or sets the candle price field used when no parent indicator is configured.
        /// </summary>
        public AppliedPrice AppliedPrice { get; set; } = AppliedPrice.Close;

        /// <summary>
        /// Returns a compact, deterministic textual representation of this source.
        /// Used by fingerprint generation in <see cref="IndicatorConfig"/>.
        /// </summary>
        /// <returns>A stable key segment representing this source.</returns>
        public override string ToString()
        {
            var tf = TimeFrame.HasValue ? $":{TimeFrame}" : "";
            return IndicatorId != null ? $"Ind:{IndicatorId}{tf}[{BufferIndex}]" : $"Price:{AppliedPrice}";
        }
    }

    /// <summary>
    /// Represents the runtime configuration used to create and identify an indicator instance.
    /// This includes the trading symbol, timeframe, indicator type, parameters,
    /// and optional input sources for chained indicators.
    /// </summary>
    public class IndicatorConfig
    {
        /// <summary>
        /// Gets the list of input sources consumed by the indicator.
        /// </summary>
        private readonly List<IndicatorSource> _sources = new();

        /// <summary>
        /// Gets the trading symbol this indicator is associated with.
        /// </summary>
        public string Symbol { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the timeframe in which the indicator is calculated.
        /// </summary>
        public Timeframe TimeFrame { get; private set; }

        /// <summary>
        /// Gets the indicator type.
        /// For custom indicators, this is typically <see cref="IndicatorType.CUSTOM"/>,
        /// and <see cref="CustomName"/> is used to distinguish the implementation.
        /// </summary>
        public IndicatorType IndicatorType { get; private set; }

        /// <summary>
        /// Gets or sets the parameter collection used by the indicator.
        /// </summary>
        public IndicatorParameters Parameters { get; set; } = new();

        /// <summary>
        /// Gets or sets the list of input sources consumed by the indicator.
        /// Each source may represent either a candle price or another indicator buffer.
        /// </summary>
        public IReadOnlyList<IndicatorSource> Sources => _sources;

        /// <summary>
        /// Gets or sets the custom indicator name.
        /// <c>null</c> indicates a built-in indicator.
        /// </summary>
        public string? CustomName { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="IndicatorConfig"/>.
        /// </summary>
        /// <param name="symbol">The trading symbol (e.g. <c>BTCUSDT</c>).</param>
        /// <param name="timeFrame">The timeframe for calculation.</param>
        /// <param name="indicatorType">The indicator type.</param>
        public IndicatorConfig(string symbol, Timeframe timeFrame, IndicatorType indicatorType)
        {
            Symbol = symbol;
            TimeFrame = timeFrame;
            IndicatorType = indicatorType;
        }

        /// <summary>
        /// Sets or overrides a parameter value.
        /// If <paramref name="value"/> is <c>null</c>, <paramref name="defaultValue"/> is used.
        /// </summary>
        /// <param name="key">Parameter key.</param>
        /// <param name="value">Runtime value (nullable).</param>
        /// <param name="defaultValue">Fallback value if <paramref name="value"/> is null.</param>
        /// <returns>The current configuration instance.</returns>
        public IndicatorConfig SetParam(string key, object? value, object defaultValue)
        {
            Parameters.Set(key, value, defaultValue);
            return this;
        }

        /// <summary>
        /// Adds an indicator source that reads from another indicator buffer.
        /// </summary>
        /// <param name="indicatorId">The source indicator identifier.</param>
        /// <param name="bufferIndex">The buffer index to read from. Default is <c>0</c>.</param>
        /// <param name="timeframe">Optional timeframe override for the source.</param>
        /// <returns>The current configuration instance.</returns>
        public IndicatorConfig AddSource(string indicatorId, int bufferIndex = 0, Timeframe? timeframe = null)
        {
            _sources.Add(new IndicatorSource
            {
                IndicatorId = indicatorId,
                BufferIndex = bufferIndex,
                TimeFrame = timeframe
            });
            return this;
        }

        /// <summary>
        /// Adds a source that reads directly from a candle price.
        /// </summary>
        /// <param name="price">The applied price field.</param>
        /// <returns>The current configuration instance.</returns>
        public IndicatorConfig AddSource(AppliedPrice price)
        {
            _sources.Add(new IndicatorSource { AppliedPrice = price });
            return this;
        }

        /// <summary>
        /// Gets the first source for backward compatibility with single-source usage.
        /// If no sources are defined, a default source instance is returned.
        /// </summary>
        public IndicatorSource Source =>
            _sources.Count > 0 ? _sources[0] : new IndicatorSource();

        /// <summary>
        /// Builds a deterministic fingerprint string used for caching and identity comparison.
        /// </summary>
        /// <returns>
        /// A stable key composed of symbol, timeframe, indicator type,
        /// custom name (if any), sources, and parameter hash.
        /// </returns>
        public string GetFingerprint()
        {
            var sourcesKey = string.Join("+", Sources.Select(s => s.ToString()));
            var customPart = CustomName != null ? $"|Custom:{CustomName}" : "";
            return $"{Symbol}|{TimeFrame}|{IndicatorType}{customPart}|{sourcesKey}|{Parameters.GetParametersHash()}";
        }
    }
}
