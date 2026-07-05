using Pt.Okx.Sdk.Clients.Market.Enums;

namespace Pt.Okx.Sdk.Clients.Market.Models
{
    /// <summary>
    /// Configuration for synthetic tick generation during backtests.
    /// This does not affect real-time tick sources.
    /// </summary>
    public class TickSourceConfig
    {
        /// <summary>
        /// Gets or sets the number of generated ticks per one-minute candle.
        /// Lower values are faster; higher values are more precise.
        /// Recommended range: 10 to 60.
        /// </summary>
        public int TicksPerCandle { get; set; } = 30;

        /// <summary>
        /// Gets or sets whether random noise is applied to intermediate prices.
        /// Enabled is more realistic; disabled is deterministic and better for tests.
        /// </summary>
        public bool UseNoise { get; set; } = true;

        /// <summary>
        /// Gets or sets the maximum noise amplitude as a fraction of the candle spread.
        /// For example, 0.1 means up to 10% of High - Low.
        /// </summary>
        public double NoiseAmplitude { get; set; } = 0.1;

        /// <summary>
        /// Gets or sets the random seed used for reproducible backtests.
        /// Null means a new random sequence for each run.
        /// </summary>
        public int? RandomSeed { get; set; } = 42;

        /// <summary>
        /// Gets or sets the generated tick path pattern inside each OHLC candle.
        /// </summary>
        public OhlcPattern OhlcPattern { get; set; } = OhlcPattern.Auto;
    }
}
