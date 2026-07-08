using Pt.Okx.Sdk.Strategy;
using Pt.Okx.Sdk.Strategy.Events;
using System.Threading;
using System.Threading.Tasks;

namespace MyCompany.MyStrategy
{
    public class MyStrategyImpl : StrategyBase
    {
        private readonly MyStrategyInput _input;

        public MyStrategyImpl(MyStrategyInput input)
        {
            _input = input;
        }

        public override Task<bool> OnInitAsync(IStrategyStateStore state, CancellationToken ct)
        {
            // Initialize your strategy here
            return Task.FromResult(true);
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
    }
}
