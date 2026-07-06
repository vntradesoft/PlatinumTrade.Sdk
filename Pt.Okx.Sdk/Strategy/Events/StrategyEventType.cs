namespace Pt.Okx.Sdk.Strategy.Events
{
    /// <summary>
    /// Event types used internally by engines for dispatch.
    /// </summary>
    public enum StrategyEventType
    {
        /// <summary>Order update event.</summary>
        Order,

        /// <summary>Balance update event.</summary>
        Balance,

        /// <summary>Position update event.</summary>
        Position,

        /// <summary>Algorithmic order update event.</summary>
        AlgoOrder,

        /// <summary>
        /// Kline or candle close event.
        /// Mapped to <see cref="TickPhase.BarClose"/> when invoking <c>OnTickAsync</c>.
        /// </summary>
        Kline,

        /// <summary>Transaction update event.</summary>
        Transaction,

        /// <summary>Trade command event from Telegram or another external source.</summary>
        TradeCommand,

        /// <summary>
        /// Intra-bar tick event.
        /// Mapped to <see cref="TickPhase.Tick"/> when invoking <c>OnTickAsync</c>.
        /// </summary>
        Tick
    }
}
