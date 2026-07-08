using Microsoft.Extensions.DependencyInjection;
using Pt.Okx.Sdk.Strategy.Parameters;
using Pt.Okx.Sdk.Strategy.Plugin;

namespace Pt.Example.Stgy.UpTrend;

public sealed class UpTrendExamplePlugin : IStrategyPlugin, IStrategyPluginMetadata, IStrategyPluginInputSchema
{
    public string Name => "Pt.Example.Stgy.UpTrend";

    public string PluginVersion => "1.0.0";

    public string RequiredSdkVersion => "1.0";

    public string Author => "PlatinumTrade Team";

    public string Description => "Simple SuperTrend strategy example: market entry on bullish reversal, close on bearish reversal.";

    public Type GetInputSchemaType() => typeof(InputParams);

    public IReadOnlyList<PtLogLevel>? PluginDisplayLogLevels =>
    [
        PtLogLevel.Debug,
        PtLogLevel.Information,
        PtLogLevel.Warning,
        PtLogLevel.Error,
        PtLogLevel.Critical,
        PtLogLevel.Success
    ];

    public void Register(IServiceCollection services)
    {
        services.AddSingleton(sp =>
        {
            var manager = sp.GetRequiredService<IInputParamManager>();
            return manager.BindSchema<InputParams>();
        });

        services.AddSingleton<IStrategy, UpTrendExampleStrategy>();
    }

    public void RegisterForBacktest(IServiceCollection services)
    {
        services.AddTransient(sp =>
        {
            var manager = sp.GetRequiredService<IInputParamManager>();
            return manager.BindSchema<InputParams>();
        });

        services.AddTransient<IStrategy, UpTrendExampleStrategy>();
    }
}
