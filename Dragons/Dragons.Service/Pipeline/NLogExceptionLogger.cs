using System.Web.Http.ExceptionHandling;
using NLog;

namespace Dragons.Service.Pipeline
{
    /// <summary>
    /// Class used to log exceptions to NLog <see cref="NLog.Logger">Logger</see>.
    /// </summary>
    public class NLogExceptionLogger : ExceptionLogger
    { 
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        /// <summary>
        /// Logs to NLog <see cref="NLog.Logger">Logger</see> on exception.
        /// </summary>
        /// <param name="context">Context exception occurred in.</param>
        public override void Log(ExceptionLoggerContext context)
        {
            Logger.Error(context.ExceptionContext.Exception);
        }
    }
}