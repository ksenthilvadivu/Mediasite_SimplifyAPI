using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace DH.Curbside.API
{
    /// <summary>
    /// Handles routing configurations
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// creates routing configuration
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
                       
        }
    }
}