using Pt.Okx.Sdk.Indicators;
using Pt.Okx.Sdk.Strategy.Parameters;

namespace MyCompany.MyIndicator
{
    public class MyIndicatorPlugin : IIndicatorPlugin
    {
        public string Name => "MyIndicator";
        public string Version => "1.0.0";
        public string Author => "MyCompany";
        public string Description => "A boilerplate indicator.";

        public void RegisterIndicators(IIndicatorRegistrationContext context)
        {
            context.Register<MyIndicatorImpl>("MY_IND", "My custom indicator", new[]
            {
                new IndicatorParameterInfo("Period", "Lookback period", 14, 1, 100)
            });
        }
    }
}
