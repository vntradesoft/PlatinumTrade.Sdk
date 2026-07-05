using System.Net;
using Pt.Okx.Sdk.Enums;


namespace Pt.Okx.Sdk.Common;

/// <summary>
/// Represents an error returned by the OKX API.
/// </summary>
public class ApiError
{
    /// <summary>
    /// Human-readable error message.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// OKX API error code, if available (e.g. "50001", "51000").
    /// </summary>
    public string? Code { get; }

    /// <summary>
    /// HTTP status code of the response, if applicable.
    /// </summary>
    public HttpStatusCode? HttpStatusCode { get; }

    /// <summary>
    /// The category of error.
    /// </summary>
    public ApiErrorType ErrorType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiError"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="code">The error code.</param>
    /// <param name="httpStatusCode">The HTTP status code.</param>
    /// <param name="errorType">The type of the error.</param>
    public ApiError(
        string message,
        string? code = null,
        HttpStatusCode? httpStatusCode = null,
        ApiErrorType errorType = ApiErrorType.Unknown)
    {
        Message = message;
        Code = code;
        HttpStatusCode = httpStatusCode;
        ErrorType = errorType;
    }

    /// <summary>
    /// Returns a string representation of the error.
    /// </summary>
    public override string ToString() =>
        Code is not null
            ? $"[{Code}] {Message}"
            : Message;
}
