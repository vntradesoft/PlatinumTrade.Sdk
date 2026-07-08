using Microsoft.Extensions.DependencyInjection;
using Pt.Okx.Sdk.Strategy;
using Pt.Okx.Sdk.Strategy.Parameters;
using Pt.Okx.Sdk.Strategy.Plugin;
using System;

namespace MyCompany.MyStrategy
{
    public class MyStrategyPlugin : IStrategyPlugin, IStrategyPluginMetadata, IStrategyPluginInputSchema
    {
        public string Name => "MyStrategy";
        public string Version => "1.0.0";
        public string Author => "MyCompany";
        public string Description => "A boilerplate trading strategy.";
        public Microsoft.Extensions.Logging.LogLevel FileLogLevel => Microsoft.Extensions.Logging.LogLevel.Debug;
        public Microsoft.Extensions.Logging.LogLevel ConsoleLogLevel => Microsoft.Extensions.Logging.LogLevel.Information;

        public Type GetInputSchemaType() => typeof(MyStrategyInput);

        public void Register(IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var manager = sp.GetRequiredService<IInputParamManager>();
                return manager.BindSchema<MyStrategyInput>();
            });

            services.AddSingleton<StrategyBase, MyStrategyImpl>();
        }

        public void RegisterForBacktest(IServiceCollection services)
        {
            services.AddTransient(sp =>
            {
                var manager = sp.GetRequiredService<IInputParamManager>();
                return manager.BindSchema<MyStrategyInput>();
            });

            services.AddTransient<StrategyBase, MyStrategyImpl>();
        }
    }
}
