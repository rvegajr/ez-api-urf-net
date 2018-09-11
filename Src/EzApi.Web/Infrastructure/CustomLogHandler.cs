using Ez.Common;
using NLog;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ez.Web.Infrastructure
{
    /// <summary></summary>
    public interface ICustomLogHander
    {

    }
    /// <summary></summary>
    /// <seealso cref="System.Net.Http.DelegatingHandler" />
    public class CustomLogHandler : DelegatingHandler, ICustomLogHander
    {
        private readonly ILogger _logger;
        /// <summary></summary>
        /// <param name="logger">The logger.</param>
        public CustomLogHandler(ILogger logger)
        {
            _logger = logger;
        }
        /// <summary></summary>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var logMetadata = BuildRequestMetadata(request);
            var response = await base.SendAsync(request, cancellationToken);
            logMetadata = BuildResponseMetadata(logMetadata, response);
            await SendToLog(logMetadata);
            return response;
        }
        /// <summary></summary>
        private LogMetadata BuildRequestMetadata(HttpRequestMessage request)
        {
            LogMetadata log = new LogMetadata
            {
                RequestMethod = request.Method.Method,
                RequestTimestamp = DateTime.UtcNow,
                RequestUri = request.RequestUri.ToString()
            };
            if (request.Content != null) log.RequestContent = request.Content.ReadAsStringAsync().Result;
            return log;
        }
        /// <summary></summary>
        private LogMetadata BuildResponseMetadata(LogMetadata logMetadata, HttpResponseMessage response)
        {
            logMetadata.ResponseStatusCode = response.StatusCode;
            logMetadata.ResponseTimestamp = DateTime.UtcNow;
            logMetadata.ResponseContentType = response.Content.Headers.ContentType.MediaType;
            if (response.Content != null) logMetadata.ResponseContent = response.Content.ReadAsStringAsync().Result;
            return logMetadata;
        }
        /// <summary>
        /// Sends to log.
        /// </summary>
        /// <param name="logMetadata">The log metadata.</param>
        /// <returns></returns>
        private async Task<bool> SendToLog(LogMetadata logMetadata)
        {
            if ((AppSettings.Instance.TraceType == TraceType.NoSwaggerDelegationHandler) && (logMetadata.RequestUri.ToLower().Contains("swagger"))) return true;
            _logger.Trace(logMetadata.AsString());
            return true;
        }
    }
    /// <summary></summary>
    public class LogMetadata
    {
        /// <summary></summary>
        public string RequestContentType { get; set; }
        /// <summary></summary>
        public string RequestContent { get; set; } = "";
        /// <summary></summary>
        public string RequestUri { get; set; }
        /// <summary></summary>
        public string RequestMethod { get; set; }
        /// <summary></summary>
        public DateTime? RequestTimestamp { get; set; }
        /// <summary></summary>
        public string ResponseContentType { get; set; }
        /// <summary></summary>
        public string ResponseContent { get; set; } = "";
        /// <summary></summary>
        public HttpStatusCode ResponseStatusCode { get; set; }
        /// <summary></summary>
        public DateTime? ResponseTimestamp { get; set; }
        /// <summary></summary>
        public string AsString()
        {
            try
            {
                var sb = new System.Text.StringBuilder();
                sb.Append(RequestUri);
                sb.Append(" ");
                sb.Append(RequestMethod);
                if (RequestContentType != null)
                {
                    sb.Append(" ");
                    sb.Append(RequestContentType);
                }
                sb.Append(" Took ");
                sb.Append(RequestTimestamp.Value.Subtract(ResponseTimestamp.Value).Seconds.ToString());
                sb.Append("s ");
                sb.Append("  Returned " + this.ResponseStatusCode.ToString() + " " + ((int)this.ResponseStatusCode).ToString());
                if (this.RequestContent.Length > 0)
                {
                    sb.Append(Environment.NewLine);
                    sb.Append(RequestContent);
                }
                if (this.ResponseContent.Length > 0)
                {
                    sb.Append(Environment.NewLine);
                    sb.Append("RETURN PAYLOAD: ");
                    sb.Append(Environment.NewLine);
                    sb.Append(ResponseContent);
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return string.Format("Error While Rendering LogMetadata {0}", ex.Message);
            }
        }
    }
}