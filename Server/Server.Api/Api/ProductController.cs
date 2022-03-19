using Phoenix.Server.Services.MainServices;
using Phoenix.Shared.Common;
using Phoenix.Shared.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
        [Route("GetAllProducts")]
        public async Task<BaseResponse<ProductDto>> GetAllProducts(ProductRequest request)
        {
            return await _productService.GetAllProducts(request);
        }
    }
}