namespace Pt.Okx.Sdk.Clients.Market.Enums
{
    /// <summary>
    /// Intrabar OHLC path used when replaying candle data as ticks.
    /// </summary>
    public enum OhlcPattern
    {
        /// <summary>Choose the path automatically from the candle direction.</summary>
        Auto,

        /// <summary>Replay as Open, High, Low, Close.</summary>
        HighFirst,

        /// <summary>Replay as Open, Low, High, Close.</summary>
        LowFirst,

        /// <summary>Choose a random path for each candle.</summary>
        Random
    }
}
