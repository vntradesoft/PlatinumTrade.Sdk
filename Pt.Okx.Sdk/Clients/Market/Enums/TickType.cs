namespace Pt.Okx.Sdk.Clients.Market.Enums
{
    /// <summary>
    /// Represents the role of a tick within a candle during replay or real-time streaming.
    /// </summary>
#pragma warning disable CA1028
    public enum TickType : byte
    {
#pragma warning restore CA10
        /// <summary>
        /// The opening tick of a candle (Open price).
        /// </summary>
        Open,

        /// <summary>
        /// The tick representing the highest price within the candle.
        /// </summary>
        High,

        /// <summary>
        /// The tick representing the lowest price within the candle.
        /// </summary>
        Low,

        /// <summary>
        /// The closing tick of a candle (Close price).
        /// </summary>
        Close,

        /// <summary>
        /// An interpolated tick within the candle, not directly from market data.
        /// Used for simulation or smoothing between OHLC points.
        /// </summary>
        Mid,

        /// <summary>
        /// A tick received directly from a real-time market data stream.
        /// </summary>
        Realtime
    }
}
