using Phoenix.Mobile.Core.Models.Common;
using Phoenix.Mobile.Core.Proxies;
using Phoenix.Mobile.Core.Utils;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services
{
    public interface IFileService
    {
        Task<FileInfoModel> UploadImage(BinaryAsset image);
    }

    public class FileService : IFileService
    {
        private readonly IFileProxy _fileProxy;
        public FileService(IFileProxy fileProxy)
        {
            _fileProxy = fileProxy;
        }
        public async Task<FileInfoModel> UploadImage(BinaryAsset image)
        {
            return (await _fileProxy.UploadImage(new Shared.Media.FileUploadDto
            {
                Content = image.Content,
                Name = image.Name,
                Type = image.Ext
            })).MapTo<FileInfoModel>();
        }
    }
}
