namespace Pt.Okx.Sdk.Strategy.Events
{
    /// <summary>
    /// Event types published to strategies.
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

        /// <summary>Kline or candle update event.</summary>
        Kline,

        /// <summary>Transaction update event.</summary>
        Transaction,

        /// <summary>Trade command event from Telegram or another external source.</summary>
        TradeCommand,

        /// <summary>Tick update event.</summary>
        Tick
    }
}
