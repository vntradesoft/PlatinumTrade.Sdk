namespace Pt.Okx.Sdk.Strategy
{
    /// <summary>
    /// Defines the lifecycle and execution contract for a trading strategy.
    /// </summary>
    /// <remarks>
    /// A strategy is initialized once, then executed repeatedly by the hosting engine,
    /// and eventually stopped when no longer needed.
    /// <para>
    /// For most use cases, inherit from <see cref="StrategyBase"/> instead of implementing
    /// this interface directly.
    /// </para>
    /// <para>
    /// The engine owns event dispatch and state mutation. Strategies receive market cadence via
    /// <see cref="Events.TickPhase"/> and may override optional event handlers in <see cref="StrategyBase"/>.
    /// </para>
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
        Task<bool> OnInitAsync(IStrategyStateStore state, CancellationToken cancellationToken);

        /// <summary>
        /// Executes strategy logic on each market tick/bar trigger.
        /// </summary>
        /// <param name="tickPhase">
        /// Indicates whether this call is an intra-bar tick or a closed bar.
        /// </param>
        /// <param name="ct">
        /// A token used to cancel execution.
        /// </param>
        /// <returns>A task representing the execution operation.</returns>
        Task OnTickAsync(Events.TickPhase tickPhase, CancellationToken ct);

        /// <summary>
        /// Stops the strategy and performs any required cleanup.
        /// </summary>
        /// <param name="cancellationToken">
        /// A token used to cancel the stop process.
        /// </param>
        /// <returns>
        /// <c>true</c> if the strategy stopped successfully; otherwise <c>false</c>.
        /// </returns>
        Task<bool> OnStopAsync(CancellationToken cancellationToken);
    }
}
