using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.ProductType;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/productType")]
    public class ProductTypeController : BaseApiController
    {
        private readonly IProductTypeService _productTypeService;
        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        [HttpPost]
        [Route("GetAllProductTypes")]
        public async Task<BaseResponse<ProductTypeDto>> GetAllProductTypes(ProductTypeRequest request)
        {
            return await _productTypeService.GetAllProductTypes(request);
        }
    }
}