using System.Diagnostics.CodeAnalysis;

namespace Pt.Okx.Sdk.Common;

/// <summary>
/// Represents the result of an API operation.
/// </summary>
/// <remarks>
/// This type encapsulates both success and failure outcomes.
/// When <see cref="Success"/> is <c>false</c>, <see cref="Error"/> will contain details.
/// </remarks>
public class ApiResult
{
    /// <summary>
    /// Gets a value indicating whether the operation completed successfully.
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// Gets the error details if the operation failed; otherwise <c>null</c>.
    /// </summary>
    public ApiError? Error { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResult"/> class.
    /// </summary>
    /// <param name="success">Indicates whether the operation succeeded.</param>
    /// <param name="error">The error information if failed; otherwise <c>null</c>.</param>
    internal ApiResult(bool success, ApiError? error)
    {
        Success = success;
        Error = error;
    }

    /// <summary>
    /// Enables implicit conversion of <see cref="ApiResult"/> to <see cref="bool"/>.
    /// </summary>
    /// <remarks>
    /// Returns <c>true</c> if <see cref="Success"/> is <c>true</c>; otherwise <c>false</c>.
    /// This allows usage in boolean expressions.
    /// <para>Example:</para>
    /// <code>
    /// if (result)
    /// {
    ///     // Handle success
    /// }
    /// </code>
    /// ⚠️ Use with caution: this conversion may hide error details.
    /// Prefer checking <see cref="Success"/> and <see cref="Error"/> explicitly in critical flows.
    /// </remarks>
    /// <param name="result">The result instance to evaluate.</param>
    /// <returns>
    /// <c>true</c> if the operation succeeded; otherwise <c>false</c>.
    /// </returns>
    public static implicit operator bool(ApiResult? result) => result?.Success ?? false;

    /// <summary>
    /// Returns whether the operation succeeded.
    /// </summary>
    /// <returns><c>true</c> if the operation succeeded; otherwise <c>false</c>.</returns>
    public bool ToBoolean() => Success;

    /// <summary>
    /// Returns a string representation of the result.
    /// </summary>
    /// <returns>
    /// <c>"Success"</c> if successful; otherwise an error description.
    /// </returns>
    public override string ToString() =>
        Success ? "Success" : $"Error: {Error}";
}

/// <summary>
/// Represents the result of an API operation that returns data.
/// </summary>
/// <typeparam name="T">The type of the returned data.</typeparam>
/// <remarks>
/// When <see cref="ApiResult.Success"/> is <c>true</c>, <see cref="Data"/> contains the result.
/// Otherwise, <see cref="ApiResult.Error"/> provides failure details.
/// </remarks>
public class ApiResult<T> : ApiResult
{
    /// <summary>
    /// Gets the data returned by the operation.
    /// Only valid when <see cref="ApiResult.Success"/> is <c>true</c>.
    /// </summary>
    public T Data { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResult{T}"/> class.
    /// </summary>
    /// <param name="data">The returned data.</param>
    /// <param name="success">Indicates whether the operation succeeded.</param>
    /// <param name="error">The error if failed; otherwise <c>null</c>.</param>
    internal ApiResult([AllowNull] T data, bool success, ApiError? error)
        : base(success, error)
    {
        Data = data!;
    }

    /// <summary>
    /// Deconstructs the result into its components.
    /// </summary>
    /// <param name="success">Indicates whether the operation succeeded.</param>
    /// <param name="data">The returned data if successful; otherwise default.</param>
    /// <param name="error">The error if failed; otherwise <c>null</c>.</param>
    /// <example>
    /// <code>
    /// var (ok, data, error) = result;
    /// </code>
    /// </example>
    public void Deconstruct(out bool success, out T? data, out ApiError? error)
    {
        success = Success;
        data = Data;
        error = Error;
    }

    /// <summary>
    /// Attempts to extract the result data or error in a null-safe manner.
    /// </summary>
    /// <param name="data">Receives the data if successful; otherwise default.</param>
    /// <param name="error">Receives the error if failed; otherwise <c>null</c>.</param>
    /// <returns><c>true</c> if the operation succeeded; otherwise <c>false</c>.</returns>
    public bool GetResultOrError(
        [MaybeNullWhen(false)] out T data,
        [NotNullWhen(false)] out ApiError? error)
    {
        if (Success)
        {
            data = Data!;
            error = null;
            return true;
        }

        data = default!;
        error = Error!;
        return false;
    }

    /// <summary>
    /// Maps the data to a new type if successful, preserving the error otherwise.
    /// </summary>
    /// <typeparam name="TValue">The target type.</typeparam>
    /// <param name="mapper">The mapping function.</param>
    /// <returns>A mapped <see cref="ApiResult{TValue}"/>.</returns>
    public ApiResult<TValue> Map<TValue>(Func<T, TValue> mapper)
    {
        ArgumentNullException.ThrowIfNull(mapper);

        if (!Success)
            return ApiResultFactory.Fail<TValue>(Error!);

        return ApiResultFactory.Ok(mapper(Data));
    }

    /// <summary>
    /// Creates a successful result with new data.
    /// </summary>
    /// <typeparam name="TValue">The new data type.</typeparam>
    /// <param name="newData">The new data.</param>
    /// <returns>A successful <see cref="ApiResult{TValue}"/>.</returns>
    public ApiResult<TValue> AsData<TValue>(TValue newData)
    {
        return ApiResultFactory.Ok(newData);
    }

    /// <summary>
    /// Converts this result into a failed result of another type.
    /// </summary>
    /// <typeparam name="TValue">The target type.</typeparam>
    /// <returns>A failed <see cref="ApiResult{TValue}"/> with the same error.</returns>
    public ApiResult<TValue> AsError<TValue>()
    {
        return ApiResultFactory.Fail<TValue>(Error ?? new ApiError("Unknown error"));
    }

    /// <summary>
    /// Returns whether the operation succeeded.
    /// </summary>
    public bool IsSuccess() => Success;

#pragma warning disable CA2225
    /// <summary>
    /// Enables implicit conversion of <see cref="ApiResult{T}"/> to <see cref="bool"/>.
    /// </summary>
    public static implicit operator bool(ApiResult<T> result)
        => result?.Success ?? false;
#pragma warning restore CA2225

    /// <summary>
    /// Returns a string representation of the result.
    /// </summary>
    public override string ToString() =>
        Success ? $"Success: {Data}" : $"Error: {Error}";
}


/// <summary>
/// Provides factory methods for creating <see cref="ApiResult"/> instances.
/// </summary>
public static class ApiResultFactory
{
    /// <summary>
    /// Creates a successful result with data.
    /// </summary>
    /// <typeparam name="TValue">The type of the data.</typeparam>
    /// <param name="data">The result data.</param>
    /// <returns>A successful <see cref="ApiResult{TValue}"/>.</returns>
    public static ApiResult<TValue> Ok<TValue>(TValue data) =>
        new(data, success: true, error: null);

    /// <summary>
    /// Creates a successful result without data.
    /// </summary>
    /// <returns>A successful <see cref="ApiResult"/>.</returns>
    public static ApiResult Ok() =>
        new(success: true, error: null);

    /// <summary>
    /// Creates a failed result with error.
    /// </summary>
    /// <typeparam name="TValue">The expected data type.</typeparam>
    /// <param name="error">The error information.</param>
    /// <returns>A failed <see cref="ApiResult{TValue}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when error is null.</exception>
    public static ApiResult<TValue> Fail<TValue>(ApiError error) =>
        new(default!, success: false, error: error ?? throw new ArgumentNullException(nameof(error)));

    /// <summary>
    /// Creates a failed result with error.
    /// </summary>
    /// <param name="error">The error information.</param>
    /// <returns>A failed <see cref="ApiResult"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when error is null.</exception>
    public static ApiResult Fail(ApiError error) =>
        new(success: false, error: error ?? throw new ArgumentNullException(nameof(error)));
}
