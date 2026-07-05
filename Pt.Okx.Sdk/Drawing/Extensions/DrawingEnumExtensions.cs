using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

namespace Pt.Okx.Sdk.Drawing.Extensions;

/// <summary>
/// Provides extension methods for working with enum values,
/// including retrieving <see cref="DescriptionAttribute"/> metadata.
/// </summary>
public static class DrawingEnumExtensions
{
    private static readonly ConcurrentDictionary<(Type, string), string> Cache = new();

    /// <summary>
    /// Gets the description defined by <see cref="DescriptionAttribute"/> for the specified enum value.
    /// Falls back to the enum name if no description is defined.
    /// </summary>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    /// <param name="value">The enum value.</param>
    /// <returns>
    /// The description value if present; otherwise the enum name.
    /// </returns>
    public static string GetDescription<TEnum>(this TEnum value)
        where TEnum : struct, Enum
    {
        var type = typeof(TEnum);
        var name = value.ToString();

        var key = (type, name);

        if (Cache.TryGetValue(key, out var result))
            return result;

        var field = type.GetField(name);
        result = field?
            .GetCustomAttribute<DescriptionAttribute>()?
            .Description
            ?? name;

        Cache[key] = result;

        return result;
    }
}
