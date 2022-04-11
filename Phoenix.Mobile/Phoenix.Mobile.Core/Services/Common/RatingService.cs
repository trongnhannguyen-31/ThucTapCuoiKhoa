using Phoenix.Mobile.Core.Models.Rating;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Rating;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IRatingService
    {
        Task<List<RatingModel>> GetRatingByProductSKUId(RatingAppRequest request);
        Task<RatingModel> AddRating(RatingAppRequest request);
    }
    public class RatingService : IRatingService
    {
        private readonly IRatingProxy _ratingProxy;
        public RatingService(IRatingProxy ratingProxy)
        {
            _ratingProxy = ratingProxy;
        }

        public async Task<List<RatingModel>> GetRatingByProductSKUId(RatingAppRequest request)
        {
            var Rating = await _ratingProxy.GetRatingByProductSKUId(request);
            return Rating.Data.MapTo<RatingModel>();
        }

        public async Task<RatingModel> AddRating(RatingAppRequest request)
        {
            var data = await _ratingProxy.AddRating(request);
            return data.MapTo<RatingModel>();
        }
    }
}
