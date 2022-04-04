using Falcon.Web.Core.Helpers;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.OrderDetail;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{

    public interface IOrderDetailService
    {
        Task<BaseResponse<OrderDetailDto>> GetAllOrderDetails(OrderDetailRequest request);

        Task<BaseResponse<OrderDetailDto>> GetAllOrderDetailById(int id, OrderDetailRequest request);
    }

    public class OrderDetailService : IOrderDetailService
    {
        private readonly DataContext _dataContext;
        public OrderDetailService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Lấy danh sách chi tiết đơn hàng
        public async Task<BaseResponse<OrderDetailDto>> GetAllOrderDetails(OrderDetailRequest request)
        {
            var result = new BaseResponse<OrderDetailDto>();
            try
            {

                //setup query
                var query = _dataContext.OrderDatails.AsQueryable();
                
                //filter
                if (request.Id > 0)
                {
                    query = query.Where(d => d.Id == request.Id);
                }

                if (request.Order_Id > 0)
                {
                    query = query.Where(d => d.Order_Id == request.Order_Id);
                }

                if (request.ProductSKU_Id > 0)
                {
                    query = query.Where(d => d.ProductSKU_Id == request.ProductSKU_Id);
                }

                if (request.Price > 0)
                {
                    query = query.Where(d => d.Price == request.Price);
                }

                if (request.Quantity > 0)
                {
                    query = query.Where(d => d.Quantity == request.Quantity);
                }

                if (request.Total > 0)
                {
                    query = query.Where(d => d.Total == request.Total);
                }

                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<OrderDetailDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        #region GetOrderDetailById
        public async Task<BaseResponse<OrderDetailDto>> GetAllOrderDetailById(int id, OrderDetailRequest request)
        {
            var result = new BaseResponse<OrderDetailDto>();
            try
            {
                //setup query
                var query = _dataContext.OrderDatails.AsQueryable();

                //filter
                if (request.Order_Id > 0)
                {
                    query = query.Where(d => d.Order_Id == request.Order_Id);
                }

                query = query.OrderByDescending(d => d.Id);

                var list = _dataContext.OrderDatails.Where(p => p.Order_Id.Equals(id));

                var data = await list.ToListAsync();
                result.Data = data.MapTo<OrderDetailDto>();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }
        #endregion
    }
}
