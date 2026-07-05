using Pt.Okx.Sdk.Storage.Enums;

namespace Pt.Okx.Sdk.Storage
{
    /// <summary>
    /// Provides a base implementation for resolving storage paths used by the system.
    /// This class defines logical path mappings for different runtime data scopes
    /// such as history, logs, cache, backtest, and live environments.
    /// </summary>
    public abstract class StoragePathProviderBase : IStoragePathProvider
    {
        /// <summary>
        /// Gets the root directory for all runtime data.
        /// </summary>
        /// <returns>The absolute path to the runtime data root.</returns>
        public abstract string GetRuntimeDataRoot();

        /// <summary>
        /// Resolves a storage path based on the specified <see cref="StoragePathScope"/>.
        /// </summary>
        /// <param name="scope">
        /// The logical storage scope to resolve. Defaults to <see cref="StoragePathScope.RuntimeDataRoot"/>.
        /// </param>
        /// <returns>The absolute path corresponding to the specified scope.</returns>
        public virtual string GetPath(StoragePathScope scope = StoragePathScope.RuntimeDataRoot)
            => scope switch
            {
                StoragePathScope.RuntimeDataRoot => GetRuntimeDataRoot(),
                StoragePathScope.Histories => GetHistoryRoot(),
                StoragePathScope.Logs => GetLogsRoot(),
                StoragePathScope.Backtest => GetBacktestRoot(),
                StoragePathScope.Live => GetLiveRoot(),
                StoragePathScope.BacktestLogs => GetBacktestLogsRoot(),
                StoragePathScope.LiveLogs => GetLiveLogsRoot(),
                StoragePathScope.State => GetStateRoot(),
                StoragePathScope.Cache => GetCacheRoot(),
                StoragePathScope.Exports => GetExportsRoot(),
                StoragePathScope.IndicatorPlugins => GetIndicatorPluginsRoot(),
                StoragePathScope.StrategyPlugins => GetStrategyPluginsRoot(),
                _ => GetRuntimeDataRoot(),
            };

        /// <summary>
        /// Gets the path for historical market data storage.
        /// </summary>
        public virtual string GetHistoryRoot() =>
            Ensure(Path.Combine(GetRuntimeDataRoot(), "Histories"));

        /// <summary>
        /// Gets the root path for application logs (e.g., rolling log files).
        /// </summary>
        public virtual string GetLogsRoot() =>
            Ensure(Path.Combine(GetRuntimeDataRoot(), "Logs"));

        /// <summary>
        /// Gets the root directory for backtesting data.
        /// </summary>
        public virtual string GetBacktestRoot() =>
            Ensure(Path.Combine(GetRuntimeDataRoot(), "Backtest"));

        /// <summary>
        /// Gets the root directory for live trading data.
        /// </summary>
        public virtual string GetLiveRoot() =>
            Ensure(Path.Combine(GetRuntimeDataRoot(), "Live"));

        /// <summary>
        /// Gets the directory for backtest-specific logs and artifacts.
        /// </summary>
        public virtual string GetBacktestLogsRoot() =>
            Ensure(Path.Combine(GetBacktestRoot(), "Logs"));

        /// <summary>
        /// Gets the directory for live trading logs and artifacts.
        /// </summary>
        public virtual string GetLiveLogsRoot() =>
            Ensure(Path.Combine(GetLiveRoot(), "Logs"));

        /// <summary>
        /// Gets the directory used for persistent runtime state
        /// (e.g., snapshots, checkpoints).
        /// </summary>
        public virtual string GetStateRoot() =>
            Ensure(Path.Combine(GetRuntimeDataRoot(), "State"));

        /// <summary>
        /// Gets the directory used for temporary cached data.
        /// This data can be safely rebuilt if needed.
        /// </summary>
        public virtual string GetCacheRoot() =>
            Ensure(Path.Combine(GetRuntimeDataRoot(), "Cache"));

        /// <summary>
        /// Gets the directory used for exported files
        /// (e.g., reports, CSVs, user outputs).
        /// </summary>
        public virtual string GetExportsRoot() =>
            Ensure(Path.Combine(GetRuntimeDataRoot(), "Export"));

        /// <summary>
        /// Gets the folder containing custom technical indicators plugins.
        /// </summary>
        public virtual string GetIndicatorPluginsRoot() =>
            Ensure(Path.Combine(GetRuntimeDataRoot(), "Plugins", "Indicators"));

        /// <summary>
        /// Gets the folder containing custom trading strategies plugins.
        /// </summary>
        public virtual string GetStrategyPluginsRoot() =>
            Ensure(Path.Combine(GetRuntimeDataRoot(), "Plugins", "Strategies"));

        /// <summary>
        /// Ensures that the specified directory exists.
        /// If it does not exist, it will be created.
        /// </summary>
        /// <param name="path">The directory path.</param>
        /// <returns>The same path after ensuring it exists.</returns>
        protected static string Ensure(string path)
        {
            Directory.CreateDirectory(path);
            return path;
        }
    }
}
