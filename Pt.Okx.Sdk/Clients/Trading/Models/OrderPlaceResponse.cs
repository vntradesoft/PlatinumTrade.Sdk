using Pt.Okx.Sdk.Clients.Trading.Enums;
using Pt.Okx.Sdk.Enums;

namespace Pt.Okx.Sdk.Clients.Trading.Models;

/// <summary>
/// Represents the response after placing an order.
/// </summary>
public record OrderPlaceResponse
{
    /// <summary>Order id</summary>
    public long? OrderId { get; set; }

    /// <summary>Client order id</summary>
    public string? ClientOrderId { get; set; }

    /// <summary>["<c>tag</c>"] Tag</summary>
    public string Tag { get; set; } = string.Empty;

    /// <summary>Code</summary>
    public int Code { get; set; }

    /// <summary>Message</summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>Sub code</summary>
    public string SubCode { get; set; } = string.Empty;

    /// <summary>Timestamp</summary>
    public DateTime? Timestamp { get; set; }

    /// <summary>Whether order placement was successful</summary>
    public bool Success => Code == 0;
}

/// <summary>
/// Represents the response after placing an algorithmic order.
/// </summary>
public record AlgoOrderResponse
{
    /// <summary>["<c>algoId</c>"] Algo order id</summary>
    public string? AlgoOrderId { get; set; }

    /// <summary>["<c>clOrdId</c>"] Client order id</summary>
    public string? ClientOrderId { get; set; }

    /// <summary>["<c>algoClOrdId</c>"] Algo client order id</summary>
    public string? AlgoClientOrderId { get; set; }

    /// <summary>
    /// Deprecated misspelling of <see cref="AlgoClientOrderId"/>.
    /// </summary>
    [Obsolete("Use AlgoClientOrderId.")]
    public string? AgloClientOrderId
    {
        get => AlgoClientOrderId;
        set => AlgoClientOrderId = value;
    }

    /// <summary>["<c>sCode</c>"] Code</summary>
    public int Code { get; set; }

    /// <summary>["<c>sMsg</c>"] Message</summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>Whether order placement was successful</summary>
    public bool Success => Code == 0;
}

/// <summary>
/// Check order info
/// </summary>
public record CheckOrderResponse
{
    /// <summary>["<c>adjEq</c>"] Current adjusted / Effective equity in USD</summary>
    public decimal AdjustedEquity { get; set; }

    /// <summary>["<c>adjEqChg</c>"] After placing order, changed quantity of adjusted / Effective equity in USD</summary>
    public decimal AdjustedEquityChange { get; set; }

    /// <summary>["<c>availBal</c>"] Current available balance in margin coin currency, only applicable to turn auto borrow off</summary>
    public decimal AvailableBalance { get; set; }

    /// <summary>["<c>availBalChg</c>"] After placing order, changed quantity of available balance after placing order, only applicable to turn auto borrow off</summary>
    public decimal AvailableBalanceChange { get; set; }

    /// <summary>["<c>imr</c>"] Current initial margin requirement in USD</summary>
    public decimal InitialMarginRequirement { get; set; }

    /// <summary>["<c>imrChg</c>"] After placing order, changed quantity of initial margin requirement in USD</summary>
    public decimal InitialMarginRequirementChange { get; set; }

    /// <summary>["<c>liab</c>"] Current liabilities of currency. For cross, it is cross liabilities. For isolated position, it is isolated liabilities</summary>
    public decimal Liabilities { get; set; }

    /// <summary>["<c>liabChg</c>"] After placing order, changed quantity of liabilities</summary>
    public decimal LiabilitiesChange { get; set; }

    /// <summary>["<c>liabChgCcy</c>"] After placing order, the unit of changed liabilities quantity. Only applicable cross and in auto borrow</summary>
    public string? LiabilitiesChangeAsset { get; set; }

    /// <summary>["<c>liqPx</c>"] Current estimated liquidation price</summary>
    public decimal LiquidationPrice { get; set; }

    /// <summary>["<c>liqPxDiff</c>"] After placing order, the distance between estimated liquidation price and mark price</summary>
    public string LiquidationPriceDifference { get; set; } = string.Empty;

    /// <summary>["<c>liqPxDiffRatio</c>"] After placing order, the distance rate between estimated liquidation price and mark price</summary>
    public decimal LiquidationPriceDifferenceRatio { get; set; }

    /// <summary>["<c>mgnRatio</c>"] Current margin ratio in USD</summary>
    public decimal MarginRatio { get; set; }

    /// <summary>["<c>mgnRatioChg</c>"] After placing order, changed quantity of margin ratio in USD</summary>
    public decimal MarginRatioChange { get; set; }

    /// <summary>["<c>mmr</c>"] Current Maintenance margin requirement in USD</summary>
    public decimal MaintenanceMarginRequirement { get; set; }

    /// <summary>["<c>mmrChg</c>"] After placing order, changed quantity of maintenance margin requirement in USD</summary>
    public decimal MaintenanceMarginRequirementChange { get; set; }

    /// <summary>["<c>posBal</c>"] Current positive asset, only applicable to margin isolated position</summary>
    public string? PositionBalance { get; set; }

    /// <summary>["<c>posBalChg</c>"] After placing order, positive asset of margin isolated, only applicable to margin isolated position</summary>
    public string? PositionBalanceChange { get; set; }

    /// <summary>["<c>type</c>"] Unit type</summary>
    public CheckUnitType? Type { get; set; }
}

/// <summary>
/// Order amend response
/// </summary>
public record OrderAmendResponse
{
    /// <summary>
    /// ["<c>ordId</c>"] Order id
    /// </summary>
    public long? OrderId { get; set; }

    /// <summary>
    /// ["<c>clOrdId</c>"] Client order id
    /// </summary>
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// ["<c>reqId</c>"] Request id
    /// </summary>
    public string RequestId { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>sCode</c>"] Code
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    public DateTime? Timestamp { get; set; }

    /// <summary>
    /// ["<c>sMsg</c>"] Message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>subCode</c>"] Sub code
    /// </summary>
    public string SubCode { get; set; } = string.Empty;

    /// <summary>
    /// Whether order edit was successful
    /// </summary>
    public bool Success => Code == 0;
}

/// <summary>
/// Order amend response
/// </summary>
public record AlgoOrderAmendResponse
{
    /// <summary>
    /// ["<c>algoId</c>"] Order id
    /// </summary>
    public long? AlgoOrderId { get; set; }

    /// <summary>
    /// ["<c>algoClOrdId</c>"] Client order id
    /// </summary>
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// ["<c>reqId</c>"] Request id
    /// </summary>
    public string RequestId { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>sCode</c>"] Code
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>sMsg</c>"] Message
    /// </summary>
    public string Message { get; set; } = string.Empty;
}

/// <summary>
/// Cancel response
/// </summary>
public record OrderCancelResponse
{
    /// <summary>
    /// ["<c>ordId</c>"] Order id
    /// </summary>
    public long? OrderId { get; set; }

    /// <summary>
    /// ["<c>clOrdId</c>"] Client order id
    /// </summary>
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>sCode</c>"] Code
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// ["<c>sMsg</c>"] Message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    public DateTime? Timestamp { get; set; }

    /// <summary>
    /// Whether order cancellation was successful
    /// </summary>
    public bool Success => Code == 0;
}

/// <summary>
/// Algo order request
/// </summary>
public record AlgoOrderRequest
{
    /// <summary>
    /// ["<c>algoId</c>"] Algo order id
    /// </summary>
    public string? AlgoOrderId { get; set; }

    /// <summary>
    /// ["<c>algoClOrdId</c>"] Client algo order id
    /// </summary>
    public string? ClientAlgoOrderId { get; set; }

    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    public string Symbol { get; set; } = string.Empty;
}

/// <summary>
/// Cancel request
/// </summary>
public record OrderCancelRequest
{
    /// <summary>
    /// ["<c>instId</c>"] Symbol name
    /// </summary>
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>ordId</c>"] Order id
    /// </summary>
    public string? OrderId { get; set; }

    /// <summary>
    /// ["<c>clOrdId</c>"] Client order id
    /// </summary>
    public string? ClientOrderId { get; set; }
}



/// <summary>
/// Order place request
/// </summary>
public record OrderPlaceRequest
{
    /// <summary>
    /// ["<c>instIdCode</c>"] Symbol code
    /// </summary>
    public long SymbolCode { get; set; }

    /// <summary>
    /// ["<c>tdMode</c>"] Trade mode
    /// </summary>
    public TradeMode TradeMode { get; set; }

    /// <summary>
    /// ["<c>side</c>"] Order side
    /// </summary>
    public OrderSide OrderSide { get; set; }

    /// <summary>
    /// ["<c>posSide</c>"] Position side
    /// </summary>
    public PositionSide? PositionSide { get; set; }

    /// <summary>
    /// ["<c>ordType</c>"] Order type
    /// </summary>
    public OrderType OrderType { get; set; }

    /// <summary>
    /// ["<c>sz</c>"] Quantity
    /// </summary>
    public decimal? Quantity { get; set; }

    /// <summary>
    /// ["<c>px</c>"] Price
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>clOrdId</c>"] Client order id
    /// </summary>
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// ["<c>tag</c>"] Tag
    /// </summary>
    public string? Tag { get; set; }

    /// <summary>
    /// ["<c>reduceOnly</c>"] Reduce only
    /// </summary>
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// ["<c>tgtCcy</c>"] Quantity type
    /// </summary>
    public QuantityAsset? QuantityType { get; set; }

    /// <summary>
    /// ["<c>pxUsd</c>"] Place options orders in USD, only applicable to options
    /// </summary>
    public decimal? PriceUsd { get; set; }

    /// <summary>
    /// ["<c>pxVol</c>"] Place options orders based on implied volatility, where 1 represents 100%. Only applicable to OPTIONS
    /// </summary>
    public decimal? PriceVol { get; set; }

    /// <summary>
    /// ["<c>banAmend</c>"] Whether to disallow the system from amending the size of the SPOT Market Order. If true, system will not amend and reject the market order if user does not have sufficient funds.
    /// </summary>
    public bool? BanAmend { get; set; }

    /// <summary>
    /// ["<c>stpMode</c>"] Self trade prevention mode
    /// </summary>
    public SelfTradePreventionMode? StpMode { get; set; }

    /// <summary>
    /// ["<c>tradeQuoteCcy</c>"] The quote currency used for trading. Only applicable to SPOT. The default value is the quote currency of the symbol, for example: for BTC-USD, the default is USD.
    /// </summary>
    public string? TradeQuoteAsset { get; set; }

    /// <summary>
    /// ["<c>attachAlgoOrds</c>"] Attached take profit / stop loss orders
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Performance", "CA1819",
        Justification = "DTO property, array is required for serialization")]
    public AttachedAlgoOrder[]? AttachedAlgoOrders { get; set; }
}


/// <summary>
/// Algo order attached to an order
/// </summary>
public record AttachedAlgoOrder
{
    /// <summary>
    /// ["<c>attachAlgoClOrdId</c>"] Client order id
    /// </summary>
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// ["<c>tpTriggerPx</c>"] Take profit trigger price
    /// </summary>
    public decimal? TakeProfitTriggerPrice { get; set; }
    /// <summary>
    /// ["<c>tpOrdPx</c>"] Take profit order price
    /// </summary>
    public decimal? TakeProfitOrderPrice { get; set; }
    /// <summary>
    /// ["<c>tpOrdKind</c>"] Take profit order kind
    /// </summary>
    public TriggerOrderKind? TakeProfitOrderKind { get; set; }
    /// <summary>
    /// ["<c>tpTriggerPxType</c>"] Take profit price type
    /// </summary>
    public TriggerPriceType? TakeProfitPriceType { get; set; }
    /// <summary>
    /// ["<c>sz</c>"] Take profit quantity
    /// </summary>
    public decimal? TakeProfitQuantity { get; set; }

    /// <summary>
    /// ["<c>slTriggerPx</c>"] Stop loss trigger price
    /// </summary>
    public decimal? StopLossTriggerPrice { get; set; }
    /// <summary>
    /// ["<c>slOrdPx</c>"] Stop loss order price
    /// </summary>
    public decimal? StopLossOrderPrice { get; set; }
    /// <summary>
    /// ["<c>slTriggerPxType</c>"] Stop loss price type
    /// </summary>
    public TriggerPriceType? StopLossPriceType { get; set; }

    /// <summary>
    /// ["<c>amendPxOnTriggerType</c>"] Whether to enable Cost-price SL. Only applicable to SL order of split TPs. Whether slTriggerPx will move to avgPx when the first TP order is triggered, 0: disable, the default value, 1: Enable
    /// </summary>
    public string? AmendPriceOnTriggerType { get; set; }

    /// <summary>
    /// ["<c>callbackRatio</c>"] Callback ratio, e.g. 0.05 represents 5%.
    /// </summary>
    public decimal? CallbackRatio { get; set; }
    /// <summary>
    /// ["<c>callbackSpread</c>"] Callback spread (price distance).
    /// </summary>
    public decimal? CallbackSpread { get; set; }
    /// <summary>
    /// ["<c>activePx</c>"] Activation price. If not provided, the trailing stop is activated immediately upon order placement.
    /// </summary>
    public decimal? ActivePrice { get; set; }
}
