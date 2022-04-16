using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.CartItem;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Phoenix.Shared.Core;
using AutoMapper;

namespace Phoenix.Server.Services.MainServices
{
    public interface ICartItemService
    {
        //Task<BaseResponse<CartItemDto>> GetAllCartItems(CartItemRequest request);
        Task<BaseResponse<CartListDto>> GetAllCartItems(CartListRequest request);
        Task<CrudResult> AddItemToCart(CartItemRequest request);
        Task<CrudResult> RemoveItemFromCart(int Id);
        Task<CrudResult> ClearCart(int User_Id);
        Task<CrudResult> UpdateCart(int Id, CartItemRequest request);
    }

    public class CartItemService : ICartItemService
    {
        private readonly DataContext _dataContext;

        public CartItemService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }



        // Lấy danh sách nhà cung cấp
        //public async Task<BaseResponse<CartItemDto>> GetAllCartItems(CartItemRequest request)
        public async Task<BaseResponse<CartListDto>> GetAllCartItems(CartListRequest request)
        {
            var result = new BaseResponse<CartListDto>();
            try
            {
                var query = (from c in _dataContext.CartItems
                             join s in _dataContext.ProductSKUs on c.ProductSKU_Id equals s.Id
                             join p in _dataContext.Products on s.Product_Id equals p.Id
                             select new
                             {
                                 Id = c.Id,
                                 ProductSKUId = s.Id,
                                 ProductName = p.Name,
                                 ProductTypeId = p.ProductType_Id,
                                 Model = p.ModelCode,
                                 Ram = s.Ram,
                                 Storage = s.Storage,
                                 Price = s.Price,
                                 Quantity = c.Quantity,
                                 UserID = c.User_Id

                             }).AsQueryable();
                if (request.UserID != 0)
                {
                    query = query.Where(d => d.UserID.Equals(request.UserID));
                }



                var config = new MapperConfiguration(cfg => cfg.CreateMissingTypeMaps = true);
                var mapper = config.CreateMapper();
                var listcart = query.Select(mapper.Map<CartListDto>).ToList();

                //var data = await query.ToListAsync();

                //result.Data = data.MapTo<CartListDto>();
                result.Data = listcart.MapTo<CartListDto>();

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public async Task<CrudResult> AddItemToCart(CartItemRequest request)
        {
            var CartItem = new CartItem();
            CartItem.ProductSKU_Id = request.ProductSKU_Id;
            CartItem.Quantity = request.Quantity;
            CartItem.User_Id = request.User_Id;

            _dataContext.CartItems.Add(CartItem);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        public async Task<CrudResult> RemoveItemFromCart(int Id)
        {
            var CartItem = _dataContext.CartItems.Find(Id);
            if (CartItem == null)
            {
                return new CrudResult()
                {
                    ErrorCode = CommonErrorStatus.KeyNotFound,
                    ErrorDescription = "Xóa không thành công."
                };
            }
            else
            {
                _dataContext.CartItems.Remove(CartItem);
                await _dataContext.SaveChangesAsync();
                return new CrudResult() { IsOk = true };
            }
        }
        //int IdMedicine, MedicineRequest request
        public async Task<CrudResult> UpdateCart(int Id, CartItemRequest request)
        {
            var CartItem = _dataContext.CartItems.Find(Id);
            CartItem.ProductSKU_Id = request.ProductSKU_Id;
            CartItem.Quantity = request.Quantity;
            CartItem.User_Id = request.User_Id;

            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        public async Task<CrudResult> ClearCart(int User_Id)
        {
            var CartItem = _dataContext.CartItems.AsQueryable().Where(a => a.User_Id == User_Id);
            //var CartItem = _dataContext.CartItems.Find(User_Id);
            if (CartItem == null)
            {
                return new CrudResult()
                {
                    ErrorCode = CommonErrorStatus.KeyNotFound,
                    ErrorDescription = "Xóa không thành công."
                };
            }
            else
            {
                //foreach (var item in CartItem)
                //{
                //    _dataContext.CartItems.RemoveRange(CartItem);
                //}
                _dataContext.CartItems.RemoveRange(CartItem);
                await _dataContext.SaveChangesAsync();
                return new CrudResult() { IsOk = true };
            }
        }
    }
}