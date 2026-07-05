namespace Pt.Okx.Sdk.Enums
{
    /// <summary>
    /// Log severity used by OKX strategy and SDK components.
    /// </summary>
    public enum PtLogLevel
    {
        /// <summary>Critical failure.</summary>
        Critical,

        /// <summary>Error.</summary>
        Error,

        /// <summary>Warning.</summary>
        Warning,

        /// <summary>Successful operation.</summary>
        Success,

        /// <summary>Core system message.</summary>
        Core,

        /// <summary>Informational message.</summary>
        Information,

        /// <summary>Debug message.</summary>
        Debug,

        /// <summary>Trace message.</summary>
        Trace,

        /// <summary>Logging is disabled.</summary>
        None
    }
}
