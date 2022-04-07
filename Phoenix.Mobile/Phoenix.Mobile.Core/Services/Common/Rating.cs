using Phoenix.Mobile.Core.Models.Rating;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Rating;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IRatingService
    {
        Task<List<RatingModel>> GetAllRatings(RatingRequest request);
    }

    public class RatingService : IRatingService
    {
        private readonly IRatingProxy _ratingProxy;
        public RatingService(IRatingProxy ratingProxy)
        {
            _ratingProxy = ratingProxy;
        }
        public async Task<List<RatingModel>> GetAllRatings(RatingRequest request)
        {
            var data = await _ratingProxy.GetAllRatings(request);
            return data.MapTo<RatingModel>();
        }
    }
}
