using Microsoft.Web.Http.Routing;
using System.Web.Http;
using System.Web.Http.Routing;
using DH.Curbside.API.Infrastructure.Filters;
using DH.Curbside.API.Infrastructure.Handlers;

namespace DH.Curbside.API
{
    /// <summary>
    /// Handles WebAPI routing
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Initializes WebAPI routing
        /// </summary>
        /// <param name="config">HttpConfiguration object</param>
        public static void Register(HttpConfiguration config)
        {         

            config.AddApiVersioning();

            var constraintResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap =
                {
                    ["apiVersion"] = typeof( ApiVersionRouteConstraint )
                }
            };
            config.MapHttpAttributeRoutes(constraintResolver);

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            GlobalConfiguration.Configuration.MessageHandlers.Add(new LoggingHandler());
            config.Filters.Add(new ValidationActionFilter());
            config.Filters.Add(new HandleApiExceptionFilter());

            config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
            );

            // Enable cors to allow all origins , methods(Get,post,Put,Delete )and Headers(content-type)
            //EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:54469/", "*", "*");
            //config.EnableCors(cors);
            // To disable tracing, please comment out or remove the following line of code
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
