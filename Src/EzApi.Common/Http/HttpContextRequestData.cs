using Ez.Common.Extentions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
namespace Ez.Common.Http
{
    /// <summary>Thread safe ObjectDictionary</summary>
    public class ObjectDictionary : Dictionary<string, object>, System.Collections.IDictionary
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public static class RequestVarHttpMessageHandlerExtensions
    {
        public static class HttpPropertyKey
        {
            public static readonly string REQUEST_VARS_KEY = "REQUEST_VARS";
        }
        public static ObjectDictionary GetRequestVariables(this HttpRequestMessage request)
        {
            if (request.Properties.TryGetValue(HttpPropertyKey.REQUEST_VARS_KEY, out var result))
            {
                return result as ObjectDictionary;
            }
            else return null;
        }

        public static void SetRequestVariables(this HttpRequestMessage request, ObjectDictionary ctx)
        {
            request.Properties[HttpPropertyKey.REQUEST_VARS_KEY] = ctx;
        }
    }

    public class RequestVarMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.SetRequestVariables(new ObjectDictionary() { });
            return base.SendAsync(request, cancellationToken);
        }
    }

    /// <summary>
    /// Many thanks to https://stackoverflow.com/questions/44736889/how-to-setup-a-per-request-global-variable-for-nlog?rq=1
    /// </summary>
    public class HttpContextRequestData
    {
        private static HttpContextRequestData instance;

        private HttpContextRequestData()
        {
            try
            {
                ///Will grab the intance that pertains to the request message rather than the instantiated one
                if ((HttpContext.Current != null) && (HttpContext.Current.Items["MS_HttpRequestMessage"] != null))
                {
                    this.httpContext = (HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage).GetRequestVariables() ?? httpContext;
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Return a new instance because we need it to be completely new to grab the Request Data Context,  or grab the singleton instance if we are using Owin
        /// </summary>
        public static HttpContextRequestData Instance
        {
            get
            {
                //
                if ((HttpContext.Current != null) && (HttpContext.Current.Items["MS_HttpRequestMessage"] != null))
                {
                    return new HttpContextRequestData();
                }
                else
                {
                    if (instance == null)
                    {
                        instance = new HttpContextRequestData();
                    }

                    return instance;
                }
            }
        }

        private System.Collections.IDictionary httpContext = new ObjectDictionary();
        /// <summary>
        /// Gets or sets the request unique identifier.
        /// </summary>
        /// <value>
        /// The request unique identifier.
        /// </value>
        public string RequestGuid { get => GetVar("RequestGuid"); set => SetVar("RequestGuid", value); }

        /// <summary>
        /// Gets or sets the request initiated.
        /// </summary>
        /// <value>
        /// The request initiated.
        /// </value>
        public DateTime RequestInitiated
        {
            get
            {
                if (httpContext["RequestInitiated"] == null)
                    return DateTime.Now;
                else
                    return Convert.ToDateTime(httpContext["RequestInitiated"]);
            }
            set
            {
                if ((HttpContext.Current != null) && ((HttpContext.Current.Items != null))) HttpContext.Current.Items["RequestInitiated"] = value;
                httpContext["RequestInitiated"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the url for this request.  The idea is that this will work with legacy asp.net as well as OWIN calls.  If there is no value  it wi
        /// </summary>
        public string ThisBaseUrl
        {
            get
            {
                if (httpContext["ThisBaseUrl"] == null)
                    if ((HttpContext.Current != null) && (HttpContext.Current.Items != null) && (HttpContext.Current.Items.Count > 0) && (HttpContext.Current.Request.Url != null))
                        httpContext["ThisBaseUrl"] = HttpContext.Current.Request.Url.ToString().BaseUrlNoScheme().ToLower();
                if (httpContext["ThisBaseUrl"] == null)
                    return System.Net.Dns.GetHostName().ToLower();
                else
                    return (string)httpContext["ThisBaseUrl"];
            }
            set
            {
                if ((HttpContext.Current != null) && ((HttpContext.Current.Items != null))) HttpContext.Current.Items["ThisBaseUrl"] = value;
                httpContext["ThisBaseUrl"] = value;
            }
        }


        /// <summary>
        /// Gets or sets the project id for this request
        /// </summary>
        public string ProjectId { get => GetVar("ProjectId"); set => SetVar("ProjectId", value); }

        private string GetVar(string VariableName)
        {
            if ((HttpContext.Current != null) && (HttpContext.Current.Items != null) && (HttpContext.Current.Items[VariableName] != null))
                return HttpContext.Current.Items[VariableName].ToString();
            else if ((httpContext[VariableName] != null) && (!string.IsNullOrEmpty(httpContext[VariableName].ToString())))
                return (string)httpContext[VariableName];
            else
                return "";
        }
        private void SetVar(string VariableName, string Value)
        {
            if ((HttpContext.Current != null) && ((HttpContext.Current.Items != null))) HttpContext.Current.Items[VariableName] = Value;
            httpContext[VariableName] = Value;
        }

        /// <summary>
        /// Gets or sets the scope id for this request.  this is useful for such fields as ProjectEstimateId, employee id, etc
        /// </summary>
        public string ScopeId { get => GetVar("ScopeId"); set => SetVar("ScopeId", value); }

        /// <summary>
        /// The custom payload maximum read count
        /// </summary>
        public static int CustomPayloadMaxReadCount = 1;
        /// <summary>
        /// Gets or sets the a custom payload for this request. NOTE! This is read "CustomPayloadMaxReadCount" times because we may not need to serailize this 
        /// more than once.  
        /// </summary>
        public string CustomPayload
        {
            get
            {

                if ((httpContext["CustomPayload"] == null) || (string.IsNullOrWhiteSpace(httpContext["CustomPayload"].ToString())))
                    return "";
                else
                {
                    if (httpContext["CustomPayloadReadCount"] == null) httpContext["CustomPayloadReadCount"] = "0";
                    int ReadCount = 0;
                    int.TryParse(httpContext["CustomPayloadReadCount"].ToString(), out ReadCount);
                    ReadCount++;
                    if (ReadCount > CustomPayloadMaxReadCount)
                    {
                        httpContext["CustomPayloadReadCount"] = "0";
                        httpContext["CustomPayload"] = "";
                        return "";
                    }
                    else
                    {
                        httpContext["CustomPayloadReadCount"] = ReadCount.ToString();
                    }
                    var PayloadAsString = httpContext["CustomPayload"].ToString();
                    return PayloadAsString;
                }
            }
            set
            {
                if ((HttpContext.Current != null) && ((HttpContext.Current.Items != null))) HttpContext.Current.Items["CustomPayload"] = value;
                if ((HttpContext.Current != null) && ((HttpContext.Current.Items != null))) HttpContext.Current.Items["CustomPayloadReadCount"] = 0;
                httpContext["CustomPayload"] = value;
                httpContext["CustomPayloadReadCount"] = "0";
            }
        }
    }

}
