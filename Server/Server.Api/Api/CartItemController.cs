using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.CartItem;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/cartitem")]
    public class CartItemController : BaseApiController
    {
        private readonly ICartItemService _cartItemService;
        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpPost]
        [Route("GetAllCartItems")]
        //public async Task<BaseResponse<CartItemDto>> GetAllCartItems(CartItemRequest request)
        public async Task<BaseResponse<CartListDto>> GetAllCartItems(CartListRequest request)
        {
            return await _cartItemService.GetAllCartItems(request);
        }

        [HttpPost]
        [Route("AddItemToCart")]
        public Task<CrudResult> AddItemToCart([FromBody] CartItemRequest request)

        {
            return _cartItemService.AddItemToCart(request);
        }

        [HttpDelete]
        [Route("RemoveItemFromCart")]
        public Task<CrudResult> RemoveItemFromCart(int Id)

        {
            return _cartItemService.RemoveItemFromCart(Id);
        }

        [HttpDelete]
        [Route("ClearCart")]
        public Task<CrudResult> ClearCart(int User_Id)

        {
            return _cartItemService.ClearCart(User_Id);
        }

        [HttpPost]
        [Route("UpdateCart")]
        public Task<CrudResult> UpdateCart(int Id, [FromBody] CartItemRequest request)
        {
            return _cartItemService.UpdateCart(Id, request);
        }
    }
}