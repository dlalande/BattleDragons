using Dragons.Core;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace Dragons.Service
{
    /// <summary>
    /// Configures the web api framework
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the web api setup.
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Create a message handler chain with an end-point.
            var routeHandlers = HttpClientFactory.CreatePipeline(new HttpControllerDispatcher(config),
                new DelegatingHandler[] { new ApiKeyDelegatingHandler(Constants.ApiKey) });

            // Web API routes
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "dragons/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: routeHandlers
            );
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }
    }
}
