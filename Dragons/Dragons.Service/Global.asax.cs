using Dragons.Service.Core;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Dragons.Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static IGameService GameService = new GameService();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GameService.InitializeAsync(Server.MapPath("~/App_Data/InitialSetups")).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
