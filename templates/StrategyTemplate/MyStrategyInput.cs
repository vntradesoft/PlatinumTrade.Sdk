using Pt.Okx.Sdk.Strategy.Parameters;

namespace MyCompany.MyStrategy
{
    public sealed class MyStrategyInput
    {
        [InputParam(
            Section = 1,
            Order = 1,
            Description = "Example strategy parameter")]
        public int ExampleParameter { get; set; } = 10;
    }
}
