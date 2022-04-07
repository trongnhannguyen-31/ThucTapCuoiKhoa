using Phoenix.Mobile.Core.Models.ProductType;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Common;
using Phoenix.Shared.ProductType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IProductTypeService
    {
        Task<List<ProductTypeModel>> GetAllProductTypes(ProductTypeRequest request);
    }

    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeProxy _productTypeProxy;
        public ProductTypeService(IProductTypeProxy productTypeProxy)
        {
            _productTypeProxy = productTypeProxy;
        }
        public async Task<List<ProductTypeModel>> GetAllProductTypes(ProductTypeRequest request)
        {
            var vendor = await _productTypeProxy.GetAllProductTypes(request);
            return vendor.Data.MapTo<ProductTypeModel>();
        }
    }
}
