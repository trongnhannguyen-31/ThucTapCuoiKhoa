using Phoenix.Mobile.Core.Models.ProductSKU;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Common;
using Phoenix.Shared.ProductSKU;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IProductSKUService
    {
        Task<List<ProductSKUModel>> GetAllProductSKUs(ProductSKURequest request);
        //Task<List<ProductSKUModel>> GetProductById(ProductSKURequest request);
        Task<ProductSKUModel> GetProductById(ProductSKURequest request);
    }

    public class ProductSKUService : IProductSKUService
    {
        private readonly IProductSKUProxy _productSKUProxy;
        public ProductSKUService(IProductSKUProxy productSKUProxy)
        {
            _productSKUProxy = productSKUProxy;
        }
        public async Task<List<ProductSKUModel>> GetAllProductSKUs(ProductSKURequest request)
        {
            var productSKU = await _productSKUProxy.GetAllProductSKUs(request);
            return productSKU.Data.MapTo<ProductSKUModel>();
        }

        //public async Task<List<ProductSKUModel>> GetProductById(ProductSKURequest request)
        //{
        //    var productSKU = await _productSKUProxy.GetAllProductSKUs(request);
        //    return productSKU.Data.MapTo<ProductSKUModel>();
        //}

        public async Task<ProductSKUModel> GetProductById(ProductSKURequest request)
        {
            var productSKU = await _productSKUProxy.GetProductById(request);
            return productSKU.Record.MapTo<ProductSKUModel>();
        }
    }
}
