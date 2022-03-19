using Phoenix.Mobile.Core.Framework;
using Phoenix.Mobile.Core.Models.Common;
using Phoenix.Framework.Core;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace Phoenix.Mobile.Core.Services
{
    public interface ICameraService
    {
        Task<BinaryAsset> ChooseFromGallery(int maxWidthHeight = 0);
        bool IsCameraAvailable();
        bool IsPickPhotoSupported();
        Task<BinaryAsset> TakeByCamera(int maxWidthHeight = 0);
    }

    public class CameraService : ICameraService
    {
        private readonly IExceptionHandler _exceptionHandler;
        public CameraService(IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }

        public async Task<BinaryAsset> ChooseFromGallery(int maxWidthHeight = 0)
        {
            try
            {

                if (!IsPickPhotoSupported())
                {
                    return null;
                }

                var result = new BinaryAsset();
                var options = new PickMediaOptions()
                {
                    //CustomPhotoSize = mediaSetting.PhotoSizePercent,
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    CompressionQuality = 92,
                    MaxWidthHeight = maxWidthHeight
                };

                var file = await CrossMedia.Current.PickPhotoAsync(options);
                if (file == null) return null;
                using (var ms = new MemoryStream())
                {
                    file.GetStream().CopyTo(ms);
                    result.Path = file.Path;
                    result.Content = ms.ToArray();
                    result.Ext = file.Path.GetFileType();
                }
                return result;

            }
            catch (Exception e)
            {
                _exceptionHandler.Handle(e);
            }
            return null;
        }

        public async Task<BinaryAsset> TakeByCamera(int maxWidthHeight = 0)
        {
            try
            {
                if (!IsCameraAvailable())
                {
                    return null;
                }

                var result = new BinaryAsset();
                var options = new StoreCameraMediaOptions
                {
                    Directory = "CongDongBau",
                    //Name = DateTime.Now.Ticks + ".jpg",
                    PhotoSize = maxWidthHeight > 0 ? PhotoSize.MaxWidthHeight : PhotoSize.Large,
                    CompressionQuality = 76,
                    MaxWidthHeight = maxWidthHeight
                };

                var file = await CrossMedia.Current.TakePhotoAsync(options);
                if (file == null) return null;
                using (var ms = new MemoryStream())
                {
                    file.GetStream().CopyTo(ms);
                    result.Path = file.Path;
                    result.Content = ms.ToArray();
                    result.Ext = file.Path.GetFileType();
                    result.Name = file.Path.GetFileName();
                    result.Type = file.Path.GetFileType();
                }
                return result;

            }
            catch (Exception e)
            {
                _exceptionHandler.Handle(e);
            }
            return null;
        }

        public bool IsCameraAvailable()
        {
            return CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported;
        }

        public bool IsPickPhotoSupported()
        {
            return CrossMedia.Current.IsPickPhotoSupported;
        }
    }
}
