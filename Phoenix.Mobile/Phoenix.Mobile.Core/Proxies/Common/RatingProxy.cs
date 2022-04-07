using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Rating;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IRatingProxy
    {
        Task<List<RatingDto>> GetAllRatings(RatingRequest request);
    }

    public class RatingProxy : BaseProxy, IRatingProxy
    {
        public async Task<List<RatingDto>> GetAllRatings(RatingRequest request)
        {
            try
            {
                var api = RestService.For<IRatingApi>(GetHttpClient());
                var result = await api.GetAllRatings(request);
                if (result == null) return new List<RatingDto>();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }
        public interface IRatingApi
        {
            [Post("/rating/GetAllRatings")]
            Task<List<RatingDto>> GetAllRatings([Body] RatingRequest request);

        }
    }
}