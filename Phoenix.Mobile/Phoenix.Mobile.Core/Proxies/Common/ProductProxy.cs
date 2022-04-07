using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.Product;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IProductProxy
    {
        Task<BaseResponse<ProductDto>> GetAllAppProducts(ProductRequest request);
        Task<BaseResponse<ProductMenuDto>> GetProductMenus(ProductMenuRequest request);
    }

    public class ProductProxy : BaseProxy, IProductProxy
    {
        public async Task<BaseResponse<ProductDto>> GetAllAppProducts(ProductRequest request)
        {
            try
            {
                var api = RestService.For<IProductApi>(GetHttpClient());
                //List<VendorDto> result = await api.GetAllVendors(request);
                //if (result == null) return new List<VendorDto>();
                return await api.GetAllAppProducts(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public async Task<BaseResponse<ProductMenuDto>> GetProductMenus(ProductMenuRequest request)
        {
            try
            {
                var api = RestService.For<IProductApi>(GetHttpClient());
                return await api.GetProductMenus(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public interface IProductApi
        {
            [Post("/product/GetAllAppProducts")]
            Task<BaseResponse<ProductDto>> GetAllAppProducts([Body] ProductRequest request);

            [Post("/product/GetProductMenus")]
            Task<BaseResponse<ProductMenuDto>> GetProductMenus([Body] ProductMenuRequest request);
        }
    }
}