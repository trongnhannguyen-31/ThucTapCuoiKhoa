using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.ProductType;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IProductTypeProxy
    {
        Task<BaseResponse<ProductTypeDto>> GetAllProductTypes(ProductTypeRequest request);
    }

    public class ProductTypeProxy : BaseProxy, IProductTypeProxy
    {
        public async Task<BaseResponse<ProductTypeDto>> GetAllProductTypes(ProductTypeRequest request)
        {
            try
            {
                var api = RestService.For<IProductTypeApi>(GetHttpClient());
                //List<VendorDto> result = await api.GetAllVendors(request);
                //if (result == null) return new List<VendorDto>();
                return await api.GetAllProductTypes(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }
        public interface IProductTypeApi
        {
            [Post("/productType/GetAllProductTypes")]
            Task<BaseResponse<ProductTypeDto>> GetAllProductTypes([Body] ProductTypeRequest request);

        }
    }
}