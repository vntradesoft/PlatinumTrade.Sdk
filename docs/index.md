---
_layout: landing
---

# Pt.Okx.Sdk API Documentation

**Pt.Okx.Sdk** is the contracts library for the Platinum Trading Platform (supporting OKX exchange futures & swaps) — it contains all the interfaces, enums, models, and patterns (such as `ApiResult`) that other components in the solution consume.

## Namespaces

| Namespace | Description |
|---|---|
| `Pt.Okx.Sdk.Clients` | Client interfaces: `IOkxClient`, `ITradeClient`, `ITimeSeriesClient`, `IAccountClient`, `IInstrumentClient` |
| `Pt.Okx.Sdk.Common` | `ApiResult` / `ApiResult<T>` pattern for error handling |
| `Pt.Okx.Sdk.Enums` | Enums: `Timeframe`, `OrderSide`, `OrderType`, `InstrumentType`, `TradeMode`, ... |
| `Pt.Okx.Sdk.Indicators` | Indicator base classes (`CalcIndBase`, `IIndicator`, `IIndicatorBuffer`) and plugin interfaces |
| `Pt.Okx.Sdk.Strategy` | Strategy interfaces: `IStrategy`, `IStrategyStateStore`, `IStrategyLogger` |
| `Pt.Okx.Sdk.Drawing` | Drawing manager interfaces and chart objects |
| `Pt.Okx.Sdk.Notifier` | Telegram command extension interface |
| `Pt.Okx.Sdk.Storage` | Storage path provider abstraction |

## Quick Links

- [API Reference](api/index.md)

## Architecture Overview

```text
Pt.Okx.Sdk           ← Contracts (this library)
  ^
  |
Core Engine          ← Engine: OKX wrapper, indicators, socket, simulator
  ^        ^
  |        |
CLI Bot    Platinum Trade App
```

