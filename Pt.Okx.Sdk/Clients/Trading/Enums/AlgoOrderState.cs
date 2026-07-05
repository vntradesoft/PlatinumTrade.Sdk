namespace Pt.Okx.Sdk.Clients.Trading.Enums
{
    /// <summary>
    /// OKX algorithmic order lifecycle states.
    /// </summary>
    public enum AlgoOrderState
    {
        /// <summary>The order is active.</summary>
        Live,

        /// <summary>The order is paused.</summary>
        Pause,

        /// <summary>The order has taken effect.</summary>
        Effective,

        /// <summary>The order has partially taken effect.</summary>
        PartiallyEffective,

        /// <summary>The order has been canceled.</summary>
        Canceled,

        /// <summary>The order failed.</summary>
        Failed,
    }
}
