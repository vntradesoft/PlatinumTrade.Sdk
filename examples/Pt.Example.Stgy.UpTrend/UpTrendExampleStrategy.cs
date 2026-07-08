using Microsoft.Extensions.Options;
using Pt.Okx.Sdk.Clients.Trading;
using Pt.Okx.Sdk.Strategy.Events;
using Pt.Okx.Sdk.Strategy.Settings;

namespace Pt.Example.Stgy.UpTrend;

internal sealed class UpTrendExampleStrategy : StrategyBase
{
    private const string SuperTrendAlias = "Example.SuperTrend";

    private readonly IStrategyLogger _logger;
    private readonly IOkxClient _client;
    private readonly InputParams _input;
    private readonly StrategySettings _settings;

    private IIndicatorSuperTrend _superTrend = null!;
    private int _lastTrendDirection;
    private DateTime _lastEvaluatedClosedBarTime = DateTime.MinValue;
    private DateTime _lastCloseAttemptTime = DateTime.MinValue;
    private bool _assumeFlatAfterCloseError;
    private DateTime _assumeFlatSince = DateTime.MinValue;
    private DateTime _lastAssumeFlatLogTime = DateTime.MinValue;

    public UpTrendExampleStrategy(
        IStrategyLogger logger,
        IOkxClient client,
        InputParams input,
        IOptions<StrategySettings> strategyOptions)
        : base()
    {
        _logger = logger;
        _client = client;
        _input = input;
        _settings = strategyOptions.Value;
    }

    private string SymbolCurrent => _settings.Symbol;

    private Timeframe SignalTimeframe => _settings.Timeframe;

    private DateTime TimeCurrent => _client.Instrument.GetCurrentTime();

    public override async Task<bool> OnInitAsync(IStrategyStateStore state, CancellationToken ct)
    {
        _ = state;

        _client.Trade.DisableLogApiEndPoint(new[]
{
            ApiName.GetPositions,
            ApiName.GetOrders,
            ApiName.PlaceOrder,
            ApiName.ClosePosition
        });

        if (string.IsNullOrWhiteSpace(SymbolCurrent))
        {
            _logger.LogError(new InvalidOperationException("Symbol is empty."), "Init", "Please set StrategySettings.Symbol or InputParams.Symbol.");
            return false;
        }

        if (_input.OrderQuantity <= 0)
        {
            _logger.LogError(new InvalidOperationException("OrderQuantity must be greater than zero."), "Init");
            return false;
        }

        var hedgeModeResult = await _client.Account.SetHedgeModeAsync(false, ct);
        if (!hedgeModeResult.Success)
        {
            _logger.LogWarning("Init", "Set one-way mode failed: {0}", hedgeModeResult.Error);
        }
        else
        {
            _logger.LogInformation("Init", "Position mode set to one-way (net).");
        }

        _superTrend = _client.Timeseries.CreateIndicatorSuperTrend(
            symbol: SymbolCurrent,
            timeframe: SignalTimeframe,
            period: _input.SuperTrendPeriod,
            multiplier: _input.SuperTrendMultiplier,
            indicatorAlias: SuperTrendAlias);

        var magicResult = _client.Trade.SetMagicNumber("exup");
        if (!magicResult.Success)
        {
            _logger.LogWarning("Init", "SetMagicNumber failed: {0}", magicResult.ErrorMsg);
        }

        _logger.LogInformation(
            "Init",
            "Initialized on {0} tf={1}, ST(period={2}, multiplier={3}), qty={4}",
            SymbolCurrent,
            SignalTimeframe,
            _input.SuperTrendPeriod,
            _input.SuperTrendMultiplier,
            _input.OrderQuantity);

        return true;
    }

    public override async Task OnTickAsync(TickPhase tickPhase, CancellationToken ct)
    {
        _ = tickPhase;

        // Evaluate strategy exactly once per closed candle of the configured timeframe.
        var closedCandle = await _client.Timeseries.GetOHCLVAsync(SymbolCurrent, SignalTimeframe, shift: 0, ct: ct);
        if (closedCandle.IsEmpty || closedCandle.Timestamp == _lastEvaluatedClosedBarTime)
        {
            return;
        }

        _lastEvaluatedClosedBarTime = closedCandle.Timestamp;
        //_logger.LogDebug("Kline", "Evaluating strategy on {0} closed bar at {1}", SymbolCurrent, closedCandle.Timestamp);

        // Read SuperTrend on closed bars only to avoid per-tick signal noise.
        var currentSt = _superTrend.GetAt(0);
        var currentDirection = _superTrend.GetTrendDirection(0);
        var previousDirection = _superTrend.GetTrendDirection(1);
        if (currentSt.IsEmpty || currentDirection.IsEmpty || previousDirection.IsEmpty)
        {
            return;
        }

        var hasPosition = await HasOpenPositionLiveAsync(ct);
        if (!hasPosition.HasValue)
        {
            return;
        }

        var hasPositionValue = hasPosition.Value;

        var hasOpenOrder = await HasOpenOrderLiveAsync(ct);
        if (!hasOpenOrder.HasValue)
        {
            return;
        }

        var hasOpenOrderValue = hasOpenOrder.Value;
        var currentTrendDirection = Math.Sign(currentDirection.Value);
        var previousTrendDirection = Math.Sign(previousDirection.Value);

        if (!hasPositionValue && _assumeFlatAfterCloseError)
        {
            _assumeFlatAfterCloseError = false;
            _assumeFlatSince = DateTime.MinValue;
            _logger.LogDebug("Exit", "Position state synchronized to flat on {0}. Clear assume-flat mode.", SymbolCurrent);
        }

        if (_assumeFlatAfterCloseError && hasPositionValue)
        {
            // Keep bot trading even if local position snapshot is stale after already-closed response.
            if (TimeCurrent - _lastAssumeFlatLogTime >= TimeSpan.FromMinutes(2))
            {
                _lastAssumeFlatLogTime = TimeCurrent;
                _logger.LogDebug("Exit", "Assume-flat mode active on {0} while local snapshot still shows position.", SymbolCurrent);
            }
        }

        var effectiveHasPosition = hasPositionValue && !_assumeFlatAfterCloseError;

        if (_lastTrendDirection == 0)
        {
            _lastTrendDirection = previousTrendDirection;
        }

        var flippedToBullish = _lastTrendDirection <= 0 && currentTrendDirection > 0;
        var flippedToBearish = _lastTrendDirection >= 0 && currentTrendDirection < 0;

        // Enter only on bullish trend flip at closed bar.
        var shouldEnter = !effectiveHasPosition
            && !hasOpenOrderValue
            && flippedToBullish;

        // Exit on bearish flip (or confirmed bearish reversal), checked once per closed bar.
        var shouldExit = effectiveHasPosition
            && !hasOpenOrderValue
            && (flippedToBearish || _superTrend.HasBearishReversalConfirmed(confirmBars: 1));

        if (shouldEnter)
        {
            await PlaceLongEntryAsync(ct);
        }
        else if (shouldExit)
        {
            // Avoid sending duplicated close requests when state updates are slightly delayed.
            if (TimeCurrent - _lastCloseAttemptTime >= TimeSpan.FromSeconds(30))
            {
                _lastCloseAttemptTime = TimeCurrent;
                await ClosePositionAsync(ct);
            }
        }

        _lastTrendDirection = currentTrendDirection;
    }

    private async Task<bool?> HasOpenPositionLiveAsync(CancellationToken ct)
    {
        var livePositionsResult = await _client.Trade.GetPositionsAsync(SymbolCurrent, ct: ct);
        if (livePositionsResult.Success)
        {
            return livePositionsResult.Data.Any(IsOpenPositionForCurrentSymbol);
        }

        _logger.LogWarning("State", "GetPositionsAsync failed; skip this Kline cycle: {0}", livePositionsResult.Error);
        return null;
    }

    private async Task<bool?> HasOpenOrderLiveAsync(CancellationToken ct)
    {
        var liveOrdersResult = await _client.Trade.GetOrdersAsync(symbol: SymbolCurrent, ct: ct);
        if (liveOrdersResult.Success)
        {
            return liveOrdersResult.Data.Any(IsActiveOrderForCurrentSymbol);
        }

        _logger.LogWarning("State", "GetOrdersAsync failed; skip this Kline cycle: {0}", liveOrdersResult.Error);
        return null;
    }

    public override Task OnOrderAsync(IReadOnlyList<Pt.Okx.Sdk.Clients.Trading.Models.Order> orders, CancellationToken ct)
    {
        _ = ct;
        _ = orders;
        return Task.CompletedTask;
    }

    public override Task OnAlgoOrderAsync(IReadOnlyList<Pt.Okx.Sdk.Clients.Trading.Models.AlgoOrder> algoOrders, CancellationToken ct)
    {
        _ = ct;
        _ = algoOrders;
        return Task.CompletedTask;
    }

    public override Task OnPositionAsync(IReadOnlyList<Pt.Okx.Sdk.Clients.Trading.Models.Position> positions, CancellationToken ct)
    {
        _ = ct;

        // If Position event arrives with empty snapshot, clear assume-flat mode immediately.
        if (positions.Count == 0 && _assumeFlatAfterCloseError)
        {
            _assumeFlatAfterCloseError = false;
            _assumeFlatSince = DateTime.MinValue;
            _logger.LogDebug("Exit", "Position event indicates flat state on {0}. Clear assume-flat mode.", SymbolCurrent);
        }

        return Task.CompletedTask;
    }

    public override Task OnTransactionAsync(IReadOnlyList<Pt.Okx.Sdk.Clients.Trading.Models.Transaction> transactions, CancellationToken ct)
    {
        _ = ct;
        _ = transactions;
        return Task.CompletedTask;
    }

    public override Task OnBalanceAsync(IReadOnlyList<Pt.Okx.Sdk.Clients.Account.Model.AccountBalance> balances, CancellationToken ct)
    {
        _ = ct;
        _ = balances;
        return Task.CompletedTask;
    }

    public override Task OnTradeCommandAsync(Pt.Okx.Sdk.Notifier.Models.TradeCommand tradeCommand, CancellationToken ct)
    {
        _ = ct;
        _ = tradeCommand;
        return Task.CompletedTask;
    }

    public override Task<bool> OnStopAsync(CancellationToken ct)
    {
        _ = ct;
        _logger.LogInformation("Stop", "Stopped strategy example for {0}", SymbolCurrent);
        return Task.FromResult(true);
    }

    private async Task PlaceLongEntryAsync(CancellationToken ct)
    {
        var quantity = GetNormalizedMarketQuantity();
        if (quantity <= 0)
        {
            _logger.LogWarning("Entry", "Skip entry because normalized quantity is invalid for {0}. inputQty={1}", SymbolCurrent, _input.OrderQuantity);
            return;
        }

        var result = await _client.Trade.PlaceOrderAsync(
            symbol: SymbolCurrent,
            side: OrderSide.Buy,
            type: OrderType.Market,
            quantity: quantity,
            ct: ct);

        if (result.Success)
        {
            _logger.LogInformation(
                "Entry",
                "Placed market buy on {0}, qty={1}, orderId={2}",
                SymbolCurrent,
                quantity,
                result.Data.OrderId);
            return;
        }

        _logger.LogWarning("Entry", "PlaceOrderAsync failed: {0}", result.Error);
    }

    private async Task ClosePositionAsync(CancellationToken ct)
    {
        var result = await _client.Trade.ClosePositionAsync(
            symbol: SymbolCurrent,
            positionSide: PositionSide.Net,
            autoCancel: true,
            ct: ct);

        if (result.Success)
        {
            _assumeFlatAfterCloseError = false;
            _assumeFlatSince = DateTime.MinValue;
            _logger.LogInformation("Exit", "Closed position on {0}", SymbolCurrent);
            return;
        }

        if (IsAlreadyClosedPositionError(result.Error))
        {
            _assumeFlatAfterCloseError = true;
            _assumeFlatSince = TimeCurrent;
            _lastAssumeFlatLogTime = TimeCurrent;
            _logger.LogInformation("Exit", "Position already closed on exchange for {0}. Assume-flat mode enabled.", SymbolCurrent);
            return;
        }

        _logger.LogWarning("Exit", "ClosePositionAsync failed: {0}", result.Error);
    }

    private static bool IsAlreadyClosedPositionError(Pt.Okx.Sdk.Common.ApiError? error)
    {
        if (error is null)
        {
            return false;
        }

        var message = error.Message ?? string.Empty;
        return message.Contains("position not found", StringComparison.OrdinalIgnoreCase)
            || message.Contains("already closed", StringComparison.OrdinalIgnoreCase)
            || message.Contains("position doesn't exist", StringComparison.OrdinalIgnoreCase);
    }

    private bool IsOpenPositionForCurrentSymbol(Pt.Okx.Sdk.Clients.Trading.Models.Position position)
    {
        return string.Equals(position.Symbol, SymbolCurrent, StringComparison.OrdinalIgnoreCase)
            && Math.Abs(position.PositionsQuantity ?? 0m) > 0.00000001m;
    }

    private bool IsActiveOrderForCurrentSymbol(Pt.Okx.Sdk.Clients.Trading.Models.Order order)
    {
        return string.Equals(order.Symbol, SymbolCurrent, StringComparison.OrdinalIgnoreCase)
            && (order.OrderState == Pt.Okx.Sdk.Clients.Trading.Enums.OrderStatus.Live
                || order.OrderState == Pt.Okx.Sdk.Clients.Trading.Enums.OrderStatus.PartiallyFilled);
    }

    private decimal GetNormalizedMarketQuantity()
    {
        var (minQty, maxQty, _) = _client.Instrument.GetLimitLotSize(SymbolCurrent, isMarket: true);
        var normalized = _client.Instrument.NormalizeLot(SymbolCurrent, _input.OrderQuantity, isMarket: true, roundUp: true);

        if (normalized < minQty)
        {
            normalized = minQty;
        }

        if (maxQty > 0 && normalized > maxQty)
        {
            normalized = maxQty;
        }

        if (normalized != _input.OrderQuantity)
        {
            _logger.LogDebug(
                "Entry",
                "Quantity normalized for {0}: input={1}, normalized={2}, min={3}, max={4}",
                SymbolCurrent,
                _input.OrderQuantity,
                normalized,
                minQty,
                maxQty);
        }

        return normalized;
    }

}
