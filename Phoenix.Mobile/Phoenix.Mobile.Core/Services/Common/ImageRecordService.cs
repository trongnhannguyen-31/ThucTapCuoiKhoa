using Phoenix.Mobile.Core.Models.ImageRecord;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Common;
using Phoenix.Shared.ImageRecord;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IImageRecordService
    {
        Task<List<ImageRecordModel>> GetAllImages(ImageRecordRequest request);
    }

    public class ImageRecordService : IImageRecordService
    {
        private readonly IImageRecordProxy _imageRecordProxy;
        public ImageRecordService(IImageRecordProxy imageRecordProxy)
        {
            _imageRecordProxy = imageRecordProxy;
        }
        public async Task<List<ImageRecordModel>> GetAllImages(ImageRecordRequest request)
        {
            var imageRecord = await _imageRecordProxy.GetAllImages(request);
            return imageRecord.Data.MapTo<ImageRecordModel>();
        }
    }
}
