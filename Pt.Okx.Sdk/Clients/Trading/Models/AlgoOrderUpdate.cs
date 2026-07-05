namespace Pt.Okx.Sdk.Clients.Trading.Models
{
    /// <summary>
    /// Represents an update to an algorithmic order in the OKX trading system.
    /// </summary>
    public record AlgoOrderUpdate : AlgoOrder
    {
        /// <summary>The result of the amendment.</summary>
        public string? AmendResult { get; set; }
        /// <summary>The source of the amendment.</summary>
        public string? AmendSource { get; set; }
        /// <summary>The notional value of the update in USD.</summary>
        public decimal? NotionalUsd { get; set; }
        /// <summary>The request identifier associated with the update.</summary>
        public string? RequestId { get; set; }
        /// <summary>The count of algorithmic orders.</summary>
        public int? AlgoOrderCount { get; set; }
        /// <summary>The time when the update was pushed.</summary>
        public DateTime? PushTime { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AlgoOrderUpdate"/> record.</summary>
        public AlgoOrderUpdate() { }
    }
}
