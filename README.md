<h1><img src="https://raw.githubusercontent.com/vntradesoft/PlatinumTrade.Sdk/main/icon.png" alt="Pt.Okx.Sdk" width="64" style="vertical-align:middle;" /> Pt.Okx.Sdk</h1>

[![Build Status](https://img.shields.io/github/actions/workflow/status/vntradesoft/PlatinumTrade.Sdk/publish.yml?style=for-the-badge&label=build)](https://github.com/vntradesoft/PlatinumTrade.Sdk/actions/workflows/publish.yml) [![License](https://img.shields.io/badge/license-MIT-blue.svg?style=for-the-badge)](LICENSE) [![Since](https://img.shields.io/badge/since-2026-0A7E3B?style=for-the-badge)](#supported-frameworks) [![Discord](https://img.shields.io/discord/1522278590610407635?style=for-the-badge&logo=discord&label=Discord)](https://discord.gg/UBV8YnMJs)

A .NET SDK providing contracts, abstractions, and common components for building algorithmic trading applications on the OKX exchange.

The SDK is designed specifically for the **Platinum Trade** desktop application. CLI integration is currently under development to extend compatibility beyond the desktop environment.

> **Note:** Currently only **OKX Perpetual Swap** in **Isolated Margin** mode is supported.

> **Looking for a full trading platform?** Pt.Okx.Sdk powers [Platinum Trade](https://github.com/vntradesoft/PlatinumTrade.App) — a complete desktop trading application built on this SDK. This package is free and MIT-licensed for anyone building their own tools.

---

## Why Pt.Okx.Sdk?

Stop worrying about exchange APIs.

Pt.Okx.Sdk provides a trading-focused development experience where developers can
concentrate on what matters most: strategies, indicators, and execution logic.

Inspired by the simplicity of MQL5 and modern trading platforms, the SDK abstracts
exchange-specific complexities behind a clean and consistent programming model.

Together with PlatinumApp, users can seamlessly move from idea to execution:

Strategy → Backtest → Optimize → Live Trade

All within a unified ecosystem designed for professional traders and developers.

---

## Screenshots

**Dashboard — Market Watch & Chart**

![Dashboard](https://raw.githubusercontent.com/vntradesoft/PlatinumTrade.Sdk/main/docs/images/dashboard.png)

<table>
<tr>
<td width="50%">

**Live Trading**

![Live Trading](https://raw.githubusercontent.com/vntradesoft/PlatinumTrade.Sdk/main/docs/images/livetrade.png)

</td>
<td width="50%">

**Backtest — Chart & Balance**

![Backtest Chart](https://raw.githubusercontent.com/vntradesoft/PlatinumTrade.Sdk/main/docs/images/backtest1.png)

</td>
</tr>
</table>

**Backtest — Metrics & Reports**

![Backtest Metrics](https://raw.githubusercontent.com/vntradesoft/PlatinumTrade.Sdk/main/docs/images/backtest.png)

---

## Features

- **Indicator Plugin**: Extensible base classes (`CalcIndBase`) and services to design custom indicator plugins.
- **Strategy Plugin**: Standardized interfaces for strategy plugins, settings, parameters, runtime telemetry, and interaction with the trading exchange.
- **Backtest and Realtime Support**: Created plugins can be used in both backtest and realtime trading workflows.
- **Notification Support**: Built-in hooks for sending runtime notifications and strategy messages.
- **Drawing Objects**: Supports chart drawing objects and visual annotations for trading workflows.
- **UI Integration**: Indicator, strategy, and drawing outputs can be displayed directly in the application UI.

---

## Supported Frameworks

| .NET implementation | Version | Support |
| --- | --- | --- |
| .NET | 8.0 | Supported |
| .NET | 9.0 | Supported |
| .NET | 10.0 | Supported |

> Current package targets are `net8.0;net9.0;net10.0`.

---

## Installation

[![NuGet version](https://img.shields.io/nuget/v/Pt.Okx.Sdk.svg?style=for-the-badge)](https://www.nuget.org/packages/Pt.Okx.Sdk) [![NuGet downloads](https://img.shields.io/nuget/dt/Pt.Okx.Sdk.svg?style=for-the-badge)](https://www.nuget.org/packages/Pt.Okx.Sdk)

Add the package via the dotnet CLI:

```bash
dotnet add package Pt.Okx.Sdk
```

### Project Templates

You can quickly scaffold a new strategy or indicator project using our official `.NET Templates`. 

First, install the templates directly from the NuGet package (or from the `templates` folder if you cloned the repo):

```bash
# If installing from cloned source:
dotnet new install ./templates/StrategyTemplate
dotnet new install ./templates/IndicatorTemplate
```

Once installed, you can generate ready-to-run boilerplate projects:

```bash
# Create a new Strategy Plugin
dotnet new pt-strategy -n MyTradingBot

# Create a new Indicator Plugin
dotnet new pt-indicator -n MyCustomIndicators
```

---

## Getting Started

### 1. Creating a Custom Strategy Plugin

To create a custom strategy, implement the `IStrategy` interface and register your services via `IStrategyPlugin`:

#### Implement the Strategy

```csharp
using Pt.Okx.Sdk.Strategy;
using Pt.Okx.Sdk.Strategy.Events;
using Pt.Okx.Sdk.Strategy.Settings;
using Microsoft.Extensions.Options;

public class SimpleMomentumStrategy : StrategyBase
{
    private readonly IStrategyLogger _logger;
    private readonly IOkxClient _client;
    private readonly StrategySettings _settings;

    public SimpleMomentumStrategy(
        IStrategyLogger logger,
        IOkxClient client,
        IOptions<StrategySettings> settings)
    {
        _logger = logger;
        _client = client;
        _settings = settings.Value;
    }

    public override async Task<bool> OnInitAsync(IStrategyStateStore state, CancellationToken ct)
    {
        _logger.LogInformation("Init", "Initializing strategy for {Symbol}", _settings.Symbol);
        return true;
    }

    public override async Task OnTickAsync(TickPhase tickPhase, CancellationToken ct)
    {
        // tickPhase indicates whether this is an intra-bar tick or a closed bar
        _logger.LogInformation("Tick", "Processing tick update for {Symbol}", _settings.Symbol);
        // Implement your trading logic here
    }

    public override Task<bool> OnStopAsync(CancellationToken ct)
    {
        _logger.LogWarning("Stop", "Strategy is stopping");
        return Task.FromResult(true);
    }
    
    // Optional: override OnOrderAsync, OnPositionAsync, etc. to react to events
    public override Task OnPositionAsync(IReadOnlyList<Pt.Okx.Sdk.Clients.Trading.Models.Position> positions, CancellationToken ct)
    {
        // Handle position updates
        return Task.CompletedTask;
    }
}
```

#### Register the Plugin

```csharp
using Pt.Okx.Sdk.Strategy;
using Microsoft.Extensions.DependencyInjection;

public class MyStrategyPlugin : IStrategyPlugin, IStrategyPluginMetadata
{
    public string Name => "My Momentum Strategy";
    public string PluginVersion => "1.0.0";
    public string RequiredSdkVersion => "1.0";

    public void Register(IServiceCollection services)
    {
        services.AddSingleton<IStrategy, SimpleMomentumStrategy>();
    }

    public void RegisterForBacktest(IServiceCollection services)
    {
        services.AddTransient<IStrategy, SimpleMomentumStrategy>();
    }
}
```

---

### 2. Creating a Custom Indicator

To build a custom indicator, inherit from `CalcIndBase` and implement `OnCalculate`:

```csharp
using Pt.Okx.Sdk.Indicators.Base;
using Pt.Okx.Sdk.Indicators.Enums;
using Pt.Okx.Sdk.Indicators.Models;
using Pt.Okx.Sdk.Indicators.Services;

public class SimpleMomentumIndicator : CalcIndBase
{
    private IIndicatorBuffer? _rocBuffer;
    private int _period;

    public SimpleMomentumIndicator(
        IIndicatorFactory factory,
        IIndicatorManager manager,
        IndicatorConfig config)
        : base(factory, manager, config)
    {
    }

    protected override IndicatorProperty CreateDefaultProperty()
    {
        _period = GetParameter<int>("Period");

        return new IndicatorProperty(
            name: $"Momentum({_period})",
            window: IndicatorWindow.Separate, // Open in a separate chart panel
            buffers: 1,
            plots: 1
        );
    }

    public override bool OnInit()
    {
        base.OnInit();
        SetBuffer(0, IndicatorBufferType.Data);
        _rocBuffer = GetBuffer(0);
        return true;
    }

    public override int OnCalculate(
        in int ratesTotal,
        in int prevCalculated,
        in DateTime[] datetime,
        in double[] opens,
        in double[] highs,
        in double[] lows,
        in double[] closes,
        in double[] volumes,
        in double spreads)
    {
        if (closes == null || ratesTotal <= _period) return 0;
        _rocBuffer ??= GetBuffer(0);

        int start = Math.Max(prevCalculated - 1, _period);
        for (int i = start; i < ratesTotal; i++)
        {
            double closeNow = closes[i];
            double closeOld = closes[i - _period];

            // ROC Formula: ((Close - Close[N]) / Close[N]) * 100
            double value = closeOld != 0 ? ((closeNow - closeOld) / closeOld) * 100 : 0;
            _rocBuffer?.SetValue(i, value);
        }

        return ratesTotal;
    }
}
```

### 3. Registering the Indicator inside a Plugin

Register your custom indicators via `IIndicatorPlugin`:

```csharp
using Pt.Okx.Sdk.Indicators.Plugin;

public class MyIndicatorPlugin : IIndicatorPlugin
{
    public string Name => "My Trading Indicators";
    public string PluginVersion => "1.0.0";
    public string RequiredSdkVersion => "1.0.0";
    public string Description => "Contains simple ROC indicators.";

    public void RegisterIndicators(IIndicatorRegistrationContext context)
    {
        context.Register(
            "SimpleMomentum",
            (factory, manager, config, options) =>
            {
                if (!config.Parameters.Contains("Period"))
                    config.SetParam("Period", null, 14);
                return new SimpleMomentumIndicator(factory, manager, config);
            },
            [
                new IndicatorParameterInfo("Period", "Period", typeof(int), 14, MinValue: 1, MaxValue: 500),
            ]);
    }
}
```

---

## Documentation

Full documentation:
https://vntradesoft.github.io/PlatinumTrade.Docs/

API Reference:
https://vntradesoft.github.io/PlatinumTrade.Docs/sdk/api/index.html

<details>
<summary>Building docs locally (for contributors)</summary>

```bash
dotnet tool install -g docfx
docfx docs/docfx.json
```

</details>

---

## Examples

The following reference implementations are available:

- **Indicator Example**: Detailed code in the [examples/Pt.Examples.Indicator/](examples/Pt.Examples.Indicator) folder.
- **Strategy Example**: Detailed code in the [examples/Pt.Example.Stgy.UpTrend/](examples/Pt.Example.Stgy.UpTrend) folder.

---

## Supported Frameworks

- .NET 8
- .NET 9
- .NET 10

---

## Community & Support

- **GitHub Discussions:** [vntradesoft/PlatinumTrade.App/discussions](https://github.com/vntradesoft/PlatinumTrade.App/discussions)
- **Issues:** [Report bugs or suggest features](https://github.com/vntradesoft/PlatinumTrade.Sdk/issues)
- **Discord:** [Join the PlatinumTrade community](https://discord.gg/UBV8YnMJs)

---

## Contributing

Contributions, bug reports, and feature requests are welcome. Feel free to open issues or submit pull requests.

---

## License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

---

## Changelog

### [0.9.0-beta.3] - 2026-07-08

#### Features
- **sdk:** Add dotnet project templates for strategy and indicator.

### [0.9.0-beta.2] - 2026-07-06

#### Features
- **sdk:** Update abstractions for strategy engine, plugins, indicators, and backtest.

### [0.9.0-beta.1] - 2026-07-05

#### Features
- **sdk:** Initial Beta Release.
  - Public contract surface for plugin development.
  - Initial `Pt.Okx.Sdk` NuGet package release.
