namespace Pt.Okx.Sdk.Strategy.Events
{
    /// <summary>
    /// Indicates whether <c>OnTickAsync</c> was triggered by an intra-bar tick or a closed bar.
    /// </summary>
    public enum TickPhase
    {
        /// <summary>
        /// Intra-bar tick update.
        /// </summary>
        Tick,

        /// <summary>
        /// Closed bar/candle update.
        /// </summary>
        BarClose
    }
}