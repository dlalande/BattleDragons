using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NLog;

namespace Dragons.Service.Extensions
{
        /// <summary>
    /// Extends the logger class with handy methods for wrapping actions or functions in a logging aspect.
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Wraps an action in trace logging upon enter and exit and logs error if caught and rethrows.
        /// </summary>
        /// <param name="logger">Logger used to trace and error log the action.</param>
        /// <param name="caller">Name of calling context.  This value is traced on enter and exit.</param>
        /// <param name="action">Action to invoke.</param>
        /// <param name="executionTimeThreshold">Length of time the action is estimated to run and will be logged as warning when execution exceeds timespan.</param>
        public static void LogExecute(this Logger logger, string caller, Action action,
            TimeSpan? executionTimeThreshold = null)
        {
            LogExecute(logger, caller, action, (c, e) =>
            {
                logger.Error(e.ToLoggableException(), $"{caller} error.");
                throw e;
            }, executionTimeThreshold);
        }

        /// <summary>
        /// Wraps an action in trace logging upon enter and exit and if exception is caught, logs error and rethrows by default but can be overridden with <paramref name="exceptionHandler">exceptionHandler</paramref>.
        /// </summary>
        /// <param name="logger">Logger used to trace and error log the function.</param>
        /// <param name="caller"></param>
        /// <param name="action">Action to invoke.</param>
        /// <param name="exceptionHandler">Action(caller,exception), when set, overrides default logging and rethrow behavior when an exception is throw in the given <paramref name="action">action</paramref>.</param>
        /// <param name="executionTimeThreshold">Length of time the action is estimated to run and will be logged as warning when execution exceeds timespan.</param>
        public static void LogExecute(this Logger logger, string caller, Action action,
            Action<string, Exception> exceptionHandler, TimeSpan? executionTimeThreshold = null)
        {
            var stopWatch = Stopwatch.StartNew();
            try
            {
                logger.Trace($"{caller} enter.");
                action.Invoke();
            }
            catch (Exception e)
            {
                exceptionHandler.Invoke(caller, e);
            }
            finally
            {
                var elapsed = stopWatch.Elapsed;
                logger.Trace($"{caller} exited in {elapsed.TotalMilliseconds} milliseconds.");
                if (executionTimeThreshold.HasValue && elapsed > executionTimeThreshold.Value)
                    logger.Warn(
                        $"{caller} exited in {elapsed.TotalMilliseconds} milliseconds and exceeded execution time threshold of {executionTimeThreshold.Value.TotalMilliseconds} milliseconds.");
            }
        }

        /// <summary>
        /// Wraps a function in trace logging upon enter and exit and logs error if caught and rethrows.
        /// </summary>
        /// <typeparam name="TResult">Type of result to return from invoked function.</typeparam>
        /// <param name="logger">Logger used to trace and error log the function.</param>
        /// <param name="caller">Name of calling context.  This value is traced on enter and exit.</param>
        /// <param name="func">Function to invoke.</param>
        /// <param name="executionTimeThreshold">Length of time the function is estimated to run and will be logged as warning when execution exceeds timespan.</param>
        /// <returns>Returns results from function invoke. Rethrows any caught exceptions.</returns>
        public static TResult LogExecute<TResult>(this Logger logger, string caller, Func<TResult> func, TimeSpan? executionTimeThreshold = null)
        {
            return LogExecute(logger, caller, func, (c, e) =>
            {
                logger.Error(e.ToLoggableException(), $"{caller} error.");
                throw e;
            }, executionTimeThreshold);
        }

        /// <summary>
        /// Wraps a function in trace logging upon enter and exit and logs error if caught and rethrows.
        /// </summary>
        /// <typeparam name="TResult">Type of result to return from invoked function.</typeparam>
        /// <param name="logger">Logger used to trace and error log the function.</param>
        /// <param name="caller"></param>
        /// <param name="func">Function to invoke.</param>
        /// <param name="exceptionHandler">Func(caller,exception), when set, overrides default logging and rethrow behavior when an exception is throw in the given <paramref name="func">func</paramref>.</param>
        /// <param name="executionTimeThreshold">Length of time the action is estimated to run and will be logged as warning when execution exceeds timespan.</param>
        /// <returns>Returns results from function invoke. Rethrows any caught exceptions.</returns>
        public static TResult LogExecute<TResult>(this Logger logger, string caller, Func<TResult> func,
            Func<string, Exception, TResult> exceptionHandler, TimeSpan? executionTimeThreshold = null)
        {
            var stopWatch = Stopwatch.StartNew();
            try
            {
                logger.Trace($"{caller} enter.");
                return func.Invoke();
            }
            catch (Exception e)
            {
                return exceptionHandler.Invoke(caller, e);
            }
            finally
            {
                var elapsed = stopWatch.Elapsed;
                logger.Trace($"{caller} exited in {elapsed.TotalMilliseconds} milliseconds.");
                if (executionTimeThreshold.HasValue && elapsed > executionTimeThreshold.Value)
                    logger.Warn(
                        $"{caller} exited in {elapsed.TotalMilliseconds} milliseconds and exceeded execution time threshold of {executionTimeThreshold.Value.TotalMilliseconds} milliseconds.");
            }
        }

        /// <summary>
        /// Wraps an awaitable function in trace logging upon enter and exit and logs error if caught and rethrows.
        /// </summary>
        /// <typeparam name="TResult">Type of result to return from invoked function.</typeparam>
        /// <param name="logger">Logger used to trace and error log the function.</param>
        /// <param name="caller">Name of calling context.  This value is traced on enter and exit.</param>
        /// <param name="func">Function to invoke.</param>
        /// <param name="executionTimeThreshold">Length of time the function is estimated to run and will be logged as warning when execution exceeds timespan.</param>
        /// <returns>Returns results from function invoke. Rethrows any caught exceptions.</returns>
        public static async Task<TResult> LogExecuteAsync<TResult>(this Logger logger, string caller, Func<Task<TResult>> func, TimeSpan? executionTimeThreshold = null)
        {
            return await LogExecute(logger, caller, func, (c, e) =>
            {
                logger.Error(e.ToLoggableException(), $"{caller} error.");
                throw e;
            }, executionTimeThreshold);
        }

        /// <summary>
        /// Wraps an awaitable function in trace logging upon enter and exit and logs error if caught and rethrows.
        /// </summary>
        /// <typeparam name="TResult">Type of result to return from invoked function.</typeparam>
        /// <param name="logger">Logger used to trace and error log the function.</param>
        /// <param name="caller">Name of calling context.  This value is traced on enter and exit.</param>
        /// <param name="func">Function to invoke.</param>
        /// <param name="exceptionHandler">Func(caller,exception,TResult), when set, overrides default logging and rethrow behavior when an exception is throw in the given <paramref name="func">func</paramref>.</param>
        /// <param name="executionTimeThreshold">Length of time the function is estimated to run and will be logged as warning when execution exceeds timespan.</param>
        /// <returns>Returns results from function invoke. Rethrows any caught exceptions.</returns>
        public static async Task<TResult> LogExecuteAsync<TResult>(this Logger logger, string caller, Func<Task<TResult>> func, Func<string, Exception, TResult> exceptionHandler, TimeSpan? executionTimeThreshold = null)
        {
            var stopWatch = Stopwatch.StartNew();
            try
            {
                logger.Trace($"{caller} enter.");
                return await func.Invoke();
            }
            catch (Exception e)
            {
                return exceptionHandler.Invoke(caller, e);
            }
            finally
            {
                var elapsed = stopWatch.Elapsed;
                logger.Trace($"{caller} exited in {elapsed.TotalMilliseconds} milliseconds.");
                if (executionTimeThreshold.HasValue && elapsed > executionTimeThreshold.Value)
                    logger.Warn(
                        $"{caller} exited in {elapsed.TotalMilliseconds} milliseconds and exceeded execution time threshold of {executionTimeThreshold.Value.TotalMilliseconds} milliseconds.");
            }
        }

        /// <summary>
        /// Wraps an awaitable function in trace logging upon enter and exit and logs error if caught and rethrows.
        /// </summary>
        /// <param name="logger">Logger used to trace and error log the function.</param>
        /// <param name="caller">Name of calling context.  This value is traced on enter and exit.</param>
        /// <param name="func">Function to invoke.</param>
        /// <param name="executionTimeThreshold">Length of time the function is estimated to run and will be logged as warning when execution exceeds timespan.</param>
        /// <returns>Returns results from function invoke. Rethrows any caught exceptions.</returns>
        public static async Task LogExecuteAsync(this Logger logger, string caller, Func<Task> func, TimeSpan? executionTimeThreshold = null)
        {
            await LogExecute(logger, caller, func, (c, e) =>
            {
                logger.Error(e.ToLoggableException(), $"{caller} error.");
                throw e;
            }, executionTimeThreshold);
        }

        /// <summary>
        /// Wraps an awaitable function in trace logging upon enter and exit and logs error if caught and rethrows.
        /// </summary>
        /// <param name="logger">Logger used to trace and error log the function.</param>
        /// <param name="caller">Name of calling context.  This value is traced on enter and exit.</param>
        /// <param name="func">Function to invoke.</param>
        /// <param name="exceptionHandler">Func(caller,exception,Task), when set, overrides default logging and rethrow behavior when an exception is throw in the given <paramref name="func">func</paramref>.</param>
        /// <param name="executionTimeThreshold">Length of time the function is estimated to run and will be logged as warning when execution exceeds timespan.</param>
        /// <returns>Returns results from function invoke. Rethrows any caught exceptions.</returns>
        public static async Task LogExecuteAsync(this Logger logger, string caller, Func<Task> func, Func<string, Exception, Task> exceptionHandler, TimeSpan? executionTimeThreshold = null)
        {
            var stopWatch = Stopwatch.StartNew();
            try
            {
                logger.Trace($"{caller} enter.");
                await func.Invoke();
            }
            catch (Exception e)
            {
                await exceptionHandler.Invoke(caller, e);
            }
            finally
            {
                var elapsed = stopWatch.Elapsed;
                logger.Trace($"{caller} exited in {elapsed.TotalMilliseconds} milliseconds.");
                if (executionTimeThreshold.HasValue && elapsed > executionTimeThreshold.Value)
                    logger.Warn(
                        $"{caller} exited in {elapsed.TotalMilliseconds} milliseconds and exceeded execution time threshold of {executionTimeThreshold.Value.TotalMilliseconds} milliseconds.");
            }
        }
    }
}