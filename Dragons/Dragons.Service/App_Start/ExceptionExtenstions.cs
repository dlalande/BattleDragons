using System;
using System.Net.Http;
using NLog;

namespace Dragons.Service
{
    /// <summary>
    /// Extends the exception class with a method that makes a more meaningful exception for logging.
    /// </summary>
    public static class ExceptionExtenstions
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Returns a more meaningful exception for logging.
        /// </summary>
        /// <param name="exception">Exception to make more log friendly.</param>
        /// <returns>Returns a more meaningful exception for logging.</returns>
        public static Exception ToLoggableException(this Exception exception)
        {

            var type = exception.GetType();
            if (!type.Name.Equals("HttpResponseException", StringComparison.InvariantCultureIgnoreCase))
                return exception;
            try
            {
                var response = (HttpResponseMessage)type.GetProperty("Response").GetValue(exception, null);
                if (response.Content != null)
                    exception = new Exception(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception responseException)
            {
                Logger.Warn(
                    $"There was an error reading the response in a HttpResponseException. {responseException}");
            }
            return exception;
        }
    }
}