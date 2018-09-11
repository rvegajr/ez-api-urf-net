using System.Web.Http;
using Microsoft.AspNet.OData.Extensions;
using ODataV4 = Microsoft.AspNet.OData;

/*
Many thanks to the following websites for allowing me to power through my WebApi OData Issues
https://genericunitofworkandrepositories.codeplex.com/  <== Awesome!  doesn't get any better than this :)
https://blog.longle.net/2013/10/09/upgrading-to-async-with-entity-framework-mvc-odata-asyncentitysetcontroller-kendo-ui-glimpse-generic-unit-of-work-repository-framework-v2-0/
https://blogs.msdn.microsoft.com/davidhardin/2014/12/17/web-api-odata-v4-lessons-learned/
*/
namespace Ez.Web
{
    public static class ODataConfig
    {
        public static void Register(HttpConfiguration config)
        {

            ODataV4.Builder.ODataModelBuilder builder = new ODataV4.Builder.ODataConventionModelBuilder();
            ODataRegistrations.Register(builder);

            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);

            ODataV4.Extensions.HttpConfigurationExtensions.MapODataServiceRoute(
                    configuration: config,
                    routeName: "ODataRoute",
                    routePrefix: "odata",
                    model: builder.GetEdmModel());

            //config.EnableQuerySupport();
        }
        
    }
}