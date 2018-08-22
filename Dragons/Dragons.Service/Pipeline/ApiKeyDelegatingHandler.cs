using Dragons.Core;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dragons.Service.Extensions;
using NLog;

namespace Dragons.Service.Pipeline
{
    /// <summary>
    /// Class used to validate api key on inbound request headers.
    /// </summary>
    public class ApiKeyDelegatingHandler : DelegatingHandler
    {
        private readonly string _apiKey;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="apiKey">Api key to validate against.</param>
        public ApiKeyDelegatingHandler(string apiKey)
        {
            this._apiKey = apiKey;
        }

        /// <summary>
        /// Checks if the incoming request has the api key in the header.
        /// </summary>
        /// <param name="request">Request to check for header.</param>
        /// <param name="cancellationToken">Token used to cancel the task.</param>
        /// <returns>Returns the http response message.</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(request.TryGetCustomHeaderValue(Constants.ClientIdHeader, out var clientId))
                MappedDiagnosticsLogicalContext.Set("ClientId", clientId);

            if (!ValidateKey(request))
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
            return base.SendAsync(request, cancellationToken);
        }

        private bool ValidateKey(HttpRequestMessage request)
        {
            if (!request.TryGetCustomHeaderValue(Constants.ApiKeyHeader, out var apiKey)) 
                return false;
            MappedDiagnosticsLogicalContext.Set("ApiKey", apiKey);
            return _apiKey.Equals(apiKey);
        }
    }
}