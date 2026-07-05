namespace Pt.Okx.Sdk.Indicators.Enums
{

    /// <summary>
    /// Type of moving-average crossover detected by helper methods.
    /// </summary>
    public enum MACrossoverType
    {
        /// <summary>No crossover was detected.</summary>
        None,

        /// <summary>The fast line crossed above the slow line.</summary>
        GoldenCross,

        /// <summary>The fast line crossed below the slow line.</summary>
        DeathCross
    }
}
