using Ez.Common;
using Ez.Web.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Tracing;

namespace Ez.Web
{
    public static class WebApiConfig
    {
        /*
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
        */

        /// <summary></summary>
        /// <param name="config"></param>
        public static void OwinRegister(HttpConfiguration config)
        {
            GlobalConfiguration.Configuration.EnsureInitialized();
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            config.DependencyResolver = new CustomUnityDependencyResolver(UnityOwinHelpers.GetConfiguredContainer());
            Register(config, true);
            ODataConfig.Register(config);
        }

        /// <summary></summary>
        public static void Register(HttpConfiguration config)
        {
            Register(config, false);
        }
        /// <summary></summary>
        public static void Register(HttpConfiguration config, bool isOwin)
        {
            //var s = UnityConfig.DumpUnityContainer(config);
            if (AppSettings.Instance.TraceType == TraceType.TraceWriter)
            {
                var traceWriter = config.DependencyResolver.GetService(typeof(ITraceWriter));
                config.Services.Replace(typeof(ITraceWriter), traceWriter);
                //config.Services.Replace(typeof(ITraceWriter), new NLogTraceWriter());
            }
            else if ((AppSettings.Instance.TraceType == TraceType.DelegationHandler) || ((AppSettings.Instance.TraceType == TraceType.NoSwaggerDelegationHandler)))
            {
                throw new System.Exception("DelegatingHandler cannot be handled.... yet");
                //var customLogHandler = (DelegatingHandler)config.DependencyResolver.GetService(typeof(ICustomLogHander));
                //config.MessageHandlers.Add(customLogHandler);
            }
            
            var exceptionLogger = config.DependencyResolver.GetService(typeof(IExceptionLogger));
            config.Services.Replace(typeof(IExceptionLogger), exceptionLogger);
            var exceptionHandler = config.DependencyResolver.GetService(typeof(IExceptionHandler));
            config.Services.Replace(typeof(IExceptionHandler), exceptionHandler);
            
            // Web API configuration and services
            var serializer = config.Formatters.JsonFormatter.CreateJsonSerializer();
            serializer.Converters.Add(new StringEnumConverter());

            config.Formatters.JsonFormatter.SerializerSettings =
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                };

            config.Formatters.Clear();
            config.Formatters.Insert(0, new JsonMediaTypeFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "webapi/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );
        }
    }
}
