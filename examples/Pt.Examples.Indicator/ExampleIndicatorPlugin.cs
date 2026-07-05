using Pt.Okx.Sdk.Indicators.Plugin;

namespace Pt.Examples.Indicator
{
    /// <summary>
    /// Plugin entry point, called when the user loads the DLL from the menu:
    /// Insert > Indicators > Custom > Load Custom Indicator...
    /// </summary>
    public class ExampleIndicatorPlugin : IIndicatorPlugin
    {
        // Metadata
        public string Name => "Okx Indicator Examples";
        public string PluginVersion => "1.0.0";
        public string RequiredSdkVersion => "1.0.0";
        public string Description => "MA Crossover + Momentum (ROC) + ExRSI (SpecialFeatures demo) example indicators";
        public string Author => "PlatinumTrade Team";

        // Register all indicators
        public void RegisterIndicators(IIndicatorRegistrationContext context)
        {
            // MA Crossover (overlay on the main chart)
            context.Register(
                "MACrossover",
                (factory, manager, config, options) =>
                {
                    // Set defaults if the user has not configured them.
                    if (!config.Parameters.Contains("FastPeriod"))
                        config.SetParam("FastPeriod", null, 10);
                    if (!config.Parameters.Contains("SlowPeriod"))
                        config.SetParam("SlowPeriod", null, 20);
                    return new CalcIndMACrossover(factory, manager, config, options);
                },
                [
                    new IndicatorParameterInfo("FastPeriod", "Fast Period", typeof(int), 10, MinValue: 1, MaxValue: 500),
                    new IndicatorParameterInfo("SlowPeriod", "Slow Period", typeof(int), 20, MinValue: 2, MaxValue: 1000),
                ]);

            // Momentum / Rate of Change (separate panel)
            context.Register(
                "Momentum",
                (factory, manager, config, options) =>
                {
                    if (!config.Parameters.Contains("Period"))
                        config.SetParam("Period", null, 14);
                    return new CalcIndMomentum(factory, manager, config, options);
                },
                [
                    new IndicatorParameterInfo("Period", "Period", typeof(int), 14, MinValue: 1, MaxValue: 500),
                ]);

            // ExRSI — demo indicator with full SpecialFeatures (bound lines + BoundFill)
            // Dùng để kiểm tra tab Advanced của CustomIndicatorDialog hiển thị đúng
            context.Register(
                "ExRSI",
                (factory, manager, config, options) =>
                {
                    if (!config.Parameters.Contains("Period"))
                        config.SetParam("Period", null, 14);
                    return new CalcIndExampleOscillator(factory, manager, config, options);
                },
                [
                    new IndicatorParameterInfo("Period", "Period", typeof(int), 14, MinValue: 2, MaxValue: 500),
                ]);
        }
    }
}
