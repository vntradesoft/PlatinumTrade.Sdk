namespace Pt.Okx.Sdk.Clients.Trading.Enums
{
    /// <summary>
    /// OKX transaction types used by fills and account history mappers.
    /// </summary>
    public enum TransactionType
    {
        /// <summary>Unknown or unmapped transaction type.</summary>
        Unknown = 0,

        /// <summary>Buy transaction.</summary>
        Buy = 1,

        /// <summary>Sell transaction.</summary>
        Sell = 2,

        /// <summary>Open long position.</summary>
        OpenLong = 3,

        /// <summary>Open short position.</summary>
        OpenShort = 4,

        /// <summary>Close long position.</summary>
        CloseLong = 5,

        /// <summary>Close short position.</summary>
        CloseShort = 6,

        /// <summary>Partial liquidation closing a long position.</summary>
        PartialLiqCloseLong = 100,

        /// <summary>Partial liquidation closing a short position.</summary>
        PartialLiqCloseShort = 101,

        /// <summary>Partial liquidation buy transaction.</summary>
        PartialLiqBuy = 102,

        /// <summary>Partial liquidation sell transaction.</summary>
        PartialLiqSell = 103,

        /// <summary>Liquidation of a long position.</summary>
        LiqLong = 104,

        /// <summary>Liquidation of a short position.</summary>
        LiqShort = 105,

        /// <summary>Liquidation buy transaction.</summary>
        LiqBuy = 106,

        /// <summary>Liquidation sell transaction.</summary>
        LiqSell = 107,

        /// <summary>Liquidation transfer in.</summary>
        LiqTransferIn = 110,

        /// <summary>Deprecated misspelling of <see cref="LiqTransferIn"/>.</summary>
        [Obsolete("Use LiqTransferIn.")]
        LiqTranferIn = LiqTransferIn,

        /// <summary>Liquidation transfer out.</summary>
        LiqTransferOut = 111,

        /// <summary>Deprecated misspelling of <see cref="LiqTransferOut"/>.</summary>
        [Obsolete("Use LiqTransferOut.")]
        LiqTranferOut = LiqTransferOut,

        /// <summary>Delivery transaction for a long position.</summary>
        DeliveryLong = 112,

        /// <summary>Delivery transaction for a short position.</summary>
        DeliveryShort = 113,

        /// <summary>Auto-deleveraging close of a long position.</summary>
        AdlCloseLong = 125,

        /// <summary>Auto-deleveraging close of a short position.</summary>
        AdlCloseShort = 126,

        /// <summary>Auto-deleveraging buy transaction.</summary>
        AdlBuy = 127,

        /// <summary>Auto-deleveraging sell transaction.</summary>
        AdlSell = 128
    }
}
