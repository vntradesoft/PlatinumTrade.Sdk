namespace Pt.Okx.Sdk.Clients.Trading.Enums
{
    /// <summary>
    /// Reasons or styles for closing a position.
    /// </summary>
    public enum ClosingPositionType
    {
        /// <summary>Close part of the position.</summary>
        ClosePartially,

        /// <summary>Close the entire position.</summary>
        CloseAll,

        /// <summary>Close caused by liquidation.</summary>
        Liquidation,

        /// <summary>Partial liquidation close.</summary>
        PartialLiquidation,

        /// <summary>Auto-deleveraging close.</summary>
        ADL
    }
}
