# SDK Quickstart

This guide will help you get started with **Pt.Okx.Sdk** to interact with the OKX exchange in C#.

---

## 1. Installation

Install the package into your project via the dotnet CLI:

```bash
dotnet add package Pt.Okx.Sdk
```

---

## 2. Initialization

Initialize the `IOkxClient` to access OKX trading and market data interfaces:

```csharp
using Pt.Okx.Sdk.Clients;
using Pt.Okx.Sdk.Notifier.Models;

// Setup optional notification configuration
var botConfig = new TelegramBotConfig 
{
    Token = "YOUR_BOT_TOKEN",
    ChatId = "YOUR_CHAT_ID"
};

// Instantiate the primary client interface
IOkxClient client = new OkxClient(botConfig);
```

---

## 3. Fetching Market Data

Retrieve recent candlestick history (OHLCV) from the OKX REST API:

```csharp
var result = await client.MarketData.GetCandlesAsync("BTC-USDT-SWAP", "1m", limit: 100);

if (result.IsSuccess)
{
    foreach (var candle in result.Data)
    {
        Console.WriteLine($"Time: {candle.Time}, Close: {candle.Close}");
    }
}
else
{
    Console.WriteLine($"API Error: {result.Error.Message} (Code: {result.Error.Code})");
}
```

---

## 4. Live Tick Subscription

Subscribe to real-time tick updates using OKX WebSocket feeds:

```csharp
client.MarketData.SubscribeToTicks("ETH-USDT-SWAP", tick => {
    Console.WriteLine($"Live ETH-USDT-SWAP Tick Price: {tick.LastPrice}");
});
```
