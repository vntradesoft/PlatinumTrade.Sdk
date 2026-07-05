using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Pt.Okx.Sdk.Strategy.Parameters
{
    /// <summary>
    /// Represents the allowed range for a parameter value.
    /// </summary>
    public class ValueRange
    {
        /// <summary>Gets or sets the minimum allowed value.</summary>
        public double? Min { get; set; }
        /// <summary>Gets or sets the maximum allowed value.</summary>
        public double? Max { get; set; }
    }

    /// <summary>
    /// Provides data for the parameter changed event.
    /// </summary>
    public class ParameterChangedEventArgs : EventArgs
    {
        /// <summary>Gets the key associated with the parameter.</summary>
        public string Key { get; }
        /// <summary>Gets the parameter that was changed.</summary>
        public InputParameter Parameter { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterChangedEventArgs"/> class.
        /// </summary>
        public ParameterChangedEventArgs(string key, InputParameter parameter)
        {

            ArgumentNullException.ThrowIfNull(key);
            ArgumentNullException.ThrowIfNull(parameter);

            Key = key;
            Parameter = parameter;
        }
    }

    /// <summary>
    /// Base class for parameter value types, providing notification, metadata, and validation support.
    /// </summary>
    public abstract class InputParameter : INotifyPropertyChanged
    {
        /// <summary>
        /// The list of allowed options for the parameter.
        /// </summary>
        protected readonly HashSet<string> _options = new HashSet<string>();

        /// <summary>Gets or sets the parameter type name.</summary>
        public string Type { get; set; } = null!;
        /// <summary>Gets or sets the valid range for the parameter.</summary>
        public ValueRange Range { get; set; } = new ValueRange();
        /// <summary>Gets or sets the description for the parameter.</summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>Gets or sets the allowed options (useful for enums or selection lists).</summary>
        public HashSet<string> Options => _options;

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>Retrieves the current value of the parameter.</summary>
        public abstract object GetRawValue();
        /// <summary>Sets the current value of the parameter.</summary>
        public abstract void SetValue(object value);
        /// <summary>Gets a display-friendly string representation of the current value.</summary>
        public abstract string GetDisplayValue();
        /// <summary>Validates the provided value.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="errorMessage">When this method returns, contains an error message if validation fails.</param>
        /// <returns><c>true</c> if the value is valid; otherwise, <c>false</c>.</returns>
        public abstract bool ValidateValue(object value, out string errorMessage);
    }

    /// <summary>
    /// A double-precision floating-point parameter.
    /// </summary>
    public class DoubleParameter : InputParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleParameter"/> class.
        /// </summary>
        public DoubleParameter() { Type = "double"; }
        private double _value;
        /// <summary>Gets or sets the double value.</summary>
        public double Value
        {
            get => _value;
            set
            {
                const double Tolerance = 1e-9;
                if (Math.Abs(_value - value) > Tolerance)
                {
                    if (Range.Min.HasValue && value < Range.Min.Value)
                        throw new ArgumentException($"Value {value} is below minimum {Range.Min.Value}");
                    if (Range.Max.HasValue && value > Range.Max.Value)
                        throw new ArgumentException($"Value {value} is above maximum {Range.Max.Value}");

                    _value = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <inheritdoc/>
        public override object GetRawValue() => Value;
        /// <inheritdoc/>
        public override void SetValue(object value) => Value = Convert.ToDouble(value);
        /// <inheritdoc/>
        public override string GetDisplayValue() => Value.ToString();
        /// <inheritdoc/>
        public override bool ValidateValue(object value, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (value is null)
            {
                errorMessage = "Value cannot be null";
                return false;
            }

            double doubleValue;

            if (value is double d)
            {
                doubleValue = d;
            }
            else if(value is IConvertible)
            {
                try
                {
                    doubleValue = Convert.ToDouble(value);
                }
                catch (InvalidOperationException)
                {
                    errorMessage = "Invalid double value";
                    return false;
                }
            }
            else
            {
                errorMessage = "Invalid double value";
                return false;
            }

            if (Range.Min.HasValue && doubleValue < Range.Min.Value)
            {
                errorMessage = $"Value {doubleValue} is below minimum {Range.Min.Value}";
                return false;
            }
            if (Range.Max.HasValue && doubleValue > Range.Max.Value)
            {
                errorMessage = $"Value {doubleValue} is above maximum {Range.Max.Value}";
                return false;
            }
            return true;
        }
    }

    /// <summary>
    /// An integer parameter.
    /// </summary>
    public class IntParameter : InputParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntParameter"/> class.
        /// </summary>
        public IntParameter() { Type = "int"; }
        private int _value;

        /// <summary>Gets or sets the integer value.</summary>
        public int Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    if (Range.Min.HasValue && value < Range.Min.Value)
                        throw new ArgumentException($"Value {value} is below minimum {Range.Min.Value}");
                    if (Range.Max.HasValue && value > Range.Max.Value)
                        throw new ArgumentException($"Value {value} is above maximum {Range.Max.Value}");

                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <inheritdoc/>
        public override object GetRawValue() => Value;
        /// <inheritdoc/>
        public override void SetValue(object value) => Value = Convert.ToInt32(value);
        /// <inheritdoc/>
        public override string GetDisplayValue() => Value.ToString();
        /// <inheritdoc/>
        public override bool ValidateValue(object value, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (value is null)
            {
                errorMessage = "Value cannot be null";
                return false;
            };

            int intValue;
            if(value is int i)
            {
                intValue = i;
            }
            else if (value is IConvertible)
            {
                try
                {
                    intValue = Convert.ToInt32(value);
                }
                catch (InvalidOperationException)
                {
                    errorMessage = "Invalid integer value";
                    return false;
                }
            }
            else
            {
                errorMessage = "Invalid integer value";
                return false;
            }

            if (Range.Min.HasValue && intValue < Range.Min.Value)
            {
                errorMessage = $"Value {intValue} is below minimum {Range.Min.Value}";
                return false;
            }
            if (Range.Max.HasValue && intValue > Range.Max.Value)
            {
                errorMessage = $"Value {intValue} is above maximum {Range.Max.Value}";
                return false;
            }
            return true;
        }
    }

    /// <summary>
    /// A string parameter.
    /// </summary>
    public class StringParameter : InputParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringParameter"/> class.
        /// </summary>
        public StringParameter() { Type = "string"; }
        private string _value = string.Empty;

        /// <summary>Gets or sets the string value.</summary>
        public string Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <inheritdoc/>
        public override object GetRawValue() => Value;
        /// <inheritdoc/>
        public override void SetValue(object value) => Value = value?.ToString() ?? "";
        /// <inheritdoc/>
        public override string GetDisplayValue() => Value ?? "";
        /// <inheritdoc/>
        public override bool ValidateValue(object value, out string errorMessage)
        {
            errorMessage = string.Empty;
            return true;
        }
    }

    /// <summary>
    /// A decimal parameter for precise financial values.
    /// </summary>
    public class DecimalParameter : InputParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalParameter"/> class.
        /// </summary>
        public DecimalParameter() { Type = "decimal"; }

        private decimal _value;

        /// <summary>Gets or sets the decimal value.</summary>
        public decimal Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    if (Range.Min.HasValue && value < Convert.ToDecimal(Range.Min.Value))
                        throw new ArgumentException($"Value {value} is below minimum {Range.Min.Value}");
                    if (Range.Max.HasValue && value > Convert.ToDecimal(Range.Max.Value))
                        throw new ArgumentException($"Value {value} is above maximum {Range.Max.Value}");

                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <inheritdoc/>
        public override object GetRawValue() => Value;

        /// <inheritdoc/>
        public override void SetValue(object value) => Value = Convert.ToDecimal(value);

        /// <inheritdoc/>
        public override string GetDisplayValue() => Value.ToString();

        /// <inheritdoc/>
        public override bool ValidateValue(object value, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (value is null)
            {
                errorMessage = "Value cannot be null";
                return false;
            }

            decimal decimalValue;
            if (value is decimal d)
            {
                decimalValue = d;
            }
            else if (value is IConvertible)
            {
                try
                {
                    decimalValue = Convert.ToDecimal(value);
                }
                catch (InvalidOperationException)
                {
                    errorMessage = "Invalid decimal value";
                    return false;
                }
            }
            else
            {
                errorMessage = "Invalid decimal value";
                return false;
            }

            if (Range.Min.HasValue && decimalValue < Convert.ToDecimal(Range.Min.Value))
            {
                errorMessage = $"Value {decimalValue} is below minimum {Range.Min.Value}";
                return false;
            }
            if (Range.Max.HasValue && decimalValue > Convert.ToDecimal(Range.Max.Value))
            {
                errorMessage = $"Value {decimalValue} is above maximum {Range.Max.Value}";
                return false;
            }

            return true;
        }
    }

    /// <summary>
    /// A timespan parameter for durations.
    /// </summary>
    public class TimeSpanParameter : InputParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanParameter"/> class.
        /// </summary>
        public TimeSpanParameter() { Type = "timespan"; }

        private TimeSpan _value;

        /// <summary>Gets or sets the timespan value.</summary>
        public TimeSpan Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <inheritdoc/>
        public override object GetRawValue() => Value;

        /// <inheritdoc/>
        public override void SetValue(object value)
        {
            if (value is TimeSpan ts)
            {
                Value = ts;
                return;
            }

            if (value is string text && TimeSpan.TryParse(text, out var parsed))
            {
                Value = parsed;
                return;
            }

            Value = Convert.ToDateTime(value).TimeOfDay;
        }

        /// <inheritdoc/>
        public override string GetDisplayValue() => Value.ToString("c");

        /// <inheritdoc/>
        public override bool ValidateValue(object value, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (value is null)
            {
                errorMessage = "Value cannot be null";
                return false;
            }

            if (value is TimeSpan)
                return true;

            if (value is string text && TimeSpan.TryParse(text, out _))
                return true;

            errorMessage = "Invalid timespan value";
            return false;
        }
    }

    /// <summary>
    /// A file path parameter.
    /// </summary>
    public class FilePathParameter : StringParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilePathParameter"/> class.
        /// </summary>
        public FilePathParameter() { Type = "filepath"; }

        /// <inheritdoc/>
        public override bool ValidateValue(object value, out string errorMessage)
        {
            errorMessage = string.Empty;

            var text = value?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(text))
                return true;

            if (text.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            {
                errorMessage = "Invalid file path";
                return false;
            }

            try
            {
                _ = Path.GetFullPath(text);
                return true;
            }
            catch (ArgumentException)
            {
                errorMessage = "Invalid file path";
                return false;
            }
            catch (NotSupportedException)
            {
                errorMessage = "Invalid file path";
                return false;
            }
            catch (PathTooLongException)
            {
                errorMessage = "Invalid file path";
                return false;
            }
        }
    }

    /// <summary>
    /// A date/time parameter.
    /// </summary>
    public class DateTimeParameter : InputParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeParameter"/> class.
        /// </summary>
        public DateTimeParameter() { Type = "datetime"; }
        private DateTime _value;

        /// <summary>Gets or sets the <see cref="DateTime"/> value.</summary>
        public DateTime Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <inheritdoc/>
        public override object GetRawValue() => Value;
        /// <inheritdoc/>
        public override void SetValue(object value) => Value = Convert.ToDateTime(value);
        /// <inheritdoc/>
        public override string GetDisplayValue() => Value.ToString("yyyy/MM/dd HH:mm:ss");
        /// <inheritdoc/>
        public override bool ValidateValue(object value, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (value is null)
            {
                errorMessage = "Value cannot be null";
                return false;
            }

            if (value is DateTime)
                return true;

            if (value is IConvertible)
            {
                try
                {
                    _ = Convert.ToDateTime(value);
                    return true;
                }
                catch (InvalidOperationException)
                {
                    errorMessage = "Invalid datetime value";
                    return false;
                }
            }

            errorMessage = "Invalid datetime value";
            return false;
        }

    }

    /// <summary>
    /// An enum-style parameter, backed by a string.
    /// </summary>
    public class EnumParameter : InputParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumParameter"/> class.
        /// </summary>
        public EnumParameter() { Type = "enum"; }
        private string _value = string.Empty;

        /// <summary>Gets or sets the enum value (as a string).</summary>
        public string Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <inheritdoc/>
        public override object GetRawValue() => Value;
        /// <inheritdoc/>
        public override void SetValue(object value) => Value = value?.ToString() ?? "";
        /// <inheritdoc/>
        public override string GetDisplayValue() => Value ?? "";
        /// <inheritdoc/>
        public override bool ValidateValue(object value, out string errorMessage)
        {
            errorMessage = string.Empty;
            var stringValue = value?.ToString() ?? "";

            if (_options.Count > 0 && !_options.Contains(stringValue))
            {
                errorMessage = $"Value {stringValue} is not in allowed options: {string.Join(", ", _options)}";
                return false;
            }
            return true;
        }
    }

    /// <summary>
    /// A boolean parameter.
    /// </summary>
    public class BoolParameter : InputParameter
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BoolParameter"/> class.
        /// </summary>
        public BoolParameter() { Type = "boolean"; }

        private bool _value;

        /// <summary>Gets or sets the boolean value.</summary>
        public bool Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <inheritdoc/>
        public override object GetRawValue() => Value;

        /// <inheritdoc/>
        public override void SetValue(object value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value), "Value cannot be null");

            if (value is bool boolValue)
            {
                Value = boolValue;
                return;
            }

            if (value is string str)
            {
                str = str.Trim().ToUpperInvariant();

                if (str is "1" or "YES" or "Y" or "ON" or "TRUE")
                {
                    Value = true;
                    return;
                }

                if (str is "0" or "NO" or "N" or "OFF" or "FALSE")
                {
                    Value = false;
                    return;
                }

                throw new ArgumentException($"Invalid boolean value: '{value}'");
            }

            // fallback parse
            if (value is IConvertible)
            {
                try
                {
                    Value = Convert.ToBoolean(value);
                    return;
                }
                catch (InvalidOperationException ) 
                {
                    // ignore, fall through
                }
            }

            throw new ArgumentException($"Invalid boolean value type: {value.GetType()}");
        }

        /// <inheritdoc/>
        public override string GetDisplayValue() => Value.ToString();

        /// <inheritdoc/>
        public override bool ValidateValue(object value, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (value is null)
            {
                errorMessage = "Value cannot be null";
                return false;
            }

            if (value is bool)
                return true;

            if (value is string str)
            {
                str = str.Trim().ToUpperInvariant();

                if (str is "TRUE" or "FALSE" or "1" or "0" or "YES" or "NO" or "Y" or "N" or "ON" or "OFF")
                    return true;
            }

            errorMessage = $"Invalid boolean value: '{value}'";
            return false;
        }
    }

    /// <summary>
    /// A parameter representing a section header/separator in the configuration.
    /// It does not carry a functional value.
    /// </summary>
    public class HeaderParameter : InputParameter
    {
        /// <summary>Initializes a new instance of the <see cref="HeaderParameter"/> class.</summary>
        public HeaderParameter() { Type = "header"; }

        /// <summary>Initializes a new instance of the <see cref="HeaderParameter"/> class with a description.</summary>
        public HeaderParameter(string description) : this()
        {
            Description = description;
        }

        private string _value = string.Empty;

        /// <summary>Gets or sets the header value (typically empty or matching description).</summary>
        public string Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <inheritdoc/>
        public override object GetRawValue() => Value;

        /// <inheritdoc/>
        public override void SetValue(object value)
        {
            Value = value?.ToString() ?? string.Empty;
        }

        /// <inheritdoc/>
        public override string GetDisplayValue() => Value;

        /// <inheritdoc/>
        public override bool ValidateValue(object value, out string errorMessage)
        {
            errorMessage = string.Empty;
            return true;
        }
    }

    /// <summary>
    /// A generic list/collection parameter for storing multiple values of the same primitive type.
    /// Values are stored as a comma-separated (or custom-separated) string in JSON.
    /// </summary>
    /// <typeparam name="T">The element type: string, int, decimal, double, bool, etc.</typeparam>
#pragma warning disable CA1002 // Do not expose generic lists - consistency with other parameter Value properties
    public class ListParameter<T> : InputParameter where T : notnull
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListParameter{T}"/> class.
        /// </summary>
        public ListParameter()
        {
            Type = $"list[{typeof(T).Name.ToUpperInvariant()}]";
        }

        private List<T> _value = new();
        private string _separator = ",";

        /// <summary>Gets or sets the separator used to delimit values in the display/serialized form (default: comma).</summary>
        public string Separator
        {
            get => _separator;
            set => _separator = value ?? ",";
        }

        /// <summary>Gets or sets the list of values.</summary>
        /// <remarks>Setter is necessary for binding and JSON deserialization, even though the property is a List{T}.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA2227:Collection properties should be read only")]
        public List<T> Value
        {
            get => _value;
            set
            {
                ArgumentNullException.ThrowIfNull(value);
                _value = new List<T>(value);
                OnPropertyChanged();
            }
        }

        /// <inheritdoc/>
        public override object GetRawValue() => new List<T>(_value);

        /// <inheritdoc/>
        public override void SetValue(object value)
        {
            if (value is null)
            {
                _value.Clear();
                OnPropertyChanged();
                return;
            }

            // If it's already a List<T>, use it directly
            if (value is List<T> list)
            {
                _value = new List<T>(list);
                OnPropertyChanged();
                return;
            }

            // If it's an IEnumerable<T>, convert it
            if (value is System.Collections.Generic.IEnumerable<T> enumerable)
            {
                _value = new List<T>(enumerable);
                OnPropertyChanged();
                return;
            }

            // Try parsing from string (comma-separated or custom separator)
            var text = value.ToString() ?? string.Empty;
            _value = ParseFromString(text);
            OnPropertyChanged();
        }

        /// <inheritdoc/>
        public override string GetDisplayValue()
        {
            if (_value.Count == 0)
                return string.Empty;

            return string.Join(_separator, _value.Select(FormatValue));
        }

        /// <inheritdoc/>
        public override bool ValidateValue(object value, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (value is null)
                return true; // Empty list is valid

            try
            {
                // If it's a string, try parsing to validate each element
                if (value is string text)
                {
                    _ = ParseFromString(text);
                    return true;
                }

                // If it's a List<T> or IEnumerable<T>, it's valid
                if (value is List<T> || value is System.Collections.Generic.IEnumerable<T>)
                    return true;

                errorMessage = $"Invalid value type for list: {value.GetType().Name}";
                return false;
            }
            catch (ArgumentException ex)
            {
                errorMessage = $"Failed to parse list items: {ex.Message}";
                return false;
            }
            catch (InvalidOperationException ex)
            {
                errorMessage = $"Failed to parse list items: {ex.Message}";
                return false;
            }
        }

        private List<T> ParseFromString(string text)
        {
            var result = new List<T>();

            if (string.IsNullOrWhiteSpace(text))
                return result;

            var items = text.Split(new[] { _separator }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in items)
            {
                var trimmed = item.Trim();
                if (string.IsNullOrEmpty(trimmed))
                    continue;

                result.Add(ParseItem(trimmed));
            }

            return result;
        }

        private T ParseItem(string item)
        {
            var targetType = typeof(T);
            var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            if (underlyingType == typeof(string))
            {
                return (T)(object)item;
            }

            if (underlyingType == typeof(int))
            {
                if (!int.TryParse(item, System.Globalization.CultureInfo.InvariantCulture, out var intValue))
                    throw new ArgumentException($"Cannot parse '{item}' as int");
                return (T)(object)intValue;
            }

            if (underlyingType == typeof(decimal))
            {
                if (!decimal.TryParse(item, System.Globalization.CultureInfo.InvariantCulture, out var decimalValue))
                    throw new ArgumentException($"Cannot parse '{item}' as decimal");
                return (T)(object)decimalValue;
            }

            if (underlyingType == typeof(double))
            {
                if (!double.TryParse(item, System.Globalization.CultureInfo.InvariantCulture, out var doubleValue))
                    throw new ArgumentException($"Cannot parse '{item}' as double");
                return (T)(object)doubleValue;
            }

            if (underlyingType == typeof(float))
            {
                if (!float.TryParse(item, System.Globalization.CultureInfo.InvariantCulture, out var floatValue))
                    throw new ArgumentException($"Cannot parse '{item}' as float");
                return (T)(object)floatValue;
            }

            if (underlyingType == typeof(bool))
            {
                var upperItem = item.ToUpperInvariant();
                if (upperItem is "TRUE" or "1" or "YES" or "Y" or "ON")
                    return (T)(object)true;
                if (upperItem is "FALSE" or "0" or "NO" or "N" or "OFF")
                    return (T)(object)false;
                throw new ArgumentException($"Cannot parse '{item}' as bool");
            }

            if (underlyingType == typeof(long))
            {
                if (!long.TryParse(item, System.Globalization.CultureInfo.InvariantCulture, out var longValue))
                    throw new ArgumentException($"Cannot parse '{item}' as long");
                return (T)(object)longValue;
            }

            throw new InvalidOperationException($"Unsupported list element type: {targetType.Name}");
        }

        private static string FormatValue(T item)
        {
            if (item is null)
                return string.Empty;

            var itemType = item.GetType();

            // Format decimals with proper precision
            if (itemType == typeof(decimal))
                return ((decimal)(object)item).ToString(System.Globalization.CultureInfo.InvariantCulture);

            if (itemType == typeof(double))
                return ((double)(object)item).ToString(System.Globalization.CultureInfo.InvariantCulture);

            if (itemType == typeof(float))
                return ((float)(object)item).ToString(System.Globalization.CultureInfo.InvariantCulture);

            return item.ToString() ?? string.Empty;
        }
    }
#pragma warning restore CA1002
}

