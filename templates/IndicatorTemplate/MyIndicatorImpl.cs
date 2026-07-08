using Pt.Okx.Sdk.Indicators;

namespace MyCompany.MyIndicator
{
    public class MyIndicatorImpl : IIndicatorCalculator
    {
        public bool IsReady => true;

        public void Calculate(decimal input)
        {
            // Core indicator calculation logic
        }

        public void Reset()
        {
            // Reset internal state
        }
    }
}
