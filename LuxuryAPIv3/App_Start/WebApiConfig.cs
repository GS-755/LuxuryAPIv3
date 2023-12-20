using System.Web.Http;
using System.Web.Http.Cors;

namespace LuxuryAPIv3
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // CORS Policy settings
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.EnableCors(cors);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
