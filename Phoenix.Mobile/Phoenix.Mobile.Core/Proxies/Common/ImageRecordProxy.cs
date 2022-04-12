using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.ImageRecord;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IImageRecordProxy
    {
        Task<BaseResponse<ImageRecordDto>> GetAllImages(ImageRecordRequest request);
    }

    public class ImageRecordProxy : BaseProxy, IImageRecordProxy
    {
        public async Task<BaseResponse<ImageRecordDto>> GetAllImages(ImageRecordRequest request)
        {
            try
            {
                var api = RestService.For<IImageRecordApi>(GetHttpClient());
                //List<VendorDto> result = await api.GetAllVendors(request);
                //if (result == null) return new List<VendorDto>();
                return await api.GetAllImages(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }
        public interface IImageRecordApi
        {
            [Post("/image/GetAllImages")]
            Task<BaseResponse<ImageRecordDto>> GetAllImages([Body] ImageRecordRequest request);

        }
    }
}