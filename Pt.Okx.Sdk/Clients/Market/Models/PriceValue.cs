namespace Pt.Okx.Sdk.Clients.Market.Models
{
    /// <summary>
    /// Represents a price value at a specific point in time.
    /// </summary>
    /// <param name="Time">The time associated with the price value.</param>
    /// <param name="Value">The price value.</param>
    public readonly record struct PriceValue(DateTime Time, decimal Value)
    {
        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        public bool IsEmpty => Time == default;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Time:yyyy-MM-dd HH:mm:ss}: {Value}";
        }
    }
}
