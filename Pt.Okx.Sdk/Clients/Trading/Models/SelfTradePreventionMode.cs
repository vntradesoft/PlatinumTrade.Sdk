namespace Pt.Okx.Sdk.Clients.Trading.Models
{

    /// <summary>
    /// Self-trade prevention modes supported by OKX.
    /// </summary>
    public enum SelfTradePreventionMode
    {
        /// <summary>Cancel the maker order.</summary>
        CancelMaker,

        /// <summary>Cancel the taker order.</summary>
        CancelTaker,

        /// <summary>Cancel both maker and taker orders.</summary>
        CancelBoth
    }
}
