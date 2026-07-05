namespace Pt.Okx.Sdk.Clients.Trading.Enums
{
    /// <summary>
    /// OKX order states.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>The order is active.</summary>
        Live,

        /// <summary>The order has been canceled.</summary>
        Canceled,

        /// <summary>The order is partially filled.</summary>
        PartiallyFilled,

        /// <summary>The order is fully filled.</summary>
        Filled,
    }
}
