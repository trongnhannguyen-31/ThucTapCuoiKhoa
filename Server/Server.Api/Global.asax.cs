using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Falcon.Web.Core.Infrastructure;
using Falcon.Web.Core.Log;
using FluentScheduler;
using Newtonsoft.Json;
using Phoenix.Server.Api.Infrastructure;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using Phoenix.Server.Services.Infrastructure;
using Phoenix.Server.Services.MainServices.Tasks;

namespace Phoenix.Server.Api
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            //Web api
            //AutoMapper
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperApiProfile>();
            });

            //IoC for web api
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            DependencyReg.Register(container);
            DependencyRegistra.ApiServerRegister(container);
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            // MVC controllers
            //WebRegistration.Register(container);
            //container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            //container.Verify();
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
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //setup schedule tasks
            JobManager.Initialize(new TaskRegistry());
            JobManager.JobException += (info) =>
            {
                using (AsyncScopedLifestyle.BeginScope(SimpleContainer.Container))
                {
                    var logger = EngineContext.Current.Resolve<ILogger>();
                    logger.Error("Task error " + info.Name, info.Exception.ToString());
                }
            };
            //finish
            using (AsyncScopedLifestyle.BeginScope(container))
            {
                var logger = EngineContext.Current.Resolve<ILogger>();
                logger.Info("Phoenix Mobile App Server Started!");
            }

            //SetupCache(container);
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

        #region methods

        //private async void SetupCache(Container container)
        //{
        //    //setup cache product skus 
        //    using (AsyncScopedLifestyle.BeginScope(container))
        //    {
        //        //var productSer = EngineContext.Current.Resolve<ProductService>();
        //        //productSer.ClearCache(CacheKeys.SkuKey);
        //        //await productSer.GetAll(CacheKeys.SkuKey);
        //    }
        //}

        #endregion
    }
}