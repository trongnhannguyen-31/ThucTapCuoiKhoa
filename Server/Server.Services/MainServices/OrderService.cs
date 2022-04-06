using AutoMapper;
using Falcon.Core;
using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Order;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IOrderService
    {
        Task<BaseResponse<OrderDto>> ChangeStatusById(int id, OrderRequest request);

        Task<BaseResponse<OrderDto>> GetAllOrders(OrderRequest request);
    }
    public class OrderService : IOrderService
    {
        private readonly DataContext _dataContext;
        public OrderService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public async Task<BaseResponse<OrderDto>> GetAllOrders(OrderRequest request)
        {
            var result = new BaseResponse<OrderDto>();
            try
            {
                //setup query
                var query = _dataContext.Orders.AsQueryable();

                //filter
                if (request.Id > 0)
                {
                    query = query.Where(d => d.Id == request.Id);
                }

                if (request.OrderDate == DateTime.Now)
                {
                    query = query.Where(d => d.OrderDate == request.OrderDate);
                }

                /*if (request.Status > 0) 
                {
                    query = query.Where(d => d.Status == request.Status); 
                }*/

                if (!string.IsNullOrEmpty(request.Address))
                {
                    query = query.Where(d => d.Address.Contains(request.Address));
                }

                if (request.Total > 0)
                {
                    query = query.Where(d => d.Total == request.Total);
                }

                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<OrderDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        // Lấy ID
        public Order GetOrderById(int id) => _dataContext.Orders.Find(id);
        //public OrderDetail GetOrderDetailById(int id) => _dataContext.OrderDatails.Find(id);

        // Thay đổi trạng thái
        /*public async Task<BaseResponse<OrderDto>> ChangeStatusById(int id, OrderRequest request)
        {
            var result = new BaseResponse<OrderDto>();
            try
            {
                var orders = GetOrderById(id);

                if (orders.Status == "Chờ xử lý")
                {
                    orders.Status = "Đã duyệt, chờ giao hàng";

                    ProductSKU productSKU = new ProductSKU();
                    productSKU.BuyCount = productSKU.BuyCount + 1;
                }
                else if (orders.Status == "Đã duyệt, chờ giao hàng")
                {
                    orders.Status = "Đã giao hàng";
                    orders.DeliveryDate = DateTime.Now;
                }

                await _dataContext.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }*/

        public async Task<BaseResponse<ChangeStatusDto>> GetListById(ChangeStatusRequest request)
        {
            var result = new BaseResponse<ChangeStatusDto>();
            try
            {
                var query = (from o in _dataContext.Orders
                             join d in _dataContext.OrderDatails on o.Id equals d.Order_Id
                             join s in _dataContext.ProductSKUs on d.ProductSKU_Id equals s.Id
                             join w in _dataContext.Warehouses on d.ProductSKU_Id equals w.ProductSKU_Id
                             select new
                             {
                                 Id = o.Id,
                                 OrderDate = o.OrderDate,
                                 Status = o.Status,
                                 DeliveryDate = o.DeliveryDate,
                                 Address = o.Address,
                                 Total = o.Total,
                                 Customer_Id = o.Customer_Id,
                                 CreatedAt = o.CreatedAt,
                                 Deleted = o.Deleted,
                                 Order_Id = d.Id,
                                 ProductSKU_Id = s.Id,
                                 Warehouse_Id = w.Id,
                             }).AsQueryable();
                if (request.Id != 0)
                {
                    query = query.Where(o => o.Id == request.Id);
                }
                var config = new MapperConfiguration(cfg => cfg.CreateMissingTypeMaps = true);
                var mapper = config.CreateMapper();
                var list = query.Select(mapper.Map<ChangeStatusDto>).ToList();

                result.Data = list.MapTo<ChangeStatusDto>();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        #region properties
        public List<ChangeStatusDto> ListOrder { get; set; } = new List<ChangeStatusDto>();

        public ChangeStatusRequest ChangeStatusRequest { get; set; } = new ChangeStatusRequest();
        #endregion

        public async Task<BaseResponse<OrderDto>> ChangeStatusById(int id, OrderRequest request)
        {
            var result = new BaseResponse<OrderDto>();
            //var change = new BaseResponse<ChangeStatusDto>();
            try
            {
                var orders = GetOrderById(id);
                //ChangeStatusRequest.Id = id;
                //var data = GetListById(ChangeStatusRequest);
                //ListOrder = data;


                if (orders.Status == "Chờ xử lý")
                {
                    orders.Status = "Đã duyệt, chờ giao hàng";
                }
                else if (orders.Status == "Yeu cau huy")
                {
                    orders.Status = "Xác nhận hủy đơn";
                }
                else if (orders.Status == "Đã duyệt, chờ giao hàng")
                {
                    orders.Status = "Đã giao hàng";
                    orders.DeliveryDate = DateTime.Now;
                }

                await _dataContext.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
