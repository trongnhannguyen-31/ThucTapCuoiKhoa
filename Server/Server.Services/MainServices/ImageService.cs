using AutoMapper;
using Falcon.Core;
using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.ImageRecord;
using Phoenix.Shared.Order;
using Phoenix.Shared.OrderDetail;
using Phoenix.Shared.Warehouse;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IImageService
    {

        ///
        Task<BaseResponse<ImageRecordDto>> GetAllImages(ImageRecordRequest request);
    }
    public class ImageService : IImageService
    {
        private readonly DataContext _dataContext;
        private readonly IImageService _imageService;
        public ImageService(DataContext dataContext, IImageService imageService)
        {
            _dataContext = dataContext;
            _imageService = imageService;
        }

        

        #region properties
        
        #endregion

       

        #region GetAllImage
        public async Task<BaseResponse<ImageRecordDto>> GetAllImages(ImageRecordRequest request)
        {
            var result = new BaseResponse<ImageRecordDto>();
            try
            {

                //setup query
                var query = _dataContext.ImageRecords.AsQueryable();
                //filter
                query = query.Where(d => d.Id==(request.Id));

                var data = await query.ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<ImageRecordDto>();


            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

       
    }
}
