using System.Runtime.Loader;

namespace Pt.Okx.Sdk.Indicators.Plugin
{
    /// <summary>
    /// Represents a loaded plugin in the OKX trading system.
    /// </summary>
    /// <param name="FilePath">The file path to the plugin assembly.</param>
    /// <param name="Name">The name of the plugin.</param>
    /// <param name="Version">The plugin version.</param>
    /// <param name="Description">A description of the plugin.</param>
    /// <param name="Author">The author of the plugin.</param>
    /// <param name="IndicatorNames">The names of the indicators provided by the plugin.</param>
    /// <param name="LoadContext">The assembly load context used to isolate the plugin.</param>
    public record IndicatorLoadedPlugin(
        string FilePath,
        string Name,
        string Version,
        string Description,
        string Author,
        IReadOnlyList<string> IndicatorNames,
        AssemblyLoadContext LoadContext);

}
