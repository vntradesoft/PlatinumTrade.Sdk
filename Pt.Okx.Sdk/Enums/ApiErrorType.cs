namespace Pt.Okx.Sdk.Enums
{
    /// <summary>
    /// SDK error classification used by API result wrappers.
    /// </summary>
    public enum ApiErrorType
    {
        /// <summary>Unknown or unclassified error.</summary>
        Unknown,

        /// <summary>Network or connection error, such as timeout or DNS failure.</summary>
        Network,

        /// <summary>Internal error within the SDK or application.</summary>
        InternalError,

        /// <summary>The server returned a non-success HTTP status code.</summary>
        ServerError,

        /// <summary>Authentication failed, such as invalid API key or signature mismatch.</summary>
        Authentication,

        /// <summary>The request was rate-limited by OKX.</summary>
        RateLimit,

        /// <summary>The response could not be parsed because of an unexpected format.</summary>
        Deserialization,

        /// <summary>Business logic error returned by OKX, such as insufficient funds.</summary>
        BusinessLogic,
    }
}
