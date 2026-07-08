using Pt.Okx.Sdk.Clients.Account.Model;
using Pt.Okx.Sdk.Clients.Trading.Models;
using Pt.Okx.Sdk.Notifier.Models;
using Pt.Okx.Sdk.Strategy.Events;

namespace Pt.Okx.Sdk.Strategy
{
    /// <summary>
    /// Base class for all trading strategies.
    /// </summary>
    /// <remarks>
    /// Inherit from <see cref="StrategyBase"/> and override <see cref="OnTickAsync"/> (required)
    /// plus any optional event handlers needed by your strategy.
    /// <para>
    /// Event dispatch and state mutation are handled by the hosting engine.
    /// </para>
    /// </remarks>
    public abstract class StrategyBase : IStrategy
    {
        /// <inheritdoc/>
        public abstract Task<bool> OnInitAsync(IStrategyStateStore state, CancellationToken cancellationToken);

        /// <inheritdoc/>
        public abstract Task<bool> OnStopAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Called on each market cadence update.
        /// </summary>
        /// <param name="tickPhase">Indicates whether this call is an intra-bar tick or a closed bar.</param>
        /// <param name="ct">Cancellation token.</param>
        public abstract Task OnTickAsync(TickPhase tickPhase, CancellationToken ct);

        /// <summary>
        /// Called when order updates arrive. Override to react to order state changes.
        /// </summary>
        public virtual Task OnOrderAsync(IReadOnlyList<Order> orders, CancellationToken ct) => Task.CompletedTask;

        /// <summary>
        /// Called when algorithmic order updates arrive (e.g., stop-loss, take-profit).
        /// </summary>
        public virtual Task OnAlgoOrderAsync(IReadOnlyList<AlgoOrder> algoOrders, CancellationToken ct) => Task.CompletedTask;

        /// <summary>
        /// Called when position updates arrive.
        /// </summary>
        public virtual Task OnPositionAsync(IReadOnlyList<Position> positions, CancellationToken ct) => Task.CompletedTask;

        /// <summary>
        /// Called when transaction updates arrive.
        /// </summary>
        public virtual Task OnTransactionAsync(IReadOnlyList<Transaction> transactions, CancellationToken ct) => Task.CompletedTask;

        /// <summary>
        /// Called when balance updates arrive.
        /// </summary>
        public virtual Task OnBalanceAsync(IReadOnlyList<AccountBalance> balances, CancellationToken ct) => Task.CompletedTask;

        /// <summary>
        /// Called when a trade command arrives (e.g., from Telegram or an external source).
        /// </summary>
        public virtual Task OnTradeCommandAsync(TradeCommand tradeCommand, CancellationToken ct) => Task.CompletedTask;
    }
}
