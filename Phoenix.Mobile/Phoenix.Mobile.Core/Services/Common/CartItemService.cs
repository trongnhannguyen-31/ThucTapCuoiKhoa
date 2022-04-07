using Phoenix.Mobile.Core.Models.Cart;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Common;
using Phoenix.Shared.CartItem;
using System.Collections.Generic;
using System.Threading.Tasks;
using Phoenix.Mobile.Core.Models.CartItem;
using AutoMapper;
using Phoenix.Shared.Core;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface ICartItemService
    {
        Task<List<CartListModel>> GetAllCartItems(CartListRequest request);
        Task<CartItemModel> AddItemToCart(CartItemRequest request);
        Task<CrudResult> RemoveItemFromCart(int Id);
        Task<CrudResult> UpdateCart(int Id, CartItemRequest request);
        Task<CrudResult> ClearCart(int User_Id);
    }

    public class CartItemService : ICartItemService
    {
        private readonly ICartItemProxy _cartItemProxy;
        public CartItemService(ICartItemProxy cartItemProxy)
        {
            _cartItemProxy = cartItemProxy;
        }

        public async Task<List<CartListModel>> GetAllCartItems(CartListRequest request)
        {
            var cartItem = await _cartItemProxy.GetAllCartItems(request);
            return cartItem.Data.MapTo<CartListModel>();
        }

        public async Task<CartItemModel> AddItemToCart(CartItemRequest request)
        {
            var data = await _cartItemProxy.AddItemToCart(request);
            return data.MapTo<CartItemModel>();
        }

        public Task<CrudResult> RemoveItemFromCart(int Id)
        {
            return _cartItemProxy.RemoveItemFromCart(Id);
        }

        public Task<CrudResult> UpdateCart(int Id, CartItemRequest request)
        {
            return _cartItemProxy.UpdateCart(Id, request);
        }

        public Task<CrudResult> ClearCart(int User_Id)
        {
            return _cartItemProxy.ClearCart(User_Id);
        }
    }
}
