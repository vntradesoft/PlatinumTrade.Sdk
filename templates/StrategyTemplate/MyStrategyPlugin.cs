using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pt.Okx.Abstractions.Strategy;
using Pt.Okx.Abstractions.Strategy.Parameters;
using Pt.Okx.Abstractions.Strategy.Plugin;
using System;

namespace MyCompany.MyStrategy
{
    public class MyStrategyPlugin : IStrategyPlugin, IStrategyPluginMetadata, IStrategyPluginInputSchema
    {
        public string Name => "MyStrategy";
        public string Version => "1.0.0";
        public string Author => "MyCompany";
        public string Description => "A boilerplate trading strategy.";
        public LogLevel FileLogLevel => LogLevel.Debug;
        public LogLevel ConsoleLogLevel => LogLevel.Information;
        public string? PluginVersion => null;

        public Type GetInputSchemaType() => typeof(MyStrategyInput);

        public void Register(IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var manager = sp.GetRequiredService<IInputParamManager>();
                return manager.BindSchema<MyStrategyInput>();
            });

            services.AddSingleton<IStrategy, MyStrategyImpl>();
        }

        public void RegisterForBacktest(IServiceCollection services)
        {
            services.AddTransient(sp =>
            {
                var manager = sp.GetRequiredService<IInputParamManager>();
                return manager.BindSchema<MyStrategyInput>();
            });

            services.AddTransient<IStrategy, MyStrategyImpl>();
        }
    }
}
