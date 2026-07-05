namespace Pt.Okx.Sdk.Notifier.Models
{
    /// <summary>
    /// Configuration details for an individual Telegram Bot channel.
    /// </summary>
    public class TelegramBotConfig
    {
        /// <summary>Gets or sets a unique alias/nickname for this bot configuration.</summary>
        public string Alias { get; set; } = string.Empty;

        /// <summary>Gets or sets the Telegram Bot Token.</summary>
        public string BotToken { get; set; } = string.Empty;

        /// <summary>Gets or sets the target chat ID to send notifications to.</summary>
        public long ChatId { get; set; }

        /// <summary>Gets or sets a value indicating whether this bot is enabled.</summary>
        public bool Enabled { get; set; } = true;
    }
}
