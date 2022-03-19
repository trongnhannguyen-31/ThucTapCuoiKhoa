using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Phoenix.Server.Api.Infrastructure;
using Falcon.Web.Api.ExceptionHandle;
using Falcon.Web.Api.Security;

namespace Phoenix.Server.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //// Web API configuration and services
            //config.MessageHandlers.Add(new WebApiAuthHandler());
            config.MessageHandlers.Add(new AuthMessageHandler());
            ////global exception handler
            config.Services.Replace(typeof(IExceptionHandler), new WebApiExceptionHandler());

            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
