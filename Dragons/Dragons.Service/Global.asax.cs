using Dragons.Service.Core;
using System.Web.Http;
using System.Web.Routing;
using Newtonsoft.Json.Serialization;

namespace Dragons.Service
{
    /// <summary>
    /// Base class for the web application.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Global static game service used by controllers.
        /// </summary>
        public static IGameService GameService = new GameService();

        /// <summary>
        /// Application start up event.
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GameService.InitializeAsync(Server.MapPath("~/App_Data/InitialSetups")).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
