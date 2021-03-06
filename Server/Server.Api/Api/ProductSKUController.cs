using Phoenix.Server.Services.MainServices;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.ProductSKU;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/productSKU")]
    public class ProductSKUController : BaseApiController
    {
        private readonly IProductSKUService _productSKUService;
        public ProductSKUController(IProductSKUService productSKUService)
        {
            _productSKUService = productSKUService;
        }

        [HttpPost]
        [Route("GetAllProductSKUs")]
        public async Task<BaseResponse<ProductSKUDto>> GetAllProductSKUs(ProductSKURequest request)
        {
            return await _productSKUService.GetAllProductSKUs(request);
        }

        [Route("GetProductById")]
        public async Task<BaseResponse<ProductSKUAppDto>> GetProductById(ProductSKURequest request)
        {
            return await _productSKUService.GetProductById(request);
        }

        [HttpPost]
        [Route("UpdateProductSKUApp")]
        public Task<CrudResult> UpdateProductSKUApp(int Id, [FromBody] ProductSKURequest request)
        {
            return _productSKUService.UpdateProductSKUApp(Id, request);
        }
    }
}