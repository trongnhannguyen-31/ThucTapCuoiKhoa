using Falcon.Web.Core.Caching;
using Falcon.Web.Core.Infrastructure;
using Falcon.Web.Core.Log;
using Falcon.Web.Core.Security;
using Falcon.Web.Core.Settings;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.Framework;
using SimpleInjector;
using SettingService = Phoenix.Server.Services.Framework.SettingService;
using Falcon.Services.Users;
using Falcon.Core.Data;
using Falcon.Core.Domain.Users;
using Phoenix.Server.Services.MainServices.Users;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.ApiServices;

namespace Phoenix.Server.Services.Infrastructure
{
    public static class DependencyRegistra
    {
        public static void Register(Container container)
        {
            //DB
            container.Register<DataContext>(Lifestyle.Scoped);
            //Framework
            container.Register<ILogger, Logger>(Lifestyle.Scoped);
            container.Register<ITokenValidation, TokenValidation>(Lifestyle.Scoped);
            container.Register<ICacheManager, MemoryCacheManager>(Lifestyle.Scoped);
            container.Register<IEncryptionService, EncryptionService>(Lifestyle.Scoped);
            container.Register<ISettingService, SettingService>(Lifestyle.Scoped);
            //Register service in dll
            container.Register<IUserService, UserService>(Lifestyle.Scoped);
            container.Register<UserAuthService>(Lifestyle.Scoped);
            //to use in web admin
            container.Register<SettingService>(Lifestyle.Scoped);
            container.Register<IVendorService, VendorService>(Lifestyle.Scoped);
            container.Register<IProductTypeService, ProductTypeService>(Lifestyle.Scoped);
            container.Register<IProductService, ProductService>(Lifestyle.Scoped);
            container.Register<IOrderService, OrderService>(Lifestyle.Scoped);
            container.Register<IOrderDetailService, OrderDetailService>(Lifestyle.Scoped);
            container.Register<IProductSKUService, ProductSKUService>(Lifestyle.Scoped);
            container.Register<IRatingService, RatingService>(Lifestyle.Scoped);
            container.Register<IWarehouseService, WarehouseService>(Lifestyle.Scoped);
            container.Register<ICustomerService, CustomerService>(Lifestyle.Scoped);
            container.Register<IDropdownService, DropdownService>(Lifestyle.Scoped);
            container.Register<IWarehouseMenuService, WarehouseMenuService>(Lifestyle.Scoped);


            EngineContext.Current.Init(new SimpleContainer(container));
        }
        public static void ApiServerRegister(Container container)
        {
            Register(container);
            
        }
    }
}