using Phoenix.Mobile.Core.Models.Product;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Common;
using Phoenix.Shared.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetAllAppProducts(ProductRequest request);
        Task<List<ProductMenuModel>> GetProductMenus(ProductMenuRequest request);
    }

    public class ProductService : IProductService
    {
        private readonly IProductProxy _productProxy;
        public ProductService(IProductProxy productProxy)
        {
            _productProxy = productProxy;
        }
        public async Task<List<ProductModel>> GetAllAppProducts(ProductRequest request)
        {
            var product = await _productProxy.GetAllAppProducts(request);
            return product.Data.MapTo<ProductModel>();
        }

        public async Task<List<ProductMenuModel>> GetProductMenus(ProductMenuRequest request)
        {
            var productMenu = await _productProxy.GetProductMenus(request);
            return productMenu.Data.MapTo<ProductMenuModel>();
        }
    }
}
