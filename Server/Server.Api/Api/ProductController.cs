using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Product;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/product")]
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Route("GetAllAppProducts")]
        public async Task<BaseResponse<ProductDto>> GetAllAppProducts(ProductRequest request)
        {
            return await _productService.GetAllAppProducts(request);
        }

        [HttpPost]
        [Route("GetProductMenus")]
        //public async Task<BaseResponse<CartItemDto>> GetAllCartItems(CartItemRequest request)
        public async Task<BaseResponse<ProductMenuDto>> GetProductMenus(ProductMenuRequest request)
        {
            return await _productService.GetProductMenus(request);
        }
    }
}