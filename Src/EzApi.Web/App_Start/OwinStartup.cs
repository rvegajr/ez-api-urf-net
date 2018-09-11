using Microsoft.Owin;
using Owin;
using Ez.Common.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Ez.Common.Extentions;

[assembly: OwinStartup(typeof(Ez.Web.Owin.Startup))]

namespace Ez.Web.Owin
{
    /// <summary></summary>
    public class Startup
    {
        /// <summary></summary>
        public static void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.OwinRegister(config);
            app.Use(typeof(SimpleMiddleWare));
            app.UseWebApi(config);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.Owin.OwinMiddleware" />
    public class SimpleMiddleWare : OwinMiddleware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleMiddleWare"/> class.
        /// </summary>
        /// <param name="next"></param>
        public SimpleMiddleWare(OwinMiddleware next) : base(next)
        {
        }
        /// <summary>
        /// Process an individual request.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Invoke(IOwinContext context)
        {
            HttpContextRequestData.Instance.ThisBaseUrl = context.Request.Uri.ToString().BaseUrlNoScheme().ToLower();
        }
    }
}