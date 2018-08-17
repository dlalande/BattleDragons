using Dragons.Service.Core;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json.Serialization;

namespace Dragons.Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static IGameService GameService = new GameService();

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GameService.InitializeAsync(Server.MapPath("~/App_Data/InitialSetups")).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
