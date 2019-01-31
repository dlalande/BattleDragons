using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Dragons.Client
{
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Class used to help deserialize exceptions in http response.
        /// </summary>
        public class HttpResponseException
        {
            /// <summary>
            /// Message of the http response.
            /// </summary>
            public string Message;

            /// <summary>
            /// Message of the exception in the response.
            /// </summary>
            public string ExceptionMessage;

            /// <summary>
            /// Type of exception in the response.
            /// </summary>
            public string ExceptionType;

            /// <summary>
            /// Stack trace of the exception in the response.
            /// </summary>
            public string StackTrace;
        }

        /// <summary>
        /// Throws a <see cref="HttpRequestException">HttpRequestException</see> when the response doesn't have a success status code.
        /// </summary>
        /// <param name="response">Response to extend</param>
        public static void EnsureSuccess(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return;

            var content = response.Content.ReadAsStringAsync().Result;

            if (string.IsNullOrWhiteSpace(content) || !content.StartsWith("{"))
                response.EnsureSuccessStatusCode();

            var responseException = JsonConvert.DeserializeObject<HttpResponseException>(content);

            //TODO: Perhaps try to construct a new exception of the ExceptionType
            var innerException = new Exception(responseException.ExceptionType, new Exception(responseException.StackTrace));

            throw new HttpRequestException(responseException.Message.Equals("An error has occurred.")
                ? responseException.ExceptionMessage
                : responseException.Message, innerException);
        }
    }
}
