using Pt.Okx.Sdk.Clients.Account.Model;
using Pt.Okx.Sdk.Clients.Market.Models;
using Pt.Okx.Sdk.Clients.Trading.Models;
using Pt.Okx.Sdk.Notifier.Models;

namespace Pt.Okx.Sdk.Strategy.Events
{
    /// <summary>
    /// Represents an event emitted by a trading strategy.
    /// </summary>
    public abstract record StrategyEvent(StrategyEventType Type);

    /// <summary>Represents an order event.</summary>
    public record OrderEvent(IReadOnlyList<Order> Data)
        : StrategyEvent(StrategyEventType.Order);

    /// <summary>Represents a balance update event.</summary>
    public record BalanceEvent(IReadOnlyList<AccountBalance> Data)
        : StrategyEvent(StrategyEventType.Balance);

    /// <summary>Represents a position update event.</summary>
    public record PositionEvent(IReadOnlyList<Position> Data)
        : StrategyEvent(StrategyEventType.Position);

    /// <summary>Represents an algorithmic order update event.</summary>
    public record AlgoOrderEvent(IReadOnlyList<AlgoOrder> Data)
        : StrategyEvent(StrategyEventType.AlgoOrder);

    /// <summary>Represents a kline data update event.</summary>
    public record KlineEvent(CandleData Data)
        : StrategyEvent(StrategyEventType.Kline);

    /// <summary>Represents a transaction event.</summary>
    public record TransactionEvent(IReadOnlyList<Transaction> Data)
        : StrategyEvent(StrategyEventType.Transaction);

    /// <summary>Represents a trade command event from Telegram.</summary>
    public record TradeCommandTelegramEvent(TradeCommand Data)
        : StrategyEvent(StrategyEventType.TradeCommand);
}
