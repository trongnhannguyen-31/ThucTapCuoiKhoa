using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using AutoMapper;
using Falcon.Web.Core.Infrastructure;
using Falcon.Web.Core.Log;
using Newtonsoft.Json;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using Phoenix.Server.Web.Infrastructure;
using System.Threading;
using Phoenix.Server.Services.Infrastructure;

namespace Phoenix.Server.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            //HttpContext.Current.Response.Cache.SetNoStore();
            //resource
            DefaultModelBinder.ResourceClassKey = "PhoenixResource";
            //Web api
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            //AutoMapper
            Mapper.Initialize(cfg => {
                cfg.AddProfile<AutoMapperWebProfile>();
                cfg.AddProfile<AutoMapperExtendWebProfile>();
            });
            //IoC for web api
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            DependencyRegistra.Register(container);
            // MVC controllers
            WebRegistration.Register(container);
            container.Verify();
            //EngineContext.Current.Resolve<SchedulerTaskDOB>().StartAsync(CancellationToken.None);
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
            //Fix date time serialize
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings =
                new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local,
                };
            //setup MVC
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
			//finish
			using (AsyncScopedLifestyle.BeginScope(container))
            {
                var logger = EngineContext.Current.Resolve<ILogger>();
                logger.Info("MasterAdmin Started!");
            }
            

        }
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            //log error
            LogException(exception);
        }
        protected void LogException(Exception exc)
        {
            if (exc == null)
                return;
            //ignore 404 HTTP errors
            var httpException = exc as HttpException;
            if (httpException != null && httpException.GetHttpCode() == 404)
                return;
            try
            {
                //log
                var logger = EngineContext.Current.Resolve<ILogger>();
                logger.Error(exc.Message, exc.ToString());
            }
            catch (Exception)
            {
                //don't throw new exception if occurs
            }
        }
    }
}