using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Media;
using Phoenix.Framework.Core;
using Refit;
using System;
using System.Threading.Tasks;


namespace Phoenix.Mobile.Core.Proxies
{
    public interface IFileProxy
    {
        Task<FileInfoDto> UploadImage(FileUploadDto request);
    }

    public class FileProxy : BaseProxy, IFileProxy
    {
        public async Task<FileInfoDto> UploadImage(FileUploadDto request)
        {
            try
            {
                var api = RestService.For<IFileApi>(GetHttpClient());
                var results = await api.UploadImageMobile(request);
                if (results == null) return new FileInfoDto();
                return results;
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(new NetworkException(e), true);
                return new FileInfoDto();
            }
        }

        public interface IFileApi
        {
            //[Post("/media")]
            //Task<FileInfoDto> UploadImageMobile(FileUploadDto request);

            [Post("/media/uploadimagemobile")]
            [Headers("Authorization: Bearer")]
            Task<FileInfoDto> UploadImageMobile([Body] FileUploadDto request);
        }
    }
}
