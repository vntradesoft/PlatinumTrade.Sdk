namespace Pt.Okx.Sdk.Clients.Trading.Enums
{
    /// <summary>
    /// Account position mode.
    /// </summary>
    public enum PositionMode
    {
        /// <summary>Long and short positions are tracked separately.</summary>
        LongShortMode,

        /// <summary>Positions are tracked as a single net position.</summary>
        NetMode
    }
}
