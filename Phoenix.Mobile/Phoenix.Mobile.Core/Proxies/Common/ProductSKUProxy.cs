using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.ProductSKU;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IProductSKUProxy
    {
        Task<BaseResponse<ProductSKUDto>> GetAllProductSKUs(ProductSKURequest request);
        Task<BaseResponse<ProductSKUAppDto>> GetProductById(ProductSKURequest request);
        Task<CrudResult> UpdateProductSKUApp(int Id, ProductSKURequest request);
    }

    public class ProductSKUProxy : BaseProxy, IProductSKUProxy
    {
        public async Task<BaseResponse<ProductSKUDto>> GetAllProductSKUs(ProductSKURequest request)
        {
            try
            {
                var api = RestService.For<IProductSKUApi>(GetHttpClient());
                //List<VendorDto> result = await api.GetAllVendors(request);
                //if (result == null) return new List<VendorDto>();
                return await api.GetAllProductSKUs(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public async Task<BaseResponse<ProductSKUAppDto>> GetProductById(ProductSKURequest request)
        {
            try
            {
                var api = RestService.For<IProductSKUApi>(GetHttpClient());
                return await api.GetProductById(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public async Task<CrudResult> UpdateProductSKUApp(int Id, ProductSKURequest request)
        {
            try
            {
                var api = RestService.For<IProductSKUApi>(GetHttpClient());
                var result = await api.UpdateProductSKUApp(Id, request);
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CrudResult();
            }
        }

        public interface IProductSKUApi
        {
            [Post("/productSKU/GetAllProductSKUs")]
            Task<BaseResponse<ProductSKUDto>> GetAllProductSKUs([Body] ProductSKURequest request);

            [Get("/productSKU/GetProductById")]
            Task<BaseResponse<ProductSKUAppDto>> GetProductById([Body] ProductSKURequest request);

            [Post("/productSKU/UpdateProductSKUApp")]
            Task<CrudResult> UpdateProductSKUApp(int Id, [Body] ProductSKURequest request);
        }
    }
}