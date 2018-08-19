using System.Web.Http;

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
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "dragons/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
