using Ez.Common.Extentions;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Ez.Web.Infrastructure
{
    /// <summary></summary>
    public class GlobalExceptionHandler : ExceptionHandler, IExceptionHandler
    {
        /// <summary> </summary>
        /// <param name="context"></param>
        public override void Handle(ExceptionHandlerContext context)
        {
            if ((context != null) && (context.Exception != null))
            {
                if (context.Exception.GetType().Name.Equals("DbUpdateException"))
                {
                    string genericErrorMessage = context.Exception.GetExceptionMessages();
                    HttpResponseMessage resp;
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent(genericErrorMessage),
                        ReasonPhrase = genericErrorMessage
                    };
                    context.Result = new ErrorMessageResult(context.Request, resp);
                }
            }
        }

        /// <summary>Use to intercept and handle exceptions that were unexpected.  Odata renders a Status code 500,  this will format the message and make it a 400</summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            if ((context != null) && (context.Exception != null))
            {
                if (context.Exception.GetType().Name.Equals("DbUpdateException"))
                {
                    string genericErrorMessage = context.Exception.GetExceptionMessages().Replace("\n", " ").Replace("\r", " ");
                    HttpResponseMessage resp;
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent(genericErrorMessage)
                    };
                    context.Result = new ErrorMessageResult(context.Request, resp);
                }
            }
        }

        /// <summary></summary>
        public class ErrorMessageResult : IHttpActionResult
        {
            private HttpRequestMessage _request;
            private HttpResponseMessage _httpResponseMessage;

            /// <summary></summary>
            /// <param name="request"></param>
            /// <param name="httpResponseMessage"></param>
            public ErrorMessageResult(HttpRequestMessage request, HttpResponseMessage httpResponseMessage)
            {
                _request = request;
                _httpResponseMessage = httpResponseMessage;
            }

            /// <summary></summary>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(_httpResponseMessage);
            }
        }
    }
}