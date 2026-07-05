using Pt.Okx.Sdk.Enums;

namespace Pt.Okx.Sdk.Clients.Instruments.Models
{
    /// <summary>
    /// Represents the limit price information for a specific trading instrument.
    /// </summary>
    public record LimitPrice
    {
        /// <summary>
        /// ["<c>instId</c>"] Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>instType</c>"] Instrument type
        /// </summary>
        public InstrumentType InstrumentType { get; set; }

        /// <summary>
        /// ["<c>buyLmt</c>"] Buy limit
        /// </summary>
        public decimal BuyLimit { get; set; }

        /// <summary>
        /// ["<c>sellLmt</c>"] Sell limit
        /// </summary>
        public decimal SellLimit { get; set; }

        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// ["<c>enabled</c>"] Whether price limit is enabled
        /// </summary>
        public bool IsEnabled { get; set; }
    }

}
