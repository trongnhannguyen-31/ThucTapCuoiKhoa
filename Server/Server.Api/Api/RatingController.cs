using Phoenix.Server.Services.MainServices;
using Phoenix.Shared.Common;
using Phoenix.Shared.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/rating")]
    public class RatingController : BaseApiController
    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        [Route("GetAllRatings")]
        public async Task<BaseResponse<RatingDto>> GetAllRatings(RatingRequest request)
        {
            return await _ratingService.GetAllRatings(request);
        }

        [HttpPost]
        [Route("GetRatingByProductSKUId")]
        //public async Task<BaseResponse<CartItemDto>> GetAllCartItems(CartItemRequest request)
        public async Task<BaseResponse<RatingAppDto>> GetRatingByProductSKUId(RatingAppRequest request)
        {
            return await _ratingService.GetRatingByProductSKUId(request);
        }
    }
}