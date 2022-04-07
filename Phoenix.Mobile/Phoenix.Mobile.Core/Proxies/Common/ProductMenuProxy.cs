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
    public interface IProductMenuProxy
    {
        Task<BaseResponse<ProductMenuDto>> GetAllProductMenus(ProductMenuRequest request);
        Task<BaseResponse<ProductMenuDto>> GetProductById(ProductMenuRequest request);
    }

    public class ProductMenuProxy : BaseProxy, IProductMenuProxy
    {
        public async Task<BaseResponse<ProductMenuDto>> GetAllProductMenus(ProductMenuRequest request)
        {
            try
            {
                var api = RestService.For<IProductMenuApi>(GetHttpClient());
                //List<VendorDto> result = await api.GetAllVendors(request);
                //if (result == null) return new List<VendorDto>();
                return await api.GetAllProductMenus(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }
        public interface IProductMenuApi
        {
            [Post("/productMenu/GetAllProductMenus")]
            Task<BaseResponse<ProductMenuDto>> GetAllProductMenus([Body] ProductMenuRequest request);

        }

        public async Task<BaseResponse<ProductMenuDto>> GetProductById(ProductMenuRequest request)
        {
            try
            {
                var api = RestService.For<IGetProductByIdApi>(GetHttpClient());
                //List<VendorDto> result = await api.GetAllVendors(request);
                //if (result == null) return new List<VendorDto>();
                return await api.GetProductById(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }
        public interface IGetProductByIdApi
        {
            [Get("/productMenu/GetProductById")]
            Task<BaseResponse<ProductMenuDto>> GetProductById([Body] ProductMenuRequest request);

        }
    }
}