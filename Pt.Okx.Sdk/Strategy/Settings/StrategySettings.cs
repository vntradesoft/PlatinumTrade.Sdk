using System.Text;
using Pt.Okx.Sdk.Clients.Market.Models;
using Pt.Okx.Sdk.Clients.Trading.Enums;
using Pt.Okx.Sdk.Enums;
using Pt.Okx.Sdk.Strategy.Settings.Enums;

namespace Pt.Okx.Sdk.Strategy.Settings
{
    /// <summary>
    /// Encapsulates the configuration settings required for running a trading strategy, 
    /// supporting both live trading and backtesting environments.
    /// </summary>
    public class StrategySettings
    {
        /// <summary>Gets or sets a value indicating whether to use the Sandbox environment.</summary>
        public bool SandBox { get; set; }

        /// <summary>Gets or sets a value indicating whether the strategy is running in backtest mode.</summary>
        public bool Backtest { get; set; } = true;

        /// <summary>Gets or sets the artificial delay in milliseconds (useful for debugging or rate-limiting).</summary>
        public int Delay { get; set; }

        /// <summary>Gets or sets the underlying asset (e.g., "BTC").</summary>
        public string Underlying { get; set; } = string.Empty;

        /// <summary>Gets or sets the trading symbol (e.g., "BTC-USDT").</summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>Gets or sets the maximum number of bars to process or store.</summary>
        public int MaxBars { get; set; } = 5000000;

        /// <summary>Gets or sets the number of bars used to warm up technical indicators before the strategy starts.</summary>
        public int WarmupBars { get; set; } = 10000;

        /// <summary>Gets or sets the date filtering option for backtesting.</summary>
        public DateOption DateOption { get; set; }

        /// <summary>Gets or sets the start time for the strategy execution or backtest period.</summary>
        public DateTime? StartTime { get; set; }

        /// <summary>Gets or sets the end time for the strategy execution or backtest period.</summary>
        public DateTime? EndTime { get; set; }

        /// <summary>Gets or sets the initial deposit amount for the simulation or trading account.</summary>
        public decimal Deposite { get; set; }

        /// <summary>Gets or sets the leverage to be used for the strategy.</summary>
        public int Leverage { get; set; }

        /// <summary>Gets or sets the trading mode (e.g., Isolated, Cross).</summary>
        public TradeMode TradeMode { get; init; } = TradeMode.Isolated;

        /// <summary>Gets or sets the margin mode (e.g., Isolated, Cross).</summary>
        public MarginMode MarginMode { get; init; } = MarginMode.Isolated;

        /// <summary>Gets or sets the path or name of the strategy file to execute.</summary>
        public string StrategyFileExecute { get; set; } = string.Empty;

        /// <summary>Gets or sets the timeframe for the strategy (e.g., 1m, 5m, 1h).</summary>
        public Timeframe Timeframe { get; set; }

        /// <summary>Gets or sets a value indicating whether to account for funding fees in simulations.</summary>
        public bool IsAllowFundingFee { get; set; }

        /// <summary>Gets or sets the options for price data resolution.</summary>
        public PriceDataOption PriceDataOptions { get; set; } = PriceDataOption.OneMinuteOHLC;

        /// <summary>Gets or sets the alias of the Telegram Bot configuration used by this strategy.</summary>
        public string TelegramBotAlias { get; set; } = string.Empty;

        /// <summary>Gets or sets the alias of the OKX Account configuration used by this strategy for Live Trade.</summary>
        public string OkxAccountAlias { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the tick simulation configuration, used only in backtest mode.
        /// If null, a default configuration is used.
        /// </summary>
        public TickSourceConfig TickConfig { get; set; } = new TickSourceConfig();

        /// <summary>
        /// Gets the effective number of bars used for warming up indicators.
        /// Falls back to <see cref="MaxBars"/> if <see cref="WarmupBars"/> is not set.
        /// </summary>
        public int EffectiveWarmupBars =>
            WarmupBars > 0 ? WarmupBars :
            MaxBars > 0 ? MaxBars : 0;


        /// <summary>
        /// Creates a deep copy of the current <see cref="StrategySettings"/> instance.
        /// </summary>
        /// <returns>A new <see cref="StrategySettings"/> object with the same property values.</returns>
        public StrategySettings Clone()
        {
            return new StrategySettings
            {
                SandBox = SandBox,
                Backtest = Backtest,
                Delay = Delay,
                Underlying = Underlying,
                Symbol = Symbol,
                MaxBars = MaxBars,
                WarmupBars = WarmupBars,
                DateOption = DateOption,
                StartTime = StartTime,
                EndTime = EndTime,
                Deposite = Deposite,
                Leverage = Leverage,
                TradeMode = TradeMode,
                MarginMode = MarginMode,
                StrategyFileExecute = StrategyFileExecute,
                Timeframe = Timeframe,
                IsAllowFundingFee = IsAllowFundingFee,
                PriceDataOptions = PriceDataOptions,
                TelegramBotAlias = TelegramBotAlias,
                OkxAccountAlias = OkxAccountAlias,
                TickConfig = TickConfig == null
                    ? new TickSourceConfig()
                    : new TickSourceConfig
                    {
                        TicksPerCandle = TickConfig.TicksPerCandle,
                        UseNoise = TickConfig.UseNoise,
                        NoiseAmplitude = TickConfig.NoiseAmplitude,
                        RandomSeed = TickConfig.RandomSeed,
                        OhlcPattern = TickConfig.OhlcPattern
                    }
            };
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine("{");
            sb.AppendLine($"  {nameof(StrategyFileExecute)}: {StrategyFileExecute},");
            sb.AppendLine($"  {nameof(SandBox)}: {SandBox},");
            sb.AppendLine($"  {nameof(Backtest)}: {Backtest},");
            sb.AppendLine($"  {nameof(Delay)}: {Delay},");
            sb.AppendLine($"  {nameof(MaxBars)}: {MaxBars},");
            sb.AppendLine($"  {nameof(WarmupBars)}: {WarmupBars},");
            sb.AppendLine($"  {nameof(DateOption)}: {DateOption},");
            sb.AppendLine($"  {nameof(StartTime)}: {StartTime},");
            sb.AppendLine($"  {nameof(EndTime)}: {EndTime},");
            sb.AppendLine($"  {nameof(Underlying)}: {Underlying},");
            sb.AppendLine($"  {nameof(Symbol)}: {Symbol},");
            sb.AppendLine($"  {nameof(Timeframe)}: {Timeframe},");
            sb.AppendLine($"  {nameof(MarginMode)}: {MarginMode},");
            sb.AppendLine($"  {nameof(Leverage)}: {Leverage},");
            sb.AppendLine($"  {nameof(Deposite)}: {Deposite},");
            sb.AppendLine($"  {nameof(IsAllowFundingFee)}: {IsAllowFundingFee},");
            sb.AppendLine($"  {nameof(PriceDataOptions)}: {PriceDataOptions},");
            sb.AppendLine($"  {nameof(TelegramBotAlias)}: {TelegramBotAlias},");
            sb.AppendLine($"  {nameof(OkxAccountAlias)}: {OkxAccountAlias},");

            if (TickConfig != null)
            {
                sb.AppendLine($"  {nameof(TickConfig)}:");
                sb.AppendLine("  {");
                sb.AppendLine($"    {TickConfig}");
                sb.AppendLine("  }");
            }
            else
            {
                sb.AppendLine($"  {nameof(TickConfig)}: null");
            }

            sb.Append('}');

            return sb.ToString();
        }
    }
}
