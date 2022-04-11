using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Mobile.Core.Models.Rating;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Shared.Common;
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
        Task<BaseResponse<RatingAppDto>> GetRatingByProductSKUId(RatingAppRequest request);
        Task<RatingAppDto> AddRating(RatingAppRequest request);
    }

    public class RatingProxy : BaseProxy, IRatingProxy
    {

        public async Task<BaseResponse<RatingAppDto>> GetRatingByProductSKUId(RatingAppRequest request)
        {
            try
            {
                var api = RestService.For<IRatingApi>(GetHttpClient());
                return await api.GetRatingByProductSKUId(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public async Task<RatingAppDto> AddRating(RatingAppRequest request)
        {
            try
            {
                var api = RestService.For<IRatingApi>(GetHttpClient());
                var result = await api.AddRating(request);
                if (result == null) return new RatingAppDto();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new RatingAppDto();
            }
        }

        public interface IRatingApi
        {
            [Post("/rating/GetRatingByProductSKUId")]
            Task<BaseResponse<RatingAppDto>> GetRatingByProductSKUId([Body] RatingAppRequest request);

            [Post("/rating/AddRating")]
            Task<RatingAppDto> AddRating([Body] RatingAppRequest request);
        }
    }
}