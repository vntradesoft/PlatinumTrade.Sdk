# Migration Guide

This guide assists in migrating from standard OKX REST/WebSocket API wrappers (or legacy integrations) to the structured **Pt.Okx.Sdk**.

---

## 1. Key Platform Rules

Unlike generic exchange wrappers, **Pt.Okx.Sdk** enforces strict constraints to ensure production safety and compatibility with the **Platinum Trade** desktop host:

- **Single Asset Mode:** You must set your OKX Account Settings to "Single-currency margin mode" (or "Multi-currency margin mode" if qualified), but the SDK is optimized primarily for single margin configurations.
- **Isolated Margin Only:** All swap position and trading commands are constrained to **Isolated Margin** (`instType=SWAP`, `mgnMode=isolated`). Cross-margin or portfolio-margin modes are currently not supported via the standard strategy wrappers.
- **Decimal Precision:** All prices, sizes, and volumes use the `decimal` type rather than `double` or `float` to prevent floating-point rounding errors during order execution.

---

## 2. API Structural Map

Below is the mapping from raw OKX REST API endpoints to the corresponding unified SDK service calls:

| Raw OKX API Endpoint | Legacy Method | Pt.Okx.Sdk Service Call |
|---|---|---|
| `/api/v5/market/candles` | `GetCandles(...)` | `client.MarketData.GetCandlesAsync(...)` |
| `/api/v5/trade/order` | `PlaceOrder(...)` | `client.Trade.PlaceOrderAsync(...)` |
| `/api/v5/account/balance` | `GetBalance(...)` | `client.Account.GetBalanceAsync(...)` |
| `/api/v5/account/positions` | `GetPositions(...)` | `client.Account.GetPositionsAsync(...)` |

---

## 3. Code Migration Example

### Before (Raw API Wrapper / Manual Request)
```csharp
var client = new LegacyOkxRestClient();
var response = await client.PlaceOrderAsync(
    instId: "BTC-USDT-SWAP", 
    tdMode: "isolated", 
    side: "buy", 
    ordType: "market", 
    sz: "0.01"
);
```

### After (Pt.Okx.Sdk Structured Request)
```csharp
var client = new OkxClient(botConfig);
var response = await client.Trade.PlaceOrderAsync(new PlaceOrderRequest 
{
    InstrumentId = "BTC-USDT-SWAP",
    TradeMode = TradeMode.Isolated,
    Side = OrderSide.Buy,
    OrderType = OrderType.Market,
    Quantity = 0.01m
});
```
