using System.Text.Json.Serialization;

namespace Pt.Okx.Sdk.Clients.Market.Models
{
    /// <summary>
    /// Represents OHLCV data for a specific trading candle. 
    /// This record struct is designed for memory efficiency and cache locality in high-frequency scenarios.
    /// </summary>
    public readonly record struct CandleData : IComparable<CandleData>
    {
        /// <summary>Gets the timestamp of the candle.</summary>
        public DateTime Timestamp { get; init; }
        /// <summary>Gets the opening price.</summary>
        public decimal Open { get; init; }
        /// <summary>Gets the highest price during the period.</summary>
        public decimal High { get; init; }
        /// <summary>Gets the lowest price during the period.</summary>
        public decimal Low { get; init; }
        /// <summary>Gets the closing price.</summary>
        public decimal Close { get; init; }
        /// <summary>Gets the trading volume during the period.</summary>
        public decimal Volume { get; init; }
        /// <summary>Gets a value indicating whether the candle represents a completed time period.</summary>
        public bool IsCompleted { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CandleData"/> struct.
        /// </summary>
        /// <param name="timestamp">The timestamp of the candle.</param>
        /// <param name="open">The opening price.</param>
        /// <param name="high">The highest price.</param>
        /// <param name="low">The lowest price.</param>
        /// <param name="close">The closing price.</param>
        /// <param name="volume">The trading volume.</param>
        /// <param name="isCompleted">Whether the candle is completed.</param>
        public CandleData(DateTime timestamp, decimal open, decimal high, decimal low, decimal close, decimal volume, bool isCompleted = false)
        {
            Timestamp = timestamp;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
            IsCompleted = isCompleted;
        }

        /// <inheritdoc/>
        public int CompareTo(CandleData other)
        {
            return Timestamp.CompareTo(other.Timestamp);
        }

        /// <summary>
        /// Gets a value indicating whether the candle is empty (has a default timestamp).
        /// </summary>
        [JsonIgnore]
        public bool IsEmpty => Timestamp == default;

        /// <summary>
        /// Gets a representative empty <see cref="CandleData"/> instance.
        /// </summary>
        public static CandleData Empty => new CandleData(default, 0, 0, 0, 0, 0, false);

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Timestamp.ToLocalTime():yyyy-MM-dd HH:mm:ss}: O={Open} H={High} L={Low} C={Close} V={Volume} IsComplete={IsCompleted}";
        }

        /// <inheritdoc/>
        public static bool operator <(CandleData left, CandleData right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <inheritdoc/>
        public static bool operator <=(CandleData left, CandleData right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <inheritdoc/>
        public static bool operator >(CandleData left, CandleData right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <inheritdoc/>
        public static bool operator >=(CandleData left, CandleData right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
