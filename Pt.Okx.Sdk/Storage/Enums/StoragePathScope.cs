namespace Pt.Okx.Sdk.Storage.Enums
{
    /// <summary>
    /// Defines logical storage path categories used by the system
    /// to organize different types of runtime and persistent data.
    /// </summary>
    public enum StoragePathScope
    {
        /// <summary>
        /// Root directory for all runtime data.
        /// </summary>
        RuntimeDataRoot,

        /// <summary>
        /// Historical market data storage.
        /// </summary>
        Histories,

        /// <summary>
        /// General log files.
        /// </summary>
        Logs,

        /// <summary>
        /// Backtesting data output.
        /// </summary>
        Backtest,

        /// <summary>
        /// Live trading runtime data.
        /// </summary>
        Live,

        /// <summary>
        /// Log files generated during backtesting.
        /// </summary>
        BacktestLogs,

        /// <summary>
        /// Log files generated during live trading.
        /// </summary>
        LiveLogs,

        /// <summary>
        /// Persistent runtime state (snapshots, checkpoints).
        /// </summary>
        State,

        /// <summary>
        /// Temporary cached data for performance optimization.
        /// </summary>
        Cache,

        /// <summary>
        /// Exported data such as reports, CSV files, or user outputs.
        /// </summary>
        Exports,

        /// <summary>
        /// Folder containing custom technical indicators plugins.
        /// </summary>
        IndicatorPlugins,

        /// <summary>
        /// Folder containing custom trading strategies plugins.
        /// </summary>
        StrategyPlugins,
    }
}
