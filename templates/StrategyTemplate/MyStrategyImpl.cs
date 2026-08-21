using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Pt.Okx.Sdk.Clients;
using Pt.Okx.Sdk.Enums;
using Pt.Okx.Sdk.Indicators.Enums;
using Pt.Okx.Sdk.Strategy;
using Pt.Okx.Sdk.Strategy.Events;
using Pt.Okx.Sdk.Strategy.Settings;

namespace MyCompany.MyStrategy
{
    public class MyStrategyImpl : StrategyBase
    {
        #region Fields
        /// <summary>
        /// The logger for the strategy.
        /// </summary>
        private readonly IStrategyLogger _logger;

        /// <summary>
        /// The OKX client for interacting with the exchange.
        /// </summary>
        private readonly IOkxClient _client;

        /// <summary>
        /// The strategy settings injected from the platform.
        /// Use the Settings tab on the app to adjust these settings.
        /// </summary>
        private readonly StrategySettings _strategySettings;

        /// <summary>
        /// The input parameters for the strategy, bound from the input schema.
        /// Use the Input Parameters tab on the app to adjust these parameters.
        /// </summary>
        private readonly MyStrategyInput _inputParams;

        #endregion

        #region Constructor
        public MyStrategyImpl(
            IStrategyLogger strategyLogger,
            IOkxClient client,
            IOptions<StrategySettings> strategySettings,
            MyStrategyInput inputParams)
        {
            _logger = strategyLogger;
            _client = client;
            _strategySettings = strategySettings.Value;
            _inputParams = inputParams;
        }
        #endregion

        #region Events
        public override async Task<bool> OnInitAsync(IStrategyStateStore state, CancellationToken ct)
        {
            _logger.LogSmart(PtLogLevel.Information, "OnInit", "Initializing...");
            // Set account mode to one-way (hedge mode off)
            var hedgeMode = await _client.Account.SetHedgeModeAsync(false, ct);
            if(!hedgeMode.Success)
            {
                _logger.LogSmart(PtLogLevel.Error, "OnInit", "Set account mode failed: {0}", hedgeMode.Error);
                return false;
            }

            // Set client order prefix to avoid order ID collisions
            var clientOrderPrefix = _client.Trade.SetOrderSourceIdPrefix("mystgy");
            if (!clientOrderPrefix.Success)
            {
                _logger.LogSmart(PtLogLevel.Error, "OnInit", "Set prefix client order failed");
                return false;
            }

            // Initialize your strategy here
            // Example init indicator
            var ma200 = _client.Timeseries.CreateIndicatorMA(
                indicatorAlias: "MA200",
                period: 200,
                method: MaMethod.SMA ,
                propertyOptions: o => { o.Labels[0].Width = 1.5; o.Labels[0].Color = IndicatorColor.Cyan; });

            return true;
        }

        public override Task OnTickAsync(TickPhase tickPhase, CancellationToken ct)
        {
            // Core logic executed on each market tick
            return Task.CompletedTask;
        }

        public override Task<bool> OnStopAsync(CancellationToken ct)
        {
            // Cleanup when the strategy is stopped
            return Task.FromResult(true);
        }
        #endregion
    }
}
