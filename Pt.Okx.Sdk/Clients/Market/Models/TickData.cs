using System.Runtime.InteropServices;
using Pt.Okx.Sdk.Clients.Market.Enums;

namespace Pt.Okx.Sdk.Clients.Market.Models
{
    /// <summary>
    /// Represents the smallest data unit in the simulation system.
    /// In backtest, this is generated synthetically from 1m OHLCV data.
    /// In real-time trading, this is sourced directly from the exchange's WebSocket tickers channel.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct TickData : IEquatable<TickData>
    {
        /// <summary>The UTC timestamp in ticks.</summary>
        public readonly long TimestampTicks;
        /// <summary>The price of the tick.</summary>
        public readonly double Price;
        /// <summary>The volume traded in this tick.</summary>
        public readonly float Volume;
        /// <summary>The sequential index of this tick within its context.</summary>
        public readonly byte TickIndex;
        /// <summary>The type of the tick (e.g., trade, bid, ask).</summary>
        public readonly TickType Type;

        /// <summary>Gets the UTC <see cref="DateTime"/> representation of the tick's timestamp.</summary>
        public DateTime Timestamp => new(TimestampTicks, DateTimeKind.Utc);

        /// <summary>
        /// Gets a value indicating whether the tick data is empty (contains default values).
        /// </summary>
        public bool IsEmpty => Price == 0.0 && TimestampTicks == 0;

        /// <summary>Gets a representative empty <see cref="TickData"/> instance.</summary>
        public static readonly TickData Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="TickData"/> struct.
        /// </summary>
        /// <param name="tsTicks">The UTC timestamp in ticks.</param>
        /// <param name="price">The price.</param>
        /// <param name="vol">The volume.</param>
        /// <param name="idx">The tick index.</param>
        /// <param name="type">The tick type.</param>
        public TickData(long tsTicks, double price, float vol, byte idx, TickType type)
        {
            TimestampTicks = tsTicks;
            Price = price;
            Volume = vol;
            TickIndex = idx;
            Type = type;
        }


        /// <summary>
        /// Determines whether this instance is equal to another <see cref="TickData"/>.
        /// Equality is defined by comparing all fields.
        /// </summary>
        /// <param name="other">The other tick data to compare with.</param>
        /// <returns>
        /// <c>true</c> if all fields are equal; otherwise <c>false</c>.
        /// </returns>
        public bool Equals(TickData other) =>
            TimestampTicks == other.TimestampTicks &&
            Price == other.Price &&
            Volume == other.Volume &&
            TickIndex == other.TickIndex &&
            Type == other.Type;

        /// <summary>
        /// Determines whether this instance is equal to the specified object.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>
        /// <c>true</c> if the object is a <see cref="TickData"/> and is equal to this instance; otherwise <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) =>
            obj is TickData other && Equals(other);

        /// <summary>
        /// Returns a hash code for this instance based on all fields.
        /// </summary>
        /// <returns>The computed hash code.</returns>
        public override int GetHashCode() =>
            HashCode.Combine(TimestampTicks, Price, Volume, TickIndex, Type);

        /// <summary>
        /// Determines whether two <see cref="TickData"/> instances are equal.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns>
        /// <c>true</c> if both instances are equal; otherwise <c>false</c>.
        /// </returns>
        public static bool operator ==(TickData left, TickData right) =>
            left.Equals(right);

        /// <summary>
        /// Determines whether two <see cref="TickData"/> instances are not equal.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns>
        /// <c>true</c> if the instances are not equal; otherwise <c>false</c>.
        /// </returns>
        public static bool operator !=(TickData left, TickData right) =>
            !left.Equals(right);

    }
}
