using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.ImageRecord;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/imageRecord")]
    public class ImageRecordController : BaseApiController
    {
        private readonly IImageRecordService _imageRecordService;
        public ImageRecordController(IImageRecordService imageRecordService)
        {
            _imageRecordService = imageRecordService;
        }

        [HttpPost]
        [Route("GetAllImages")]
        public async Task<BaseResponse<ImageRecordDto>> GetAllImages(ImageRecordRequest request)
        {
            return await _imageRecordService.GetAllImages(request);
        }
    }
}