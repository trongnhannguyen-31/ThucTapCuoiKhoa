using Phoenix.Mobile.Core.Models.Product;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Common;
using Phoenix.Shared.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IProductMenuService
    {
        Task<List<ProductMenuModel>> GetAllProductMenus(ProductMenuRequest request);
        //Task<List<ProductMenuModel>> GetProductById(ProductMenuRequest request);
        Task<ProductMenuModel> GetProductById(ProductMenuRequest request);
        
    }

    public class ProductMenuService : IProductMenuService
    {
        private readonly IProductMenuProxy _productMenuProxy;
        public ProductMenuService(IProductMenuProxy productMenuProxy)
        {
            _productMenuProxy = productMenuProxy;
        }
        public async Task<List<ProductMenuModel>> GetAllProductMenus(ProductMenuRequest request)
        {
            var productMenu = await _productMenuProxy.GetAllProductMenus(request);
            return productMenu.Data.MapTo<ProductMenuModel>();
        }

        //public async Task<List<ProductMenuModel>> GetProductById(ProductMenuRequest request)
        //{
        //    var productMenu = await _productMenuProxy.GetAllProductMenus(request);
        //    return productMenu.Data.MapTo<ProductMenuModel>();
        //}

        public async Task<ProductMenuModel> GetProductById(ProductMenuRequest request)
        {
            var productMenu = await _productMenuProxy.GetProductById(request);
            return productMenu.Record.MapTo<ProductMenuModel>();
        }
    }
}
