using Dragons.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Dragons.Service
{
    /// <summary>
    /// Class used to validate api key on inbound request headers.
    /// </summary>
    public class ApiKeyDelegatingHandler : DelegatingHandler
    {
        private readonly string Key;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="key">Api key to validate against.</param>
        public ApiKeyDelegatingHandler(string key)
        {
            this.Key = key;
        }

        /// <summary>
        /// Checks if the incoming request has the api key in the header.
        /// </summary>
        /// <param name="request">Request to check for header.</param>
        /// <param name="cancellationToken">Token used to cancel the task.</param>
        /// <returns>Returns the http response message.</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!ValidateKey(request))
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
            return base.SendAsync(request, cancellationToken);
        }

        private bool ValidateKey(HttpRequestMessage message)
        {
            if (!message.Headers.Contains(Constants.ApiKeyHeader))
                return false;
            var values = message.Headers.GetValues(Constants.ApiKeyHeader).ToList();
            return Key.Equals(values.FirstOrDefault());
        }
    }
}