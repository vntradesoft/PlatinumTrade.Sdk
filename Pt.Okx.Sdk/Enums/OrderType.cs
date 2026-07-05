namespace Pt.Okx.Sdk.Enums
{
    /// <summary>
    /// OKX order types used by trading requests.
    /// </summary>
    public enum OrderType
    {
        /// <summary>Market order.</summary>
        Market,

        /// <summary>Limit order.</summary>
        Limit,

        /// <summary>Post-only order.</summary>
        PostOnly,

        /// <summary>Fill-or-kill order.</summary>
        FillOrKill,

        /// <summary>Immediate-or-cancel order.</summary>
        ImmediateOrCancel,

        /// <summary>Optimal limit order.</summary>
        OptimalLimitOrder,

        /// <summary>Market maker protection order.</summary>
        MarketMakerProtection,

        /// <summary>Post-only market maker protection order.</summary>
        MarketMakerProtectionPostOnly,

        /// <summary>Easy liquidity provider order.</summary>
        Elp
    }
}
