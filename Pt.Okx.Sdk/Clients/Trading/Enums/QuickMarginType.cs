namespace Pt.Okx.Sdk.Clients.Trading.Enums
{
    /// <summary>
    /// Quick margin behavior for margin orders.
    /// </summary>
    public enum QuickMarginType
    {
        /// <summary>Manual borrow and repayment.</summary>
        Manual,

        /// <summary>Automatically borrow when needed.</summary>
        AutoBorrow,

        /// <summary>Automatically repay when possible.</summary>
        AutoRepay
    }
}
