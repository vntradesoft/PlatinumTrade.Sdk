namespace Pt.Okx.Sdk.Notifier.Enums
{
    /// <summary>
    /// Commands that can be sent to a strategy.
    /// </summary>
    public enum TradeAction
    {
        /// <summary>Close the active position or order context.</summary>
        Close,

        /// <summary>Return detailed strategy state.</summary>
        Details,

        /// <summary>Return account balance information.</summary>
        Balance,

        /// <summary>Return strategy metrics.</summary>
        Metrics,

        /// <summary>Return current strategy status.</summary>
        Status,

        /// <summary>Pause trading.</summary>
        PauseTrading,

        /// <summary>Resume trading.</summary>
        ResumeTrading,

        /// <summary>Configure strategy settings.</summary>
        Configure,

        /// <summary>Shutdown the strategy.</summary>
        Shutdown,

        /// <summary>Return recent logs.</summary>
        Logs,

        /// <summary>Upload logs.</summary>
        UploadLogs,

        /// <summary>Custom action handled by a plugin or strategy-specific command handler.</summary>
        Custom
    }
}
