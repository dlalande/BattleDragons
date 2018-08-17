using System.Web.Mvc;

namespace Dragons.Service.Controllers
{    
    /// <summary>
    /// Controller to handle default action on root.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Default action for controller.  Redirects to swagger endpoint.
        /// </summary>
        /// <returns>Redirect to swagger endpoint.</returns>
        public ActionResult Index()
        {
            return new RedirectResult("/swagger", true);
        }
    }
}
