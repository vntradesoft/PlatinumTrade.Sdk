using Pt.Okx.Sdk.Strategy.Events;

namespace Pt.Okx.Sdk.Strategy
{
    /// <summary>
    /// Defines the lifecycle and execution contract for a trading strategy.
    /// </summary>
    /// <remarks>
    /// A strategy is initialized once, then executed repeatedly based on incoming events,
    /// and eventually stopped when no longer needed.
    /// </remarks>
    public interface IStrategy
    {
        /// <summary>
        /// Initializes the strategy with the provided state store.
        /// </summary>
        /// <param name="state">
        /// The strategy state store used to persist and retrieve internal state.
        /// </param>
        /// <param name="cancellationToken">
        /// A token used to cancel the initialization process.
        /// </param>
        /// <returns>
        /// <c>true</c> if initialization succeeded; otherwise <c>false</c>.
        /// </returns>
        Task<bool> InitializeAsync(IStrategyStateStore state, CancellationToken cancellationToken);

        /// <summary>
        /// Executes the strategy logic for a given event.
        /// </summary>
        /// <param name="eventType">
        /// The event that triggered execution (e.g., new tick, new bar, timer, order, algo order, balance, transaction, position).
        /// </param>
        /// <param name="state">
        /// The strategy state store for reading or updating internal state.
        /// </param>
        /// <param name="ct">
        /// A token used to cancel execution.
        /// </param>
        /// <returns>A task representing the execution operation.</returns>
        Task RunAsync(StrategyEventType eventType, IStrategyStateStore state, CancellationToken ct);

        /// <summary>
        /// Stops the strategy and performs any required cleanup.
        /// </summary>
        /// <param name="cancellationToken">
        /// A token used to cancel the stop process.
        /// </param>
        /// <returns>
        /// <c>true</c> if the strategy stopped successfully; otherwise <c>false</c>.
        /// </returns>
        Task<bool> StopAsync(CancellationToken cancellationToken);
    }
}
