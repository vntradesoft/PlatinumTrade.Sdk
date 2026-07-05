namespace Pt.Okx.Sdk.Enums
{

    /// <summary>
    /// OKX API operation names used for logging and error context.
    /// </summary>
    public enum ApiName
    {
        /// <summary>Place an order.</summary>
        PlaceOrder,

        /// <summary>Cancel an order.</summary>
        CancelOrder,

        /// <summary>Cancel multiple orders.</summary>
        CancelMultipleOrder,

        /// <summary>Modify an order.</summary>
        ModifyOrder,

        /// <summary>Place an algorithmic order.</summary>
        PlaceAlgoOrder,

        /// <summary>Cancel an algorithmic order.</summary>
        CancelAlgoOrder,

        /// <summary>Close a position.</summary>
        ClosePosition,

        /// <summary>Get open orders.</summary>
        GetOrders,

        /// <summary>Get positions.</summary>
        GetPositions,

        /// <summary>Get one algorithmic order.</summary>
        GetAlgoOrder,

        /// <summary>Get historical orders.</summary>
        GetHistoryOrders,

        /// <summary>Get historical algorithmic orders.</summary>
        GetHistoryAlgoOrders,

        /// <summary>Get historical positions.</summary>
        GetHistoryPositions,

        /// <summary>Get user trades.</summary>
        GetUserTrade,

        /// <summary>Get one order.</summary>
        GetOrder,

        /// <summary>Amend an algorithmic order.</summary>
        AmendAlgoOrder,

        /// <summary>Amend an order.</summary>
        AmendOrder,

        /// <summary>Get algorithmic orders.</summary>
        GetAlgoOrders,

        /// <summary>Check an order before placement.</summary>
        OrderCheck,

        /// <summary>Get archived user trades.</summary>
        GetUserTradesArchive,

        /// <summary>Get archived orders.</summary>
        GetOrdersArchive,
    }
}
