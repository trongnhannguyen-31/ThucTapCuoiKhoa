using Falcon.Web.Core.Helpers;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Rating;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IRatingService
    {
        Task<BaseResponse<RatingDto>> GetAllRatings(RatingRequest request);
    }
    public class RatingService : IRatingService
    {
        private readonly DataContext _dataContext;
        public RatingService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Lấy danh sách đánh giá
        public async Task<BaseResponse<RatingDto>> GetAllRatings(RatingRequest request)
        {
            var result = new BaseResponse<RatingDto>();
            try
            {
                //setup query
                var query = _dataContext.Ratings.AsQueryable();

                //filter
                /*if (request.Id > 0)
                {
                    query = query.Where(d => d.Id == request.Id);
                } */

                if (request.Rate > 0)
                {
                    query = query.Where(d => d.Rate == request.Rate);
                }

                if (!string.IsNullOrEmpty(request.Comment))
                {
                    query = query.Where(d => d.Comment.Contains(request.Comment));
                }

                if (request.Customer_Id > 0)
                {
                    query = query.Where(d => d.Customer_Id == request.Customer_Id);

                }

                if (request.Product_Id > 0)
                {
                    query = query.Where(d => d.Product_Id == request.Product_Id);
                }

                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<RatingDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
