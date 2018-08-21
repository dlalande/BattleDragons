using System.IdentityModel;
using System.Net.Http;

namespace Dragons.Service
{
    /// <summary>
    /// Extends the HttpRequestMessage with handy methods for managing custom headers.
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// Returns the value of a custom header if it exists.  Returns null if custom header is missing.
        /// </summary>
        /// <param name="request">Request to get custom header from.</param>
        /// <param name="customHeaderKey">Key of custom header.</param>
        /// <param name="value">Custom header value as output.</param>
        /// <returns>Returns true if a custom header exists</returns>
        public static bool TryGetCustomHeaderValue(this HttpRequestMessage request, string customHeaderKey, out string value)
        {
            value = null;
            if (!request.Headers.Contains(customHeaderKey))
                return false;
            value = string.Join(string.Empty, request.Headers.GetValues(customHeaderKey));
            return true;
        }

        /// <summary>
        /// Returns the value of a custom header if it exists.  Throws <see cref="BadRequestException">BadRequestException</see> if custom header is missing.
        /// </summary>
        /// <param name="request">Request to get custom header from.</param>
        /// <param name="customHeaderKey">Key of custom header.</param>
        /// <returns>Returns the value of a custom header if it exists.  Throws <see cref="BadRequestException">BadRequestException</see> if custom header is missing.</returns>
        public static string GetCustomHeaderValue(this HttpRequestMessage request, string customHeaderKey)
        {
            if (!request.Headers.Contains(customHeaderKey))
                throw new BadRequestException($"{customHeaderKey} header missing from request.");

            return string.Join(string.Empty, request.Headers.GetValues(customHeaderKey));
        }
    }
}