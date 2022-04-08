using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices.Media.Models;
using CommonHelper = Falcon.Core.CommonHelper;
using Falcon.Web.Core.Helpers;
using Falcon.Web.Core.Settings;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Falcon.Core;
using Phoenix.Server.Data.Entity;
using Falcon.Services.Security;

namespace Phoenix.Server.Services.MainServices.Media
{
    public class ImageService
    {
        private readonly DataContext _dataContext;
        private readonly ISettingService _settingService;
        private const string SubPath = "\\Upload\\Image";
        public string HostAddress =>  HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.Url.PathAndQuery, "");

        public ImageService(DataContext dataContext, ISettingService settingService)
        {
            _dataContext = dataContext;
            _settingService = settingService;
        }
        public async Task<ImageModel> GetById(int id)
        {
            var image = await _dataContext.ImageRecords.FindAsync(id);
            if (image == null || image.Deleted) return null;
            return image.MapTo<ImageModel>();
        }
        public IPagedList<ImageRecord> SearchAllImage(int page = 0, int pageSize = 20,
            string title = null)
        {
            var query = _dataContext.ImageRecords.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(x => x.FileName.Contains(title));
            }
            query = query.OrderByDescending(x => x.CreatedAt);
            return new PagedList<ImageRecord>(query, page, pageSize);
        }
        public ImageRecord WriteImageFile(string host, string fileName, byte[] content, string path = "")
        {
            host = string.IsNullOrEmpty(host) ? HostAddress : host;
            string extension = string.Empty;
            if(fileName.Length > 4)
            {
                extension = fileName.Substring(fileName.Length - 4);
                extension = extension.Contains(".") ? extension : string.Empty;
            }

            var now = DateTime.Today;
            var pathTime = now.Year.ToString("0000") + now.Month.ToString("00");
            var relPath = "/" + Path.Combine("Upload", pathTime, path.Replace('\\', '/'));
            var fullPath = CommonHelper.MapPath(relPath.Replace('\\', '/'));
            Directory.CreateDirectory(fullPath);
            fileName = DateTime.Now.Ticks + (string.IsNullOrEmpty(extension)? extension : ".png");
            //Save file to Image uploading folder
            //string base64String = Convert.ToBase64String(content);
            File.WriteAllBytes(Path.Combine(fullPath, fileName), content);
            var img = new ImageRecord()
            {
                CreatedAt = DateTime.Now,
                FileName = fileName,
                RelativePath = relPath.Replace('\\','/') + "/" + fileName,
                IsUsed = true,
            };
            img.AbsolutePath = host + img.RelativePath.Substring(host.EndsWith("/") ? 1 : 0);

            _dataContext.ImageRecords.Add(img);
            _dataContext.SaveChanges();
            return img;
        }
        public void DeleteImageFileInDisk(int id, string path="")
        {
            var now = DateTime.Today;
            //var pathTime = now.Year.ToString("0000") + now.Month.ToString("00");
            //var relPath = "/" + Path.Combine("Upload", pathTime, path.Replace('\\', '/'));
            //var fullPath = CommonHelper.MapPath(relPath.Replace('\\', '/'));
            var img = _dataContext.ImageRecords.Find(id);
            if(img != null)
            {
                if(File.Exists(Path.Combine(img.RelativePath, img.FileName)))
                {
                    File.Delete(Path.Combine(img.RelativePath, img.FileName));
                }
                _dataContext.ImageRecords.Remove(img);
                _dataContext.SaveChanges();
            }    

        }
        public void SoftDeleteImage(int id, string path = "")
        {
            var now = DateTime.Today;
            var pathTime = now.Year.ToString("0000") + now.Month.ToString("00");
            var relPath = "/" + Path.Combine("Upload", pathTime, path.Replace('\\', '/'));
            var fullPath = CommonHelper.MapPath(relPath.Replace('\\', '/'));
            var img = _dataContext.ImageRecords.Find(id);
            if (img != null)
            {
                if (File.Exists(Path.Combine(fullPath, img.FileName)))
                {
                    File.Delete(Path.Combine(fullPath, img.FileName));
                }
                img.Deleted = true;
                img.IsUsed = false;
                _dataContext.SaveChanges();
            }
        }
        public ImageRecord ReplaceOldImage(int? imageId, string host, string fileName, byte[] content, string path = "")
        {
            host = string.IsNullOrEmpty(host) ? HostAddress : host;
            string extension = string.Empty;
            if (fileName.Length > 4)
            {
                extension = fileName.Substring(fileName.Length - 4);
                extension = extension.Contains(".") ? extension : string.Empty;
            }
            if (imageId.HasValue)
            {
                var now = DateTime.Now;
                var pathTime = now.Year.ToString("0000") + now.Month.ToString("00");
                var relPath = "/" + Path.Combine("Upload", pathTime, path.Replace('\\', '/'));
                var fullPath = CommonHelper.MapPath(relPath.Replace('\\', '/'));
                Directory.CreateDirectory(fullPath);
                fileName = DateTime.Now.Ticks + (string.IsNullOrEmpty(extension) ? extension : ".png");
                //Save file to Image uploading folder
                //string base64String = Convert.ToBase64String(content);
                File.WriteAllBytes(Path.Combine(fullPath, fileName), content);
                var oldImage = _dataContext.ImageRecords.Find(imageId);
                DeleteImageInDisk(Path.Combine(fullPath, oldImage.FileName));
                oldImage.CreatedAt = DateTime.Now;
                oldImage.FileName = fileName;
                oldImage.RelativePath = relPath.Replace('\\', '/') + "/" + fileName;
                oldImage.AbsolutePath = host + oldImage.RelativePath.Substring(host.EndsWith("/") ? 1 : 0);
                _dataContext.SaveChanges();
                return oldImage;
            }
            return null;
        }
        public void DeleteImageInDisk(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }
        public ImageRecord GetImageById(int id) => _dataContext.ImageRecords.Find(id);
        public bool DeleteImage(ImageRecord entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            bool result = false;
            try
            {
                _dataContext.ImageRecords.Remove(entity);
                _dataContext.SaveChanges();
                result = true;
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }

        /*public async Task<ImageResponse> InsertImage(byte[] pictureBinary, string filename, string storeName = "pos123")
        {
            var _posContext = TokenHelper.PosContext();
            var storageFolder = $@"\uploads\{storeName}";
            if (!Directory.Exists(LocalMapPath(storageFolder)))
                Directory.CreateDirectory(LocalMapPath(storageFolder));

            #region validate
            if (pictureBinary == null || pictureBinary.Length < 0) return new ImageResponse() { Message = "File empty" };
            if (string.IsNullOrEmpty(filename)) return new ImageResponse() { Message = "File" };
            filename = Path.GetFileName(filename);

            if (!CheckImageFileType(filename))
                return new ImageResponse() { Message = "Image file type: .jpg, .png" };
            #endregion
            string newFileName = $"{DateTime.Now.Ticks}{Path.GetExtension(filename)}";
            var storageFolderPath = Path.Combine(LocalMapPath(storageFolder), newFileName);
            File.WriteAllBytes(storageFolderPath, pictureBinary);

            var relativePath = Path.Combine(storageFolder, newFileName);
            var serversettings = _settingService.LoadSetting<ServerSettings>();

            var image = new ImageRecord()
            {
#if DEBUG
                AbsolutePath = HostAddress + GenAbsolutePath(relativePath),
#else
                AbsolutePath = (string.IsNullOrEmpty(serversettings.CdnServer) ? HostAddress : serversettings.CdnServer) + GenAbsolutePath(relativePath),
#endif
                RelativePath = relativePath,
                IsUsed = true,
                IsExternal = false,
                CreatedAt = DateTime.Now,
                FileName = newFileName,
            };
            _posContext.ImageRecords.Add(image);
            await _posContext.SaveChangesAsync();
            return new ImageResponse()
            {
                IsOk = true,
                Message = "Success",
                Image = image.MapTo<ImageDto>()
            };
            _posContext.Dispose();
        }*/
    }
}
