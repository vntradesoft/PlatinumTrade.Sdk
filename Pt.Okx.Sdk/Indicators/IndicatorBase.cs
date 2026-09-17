using Pt.Okx.Abstractions.Indicators.Base;
using Pt.Okx.Abstractions.Indicators.Services;
using Pt.Okx.Shared.Indicators.Models;

namespace Pt.Okx.Sdk.Indicators
{
    /// <summary>
    /// Base class for indicator calculators in the SDK.
    /// Inherits from <see cref="Pt.Okx.Abstractions.Indicators.Base.IndicatorBase"/>.
    /// </summary>
    public abstract class IndicatorBase : Pt.Okx.Abstractions.Indicators.Base.IndicatorBase
    {
        /// <summary>
        /// Initializes the base calculator with factory, manager, config, and optional property customizations.
        /// </summary>
        protected IndicatorBase(
            IIndicatorFactory factory,
            IIndicatorManager manager,
            IndicatorConfig config,
            Action<IndicatorProperty>? propertyOptions = null)
            : base(factory, manager, config, propertyOptions)
        {
        }
    }
}
