using System.Threading.Tasks;
using Acr.UserDialogs;
using Akavache;
using AutoMapper;
using FreshMvvm;
using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Proxies;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Services;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;

namespace Phoenix.Mobile
{
    public static class Bootstrap
    {
        public static void Init()
        {
            //DbFactory.Init();                    
            BlobCache.ApplicationName = "PhoenixAppCache";
            Phoenix.Framework.Register.RegisterKey("CFD348109A22B86A8F991BCF");
            AutoMapperConfig();
            RegisterDependencies();
            //RegisMock();
        }
        public static async Task InitAsync()
        {
            await Task.Run(() =>
            {
                //DbFactory.Init();                    
                BlobCache.ApplicationName = "PhoenixAppCache";
                Phoenix.Framework.Register.RegisterKey("CFD348109A22B86A8F991BCF");
                AutoMapperConfig();
                RegisterDependencies();
                //RegisMock();
            });
        }

        private static void AutoMapperConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ExternalMapperProfile>();
                cfg.AddProfile<InternalMapperProfile>();
            });
            FreshIOC.Container.Register<IMapper>(new Mapper(configuration));
        }
        //private static void RegisMock()
        //{
        //    FreshIOC.Container.Register<IAccountService, AccountService>();
        //}
        private static void RegisterDependencies()
        {
            //service
            FreshIOC.Container.Register<IDialogService, DialogService>();
            FreshIOC.Container.Register<IExceptionHandler, ExceptionHandler>();
            FreshIOC.Container.Register<IUserDialogs>(UserDialogs.Instance);
            FreshIOC.Container.Register<IWorkContext, WorkContext>();
            FreshIOC.Container.Register<ICameraService, CameraService>();
            FreshIOC.Container.Register<IAuthService, AuthService>();
            FreshIOC.Container.Register<IUserService, UserService>();
            FreshIOC.Container.Register<IFileService, FileService>();
            FreshIOC.Container.Register<IVendorService, VendorService>();


            //proxy
            FreshIOC.Container.Register<IAuthProxy, AuthProxy>();
            FreshIOC.Container.Register<IUserProxy, UserProxy>();
            FreshIOC.Container.Register<IFileProxy, FileProxy>();
            FreshIOC.Container.Register<IVendorProxy, VendorProxy>();
        }
    }
}
