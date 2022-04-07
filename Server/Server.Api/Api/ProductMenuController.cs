using Phoenix.Server.Services.MainServices;
using Phoenix.Shared.Common;
using Phoenix.Shared.Product;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/productMenu")]
    public class ProductMenuController : BaseApiController
    {
        private readonly IProductMenuService _productMenuService;
        public ProductMenuController(IProductMenuService productMenuService)
        {
            _productMenuService = productMenuService;
        }

        [HttpPost]
        [Route("GetAllProductMenus")]
        public async Task<BaseResponse<ProductMenuDto>> GetAllProductMenus(ProductMenuRequest request)
        {
            return await _productMenuService.GetAllProductMenus(request);
        }

        //[Route("GetProductById")]
        //public async Task<BaseResponse<ProductMenuDto>> GetProductById(ProductMenuRequest request)
        //{
        //    return await _productMenuService.GetProductById(request);
        //}
    }
}