using System.Security.Cryptography;
using System.Text;

namespace Pt.Okx.Sdk.Indicators.Models
{
    /// <summary>
    /// Mutable parameter bag for indicator configuration.
    /// Provides typed set/get helpers and a deterministic hash for identity/fingerprints.
    /// </summary>
    public class IndicatorParameters
    {
        /// <summary>
        /// Internal key-value storage for parameter entries.
        /// </summary>
        private readonly Dictionary<string, object?> _parameters = new();

        /// <summary>
        /// Sets a parameter value by key.
        /// If <paramref name="value"/> is <c>null</c>, <paramref name="defaultValue"/> is stored instead.
        /// </summary>
        /// <typeparam name="TValue">The parameter value type.</typeparam>
        /// <param name="key">Parameter key.</param>
        /// <param name="value">Runtime parameter value (nullable).</param>
        /// <param name="defaultValue">Fallback value used when <paramref name="value"/> is <c>null</c>.</param>
        public void Set<TValue>(string key, TValue? value, TValue defaultValue) => _parameters[key] = value ?? defaultValue;

        /// <summary>
        /// Attempts to get a typed parameter value by key.
        /// Returns <c>default</c> when the key does not exist or the value cannot be cast to <typeparamref name="TValue"/>.
        /// </summary>
        /// <typeparam name="TValue">Expected return type.</typeparam>
        /// <param name="key">Parameter key.</param>
        /// <returns>The typed value if available; otherwise <c>default</c>.</returns>
        public TValue? Get<TValue>(string key)
        {
            if (_parameters.TryGetValue(key, out var value))
            {
                if (value is TValue tValue)
                    return tValue;
                return default;
            }
            return default;
        }

        /// <summary>
        /// Checks whether a parameter exists in the bag.
        /// </summary>
        /// <param name="key">Parameter key to test.</param>
        /// <returns><c>true</c> if the key exists; otherwise <c>false</c>.</returns>
        public bool Contains(string key) => _parameters.ContainsKey(key);

        /// <summary>
        /// Computes a short deterministic hash from all parameters.
        /// Keys are sorted before hashing to keep order-independent results.
        /// </summary>
        /// <returns>An 8-character Base64 prefix derived from SHA-256.</returns>
        public string GetParametersHash()
        {
            var sorted = _parameters.OrderBy(kvp => kvp.Key).ToList();

            var paramString = string.Join(
                "|",
                sorted.Select(kvp => $"{kvp.Key}={kvp.Value}"));

            byte[] hash = SHA256.HashData(
                Encoding.UTF8.GetBytes(paramString));

            return Convert.ToBase64String(hash)[..8];
        }

        /// <summary>
        /// Returns a shallow copy of all parameter entries.
        /// </summary>
        /// <returns>A new dictionary containing the current key-value pairs.</returns>
        public IReadOnlyDictionary<string, object?> All =>
       new Dictionary<string, object?>(_parameters);
    }
}
