using Pt.Okx.Sdk.Notifier.Enums;

namespace Pt.Okx.Sdk.Notifier.Models
{
    /// <summary>
    /// Represents a command sent to a trading strategy.
    /// </summary>
    public record TradeCommand
    {
        /// <summary>Gets or sets the command action.</summary>
        public TradeAction Action { get; set; }

        /// <summary>Gets or sets the Telegram Chat ID from which this command originated.</summary>
        public long SourceChatId { get; set; }

        /// <summary>Gets or sets the target symbol.</summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>Gets or sets the command amount.</summary>
        public decimal Amount { get; set; }

        /// <summary>Gets or sets the command price.</summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the plugin-specific command identifier used when <see cref="Action"/> is <see cref="TradeAction.Custom"/>.
        /// Examples: "setlevel", "forcetpchase", or "resetrisk".
        /// </summary>
        public string CommandTag { get; set; } = string.Empty;

        /// <summary>Gets or sets additional key-value parameters for custom commands.</summary>
        public IDictionary<string, string> Params { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Returns a string representation of the command.
        /// </summary>
        public override string ToString()
        {
            return Action switch
            {
                TradeAction.Custom => $"Custom command: /{CommandTag}",
                TradeAction.Close => $"Close position {Symbol}",
                TradeAction.Details => "Check strategy details",
                TradeAction.Balance => "Account balance",
                TradeAction.Metrics => "Strategy metrics",
                TradeAction.Status => "Strategy status",
                TradeAction.PauseTrading => "Pause trading",
                TradeAction.ResumeTrading => "Resume trading",
                TradeAction.Configure => "Strategy configuration",
                TradeAction.Shutdown => "Shutdown strategy",
                TradeAction.Logs => "Recent logs",
                TradeAction.UploadLogs => $"Upload logs for {Symbol}",
                _ => "Unknown command"
            };
        }
    }
}
