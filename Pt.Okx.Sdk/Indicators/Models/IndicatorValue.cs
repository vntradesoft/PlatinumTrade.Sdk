namespace Pt.Okx.Sdk.Indicators.Models
{
    /// <summary>
    /// Represents a single indicator value with its associated timestamp,
    /// including support for empty-value semantics.
    /// </summary>
    public record struct IndicatorValue : IEquatable<IndicatorValue>, IComparable<IndicatorValue>
    {
        /// <summary>
        /// Gets the timestamp associated with the value.
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Gets the numeric value of the indicator.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Gets a value indicating whether this instance represents an empty value.
        /// A value is considered empty if the timestamp is <see cref="DateTime.MinValue"/>
        /// or if the numeric value is <see cref="double.NaN"/>.
        /// </summary>
        public bool IsEmpty => Timestamp == DateTime.MinValue || double.IsNaN(Value);

        /// <summary>
        /// Represents an empty indicator value.
        /// </summary>
        public static readonly IndicatorValue Empty = new(DateTime.MinValue, double.NaN);

        /// <summary>
        /// Initializes a new instance of the <see cref="IndicatorValue"/> struct.
        /// </summary>
        /// <param name="timestamp">The timestamp of the value.</param>
        /// <param name="value">The numeric value.</param>
        public IndicatorValue(DateTime timestamp, double value)
        {
            Timestamp = timestamp;
            Value = value;
        }

        /// <summary>
        /// Creates a copy of this value with a different timestamp.
        /// </summary>
        /// <param name="timestamp">The new timestamp.</param>
        /// <returns>A new <see cref="IndicatorValue"/> with the updated timestamp.</returns>
        public IndicatorValue WithTimestamp(DateTime timestamp)
        {
            return new IndicatorValue(timestamp, Value);
        }

        /// <summary>
        /// Determines equality based on numeric value only.
        /// Empty values are always considered unequal (similar to NaN semantics).
        /// </summary>
        /// <param name="other">The value to compare with.</param>
        /// <returns>
        /// <c>true</c> if both values are non-empty and numerically equal; otherwise <c>false</c>.
        /// </returns>
        public bool Equals(IndicatorValue other)
        {
            if (IsEmpty || other.IsEmpty) return false;
            return Value == other.Value;
        }

        /// <summary>
        /// Returns the hash code based on the numeric value.
        /// Empty values return <c>0</c>.
        /// </summary>
        public override int GetHashCode() =>
            IsEmpty ? 0 : Value.GetHashCode();

        /// <summary>
        /// Compares two values by their numeric value.
        /// Empty values are treated as greater than all valid values
        /// (i.e., they sort to the end).
        /// </summary>
        /// <param name="other">The value to compare with.</param>
        /// <returns>
        /// A signed integer indicating the relative order.
        /// </returns>
        public int CompareTo(IndicatorValue other)
        {
            if (IsEmpty && other.IsEmpty) return 0;
            if (IsEmpty) return 1;
            if (other.IsEmpty) return -1;
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Determines whether one value is less than another based on numeric comparison.
        /// Empty values are treated as greater than all valid values.
        /// </summary>
        public static bool operator <(IndicatorValue left, IndicatorValue right) =>
            left.CompareTo(right) < 0;

        /// <summary>
        /// Determines whether one value is greater than another based on numeric comparison.
        /// Empty values are treated as greater than all valid values.
        /// </summary>
        public static bool operator >(IndicatorValue left, IndicatorValue right) =>
            left.CompareTo(right) > 0;

        /// <summary>
        /// Determines whether one value is less than or equal to another.
        /// </summary>
        public static bool operator <=(IndicatorValue left, IndicatorValue right) =>
            left.CompareTo(right) <= 0;

        /// <summary>
        /// Determines whether one value is greater than or equal to another.
        /// </summary>
        public static bool operator >=(IndicatorValue left, IndicatorValue right) =>
            left.CompareTo(right) >= 0;

        /// <summary>
        /// Converts this instance to a <see cref="double"/> value.
        /// Returns <see cref="double.NaN"/> if the value is empty.
        /// </summary>
        /// <param name="iv">The indicator value.</param>
        /// <returns>The numeric value.</returns>
        public static implicit operator double(IndicatorValue iv) => iv.Value;

        /// <summary>
        /// Converts this instance to a <see cref="double"/> value.
        /// </summary>
        /// <returns></returns>
        public double ToDouble() => Value;

        /// <summary>
        /// Returns a debug-friendly string containing timestamp and value.
        /// </summary>
        public override string ToString() =>
            $"{Timestamp.ToLocalTime():yyyy.MM.dd HH:mm:ss}: {Value:F9}";
    }
}
