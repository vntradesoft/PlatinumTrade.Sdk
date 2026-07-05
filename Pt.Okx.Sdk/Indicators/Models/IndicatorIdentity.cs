using Pt.Okx.Sdk.Enums;
using Pt.Okx.Sdk.Indicators.Enums;

namespace Pt.Okx.Sdk.Indicators.Models
{
    /// <summary>
    /// Represents the logical identity of an indicator instance.
    /// Combines symbol, timeframe, type, and parameter values.
    /// </summary>
    public class IndicatorIdentity
    {
        /// <summary>
        /// Gets or sets the trading symbol for the indicator instance.
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the timeframe where the indicator is calculated.
        /// </summary>
        public Timeframe TimeFrame { get; set; }

        /// <summary>
        /// Gets or sets the indicator type.
        /// </summary>
        public IndicatorType IndicatorType { get; set; }

        /// <summary>
        /// Gets or sets the parameter bag used by this identity.
        /// </summary>
        public IndicatorParameters Parameters { get; set; } = new();

        /// <summary>
        /// Generates a compact unique ID string from identity fields and parameter hash.
        /// </summary>
        /// <returns>A deterministic identifier suitable for dictionary keys.</returns>
        public string GenerateUniqueId()
        {
            var paramsHash = Parameters.GetParametersHash();
            return $"{Symbol}_{TimeFrame}_{IndicatorType}_{paramsHash}";
        }

        /// <summary>
        /// Returns a human-readable display name including inline parameters.
        /// </summary>
        /// <returns>A display string such as <c>RSI(Period=14)@BTCUSDT_H1</c>.</returns>
        public string GetDisplayName()
        {
            var paramsList = Parameters.All
                .Select(kvp => $"{kvp.Key}={kvp.Value}")
                .ToArray();
            var paramsStr = paramsList.Length > 0 ? $"({string.Join(",", paramsList)})" : "";
            return $"{IndicatorType}{paramsStr}@{Symbol}_{TimeFrame}";
        }
    }
}
