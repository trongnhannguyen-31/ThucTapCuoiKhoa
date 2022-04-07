using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.CartItem;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Phoenix.Shared.Core;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface ICartItemProxy
    {
        Task<BaseResponse<CartListDto>> GetAllCartItems(CartListRequest request);
        Task<CartItemDto> AddItemToCart(CartItemRequest request);
        Task<CrudResult> RemoveItemFromCart(int Id);
        Task<CrudResult> UpdateCart(int Id, CartItemRequest request);
        Task<CrudResult> ClearCart(int User_Id);
    }

    public class CartItemProxy : BaseProxy, ICartItemProxy
    {
        public async Task<BaseResponse<CartListDto>> GetAllCartItems(CartListRequest request)
        {
            try
            {
                var api = RestService.For<ICartItemApi>(GetHttpClient());
                //List<VendorDto> result = await api.GetAllVendors(request);
                //if (result == null) return new List<VendorDto>();
                return await api.GetAllCartItems(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public async Task<CartItemDto> AddItemToCart(CartItemRequest request)
        {
            try
            {
                var api = RestService.For<ICartItemApi>(GetHttpClient());
                var result = await api.AddItemToCArt(request);
                if (result == null) return new CartItemDto();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CartItemDto();
            }
        }

        public async Task<CrudResult> RemoveItemFromCart(int Id)
        {
            try
            {
                var api = RestService.For<ICartItemApi>(GetHttpClient());
                var result = await api.RemoveItemFromCart(Id);
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CrudResult();
            }
        }

        public async Task<CrudResult> UpdateCart(int Id, CartItemRequest request)
        {
            try
            {
                var api = RestService.For<ICartItemApi>(GetHttpClient());
                var result = await api.UpdateCart(Id, request);
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CrudResult();
            }
        }

        public async Task<CrudResult> ClearCart(int User_Id)
        {
            try
            {
                var api = RestService.For<ICartItemApi>(GetHttpClient());
                var result = await api.ClearCart(User_Id);
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CrudResult();
            }
        }

        public interface ICartItemApi
        {
            [Post("/cartitem/GetAllCartItems")]
            Task<BaseResponse<CartListDto>> GetAllCartItems([Body] CartListRequest request);

            [Post("/cartitem/AddItemToCart")]
            Task<CartItemDto> AddItemToCArt([Body] CartItemRequest request);

            [Delete("/cartitem/RemoveItemFromCart")]
            Task<CrudResult> RemoveItemFromCart(int Id);

            [Post("/cartitem/UpdateCart")]
            Task<CrudResult> UpdateCart(int Id, [Body] CartItemRequest request);

            [Delete("/cartitem/ClearCart")]
            Task<CrudResult> ClearCart(int User_Id);
        }
    }
}