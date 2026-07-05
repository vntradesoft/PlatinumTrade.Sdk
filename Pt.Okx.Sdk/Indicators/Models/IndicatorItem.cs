using Pt.Okx.Sdk.Indicators.Enums;

namespace Pt.Okx.Sdk.Indicators.Models
{
    /// <summary>
    /// Snapshot container for indicator values at a specific timestamp.
    /// Holds identity, rendering property metadata, and per-buffer values.
    /// </summary>
    public class IndicatorItem
    {
        /// <summary>
        /// Gets or sets the runtime indicator instance ID.
        /// </summary>
        public string IndicatorId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets rendering metadata for this indicator instance.
        /// </summary>
        public IndicatorProperty Property { get; set; } = null!;

        /// <summary>
        /// Gets or sets identity metadata for this indicator instance.
        /// </summary>
        public IndicatorIdentity Identity { get; set; } = null!;

        /// <summary>
        /// Gets or sets the timestamp associated with this value snapshot.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets buffer type metadata keyed by buffer index.
        /// </summary>
        public IDictionary<int, IndicatorBufferType> ValueTypes { get; } = new Dictionary<int, IndicatorBufferType>();

        /// <summary>
        /// Gets or sets numeric buffer values keyed by buffer index.
        /// </summary>
        public IDictionary<int, double> Values { get; } = new Dictionary<int, double>();

        /// <summary>
        /// Initializes a new <see cref="IndicatorItem"/> as a copy of another instance.
        /// Scalar fields are copied directly; dictionaries are deep-copied by key/value entries.
        /// </summary>
        /// <param name="other">The source item to copy from.</param>
        public IndicatorItem(IndicatorItem other)
        {
            ArgumentNullException.ThrowIfNull(other);

            IndicatorId = other.IndicatorId;
            Timestamp = other.Timestamp;

            Property = other.Property;
            Identity = other.Identity;


            ValueTypes = other.ValueTypes != null
                ? new Dictionary<int, IndicatorBufferType>(other.ValueTypes)
                : new Dictionary<int, IndicatorBufferType>();

            Values = other.Values != null
                ? new Dictionary<int, double>(other.Values)
                : new Dictionary<int, double>();

        }

        /// <summary>
        /// Initializes a new empty <see cref="IndicatorItem"/> instance.
        /// </summary>
        public IndicatorItem() { }

        /// <summary>
        /// Gets the display name for this snapshot from <see cref="Property"/>.
        /// </summary>
        /// <returns>The indicator display name.</returns>
        public string DisplayName => Property.Name;

        /// <summary>
        /// Creates a new <see cref="IndicatorItem"/> instance with the specified parameters.
        /// </summary>
        /// <param name="indicatorId">The unique identifier for the indicator instance.</param>
        /// <param name="timestamp">The timestamp associated with this value snapshot.</param>
        /// <param name="property">The rendering metadata for this indicator instance.</param>
        /// <param name="identity">The identity metadata for this indicator instance.</param>
        /// <param name="valueTypes">The buffer type metadata keyed by buffer index.</param>
        /// <param name="values">The numeric buffer values keyed by buffer index.</param>
        /// <returns>A new <see cref="IndicatorItem"/> instance.</returns>
        public static IndicatorItem Create(
            string indicatorId,
            DateTime timestamp,
            IndicatorProperty property,
            IndicatorIdentity identity,
            IDictionary<int, IndicatorBufferType> valueTypes,
            IDictionary<int, double> values)
        {
            ArgumentNullException.ThrowIfNull(indicatorId);
            ArgumentNullException.ThrowIfNull(property);
            ArgumentNullException.ThrowIfNull(identity);
            ArgumentNullException.ThrowIfNull(valueTypes);
            ArgumentNullException.ThrowIfNull(values);

            var item = new IndicatorItem
            {
                IndicatorId = indicatorId,
                Timestamp = timestamp,
                Property = property,
                Identity = identity
            };

            foreach (var kv in valueTypes)
                item.ValueTypes[kv.Key] = kv.Value;

            foreach (var kv in values)
                item.Values[kv.Key] = kv.Value;

            return item;
        }
    }
}
