using Newtonsoft.Json;
using NLog;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace Ez.Web.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.ExceptionHandling.ExceptionLogger" />
    public class NLogExceptionLogger : ExceptionLogger
    {
        private readonly ILogger _logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="NLogExceptionLogger"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public NLogExceptionLogger(ILogger logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// When overridden in a derived class, logs the exception synchronously.
        /// </summary>
        /// <param name="context">The exception logger context.</param>
        public override void Log(ExceptionLoggerContext context)
        {
            _logger.Log(LogLevel.Error, context.Exception);
        }
        /// <summary>
        /// When overridden in a derived class, logs the exception synchronously.
        /// </summary>
        /// <param name="exception">The exception as a string</param>
        public void Log(ExceptionResponse exception)
        {
            _logger.Log(LogLevel.Error, exception);
        }
        /// <summary>
        /// When overridden in a derived class, logs the exception synchronously.
        /// </summary>
        /// <param name="exception">The exception</param>
        public void Log(Exception exception)
        {
            _logger.Log(LogLevel.Error, exception);
        }
    }


    /// <summary></summary>
    public static class HttpResponseMessageExtension
    {
        /// <summary>
        /// Exceptions the response.
        /// </summary>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        /// <returns></returns>
        public static async Task<ExceptionResponse> ExceptionResponse(this HttpResponseMessage httpResponseMessage)
        {
            string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            ExceptionResponse exceptionResponse = JsonConvert.DeserializeObject<ExceptionResponse>(responseContent);
            return exceptionResponse;
        }
    }

    /// <summary></summary>
    public class ExceptionResponse
    {
        /// <summary></summary>
        public string Message { get; set; }
        /// <summary></summary>
        public string ExceptionMessage { get; set; }
        /// <summary></summary>
        public string ExceptionType { get; set; }
        /// <summary></summary>
        public string StackTrace { get; set; }
        /// <summary></summary>
        public ExceptionResponse InnerException { get; set; }
    }
}