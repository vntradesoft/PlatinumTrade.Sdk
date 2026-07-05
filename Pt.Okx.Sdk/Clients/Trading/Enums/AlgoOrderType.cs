namespace Pt.Okx.Sdk.Clients.Trading.Enums
{
    /// <summary>
    /// OKX algorithmic order types exposed by the SDK mapper layer.
    /// </summary>
    public enum AlgoOrderType
    {
        /// <summary>A conditional take-profit or stop-loss order.</summary>
        Conditional,

        /// <summary>One-cancels-the-other order.</summary>
        OCO,

        /// <summary>Trigger order.</summary>
        Trigger,

        /// <summary>Trailing order.</summary>
        TrailingOrder,

        /// <summary>Iceberg order.</summary>
        Iceberg,

        /// <summary>Time-weighted average price order.</summary>
        TWAP,

        /// <summary>Chase order.</summary>
        Chase
    }
}
