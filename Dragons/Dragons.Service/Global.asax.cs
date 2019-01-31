using System;
using System.Web;
using Dragons.Service.Core;
using System.Web.Http;
using System.Web.Routing;
using Newtonsoft.Json.Serialization;
using NLog;

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

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static Exception _startException;
        /// <summary>
        /// Application start up event.
        /// </summary>
        protected void Application_Start()
        {
            Logger.Info("Dragons.Service starting...");
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            try
            {
                GameService.InitializeAsync(Server.MapPath("~/App_Data/InitialSetups")).ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch(Exception e)
            {
                _startException = e;
            }
        }

        /// <summary>
        /// Application begin request event.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (_startException != null)
                this.Context.AddError(_startException);
        }

        /// <summary>
        /// Application request completed event.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        protected void Application_RequestCompleted(object sender, EventArgs e)
        {
            if (_startException != null)
                HttpRuntime.UnloadAppDomain();
        }
    }
}
