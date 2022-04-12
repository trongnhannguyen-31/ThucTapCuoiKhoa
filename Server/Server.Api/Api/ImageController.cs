using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.CartItem;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Phoenix.Shared.ImageRecord;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/images")]
    public class ImageController : BaseApiController
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        [Route("GetAllCartImages")]
        //public async Task<BaseResponse<CartItemDto>> GetAllCartItems(CartItemRequest request)
        public async Task<BaseResponse<ImageRecordDto>> GetAllImages(ImageRecordRequest request)
        {
            return await _imageService.GetAllImages(request);
        }

        
    }
}