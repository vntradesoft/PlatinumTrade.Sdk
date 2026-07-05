namespace Pt.Okx.Sdk.Clients.Trading.Enums
{
    /// <summary>
    /// Currency unit transitions returned by OKX order checks.
    /// </summary>
    public enum CheckUnitType
    {
        /// <summary>Base currency before and after placing the order.</summary>
        BaseBoth,

        /// <summary>Base currency before placing the order, quote currency after placing it.</summary>
        BaseBeforeQuoteAfter,

        /// <summary>Quote currency before placing the order, base currency after placing it.</summary>
        QuoteBeforeBaseAfter,

        /// <summary>Quote currency before and after placing the order.</summary>
        QuoteBoth
    }
}
