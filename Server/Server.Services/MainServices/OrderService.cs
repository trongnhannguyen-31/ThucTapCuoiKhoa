using AutoMapper;
using Falcon.Core;
using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Order;
using Phoenix.Shared.OrderDetail;
using Phoenix.Shared.Warehouse;
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

        Task<BaseResponse<OrderDto>> OrdersCancelById(int id, OrderRequest request);
        Task<BaseResponse<OrderDto>> GetAllOrders(OrderRequest request);
        
        Task<BaseResponse<OrderDetailDto>> GetAllOrderDetailById(OrderDetailRequest request);

        Task<BaseResponse<OrderDto>> GetAllCancelOrders(OrderRequest request);

        Task<BaseResponse<OrderAppDto>> GetAllAppOrders(OrderAppRequest request);
        Task<CrudResult> AddOrder(OrderAppRequest request);
        Task<BaseResponse<OrderAppDto>> GetLatestOrder(OrderAppRequest request);
        Task<CrudResult> EditOrder(int Id, OrderAppRequest request);
    }
    public class OrderService : IOrderService
    {
        private readonly DataContext _dataContext;
        private readonly IWarehouseService _warehouseService;
        public OrderService(DataContext dataContext, IWarehouseService warehouseService)
        {
            _dataContext = dataContext;
            _warehouseService = warehouseService;
        }

        #region List
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

                if (!string.IsNullOrEmpty(request.Address))
                {
                    query = query.Where(d => d.Address.Contains(request.Address));
                }

                if (request.Total > 0)
                {
                    query = query.Where(d => d.Total == request.Total);
                }

                /*if (request.CancelRequest == false)
                {
                    query = query.Where(d => d.CancelRequest.Equals(request.CancelRequest));
                }*/

                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<OrderDto>();
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

        public async Task<BaseResponse<OrderDto>> GetAllCancelOrders(OrderRequest request)
        {
            var result = new BaseResponse<OrderDto>();
            try
            {
                //setup query
                var query = _dataContext.Orders.AsQueryable();
                
                query = query.Where(d => d.CancelRequest == true);            

                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<OrderDto>();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success= false;
                result.Message= ex.Message;
            }

            return result;
        }

        // Lấy ID
        public Order GetOrderById(int id) => _dataContext.Orders.Find(id);

        // Thay đổi trạng thái
        public async Task<BaseResponse<OrderDto>> ChangeStatusById(int id, OrderRequest request)
        {
            var result = new BaseResponse<OrderDto>();
            try
            {
                var orders = GetOrderById(id);
                if (orders.CancelRequest == true)
                {
                    orders.Status = "Hủy đơn hàng thành công";
                    //orders.CancelRequest = false;
                    /*if (orders.Status == "Chờ xử lý" || orders.Status == "Đã duyệt, chờ xử lý")
                    {
                        orders.Status = "Hủy đơn hàng thành công";
                    }
                    else if (orders.Status == "Đã giao hàng")
                    {
                        orders.Status = "Đơn hàng đang được vận chuyển, hủy đơn hàng không thành công";
                    }
                    else if (orders.Status == "Đơn hàng đang được vận chuyển, hủy đơn hàng không thành công")
                    {
                        orders.Status = "Đã giao hàng thành công";
                        orders.DeliveryDate = DateTime.Now;
                    }*/
                }
                else
                {
                    if (orders.Status == "Chờ xử lý")
                    {
                        orders.Status = "Đã duyệt, đang xử lý";
                    }
                    else if (orders.Status == "Đã duyệt, đang xử lý")
                    {
                        orders.Status = "Đang giao hàng";
                    }
                    else if (orders.Status == "Đang giao hàng")
                    {
                        orders.Status = "Đã giao hàng thành công";
                        orders.DeliveryDate = DateTime.Now;
                    }
                    
                }
                
                /*if (orders.Status == "Chờ xử lý")
                {
                    orders.Status = "Đã duyệt, đang xử lý";
                }
                else if (orders.Status == "Đã duyệt, đang xử lý")
                {
                    orders.Status = "Đang giao hàng";
                }
                else if (orders.Status == "Đang giao hàng")
                {
                    orders.Status = "Đã giao hàng";
                    orders.DeliveryDate = DateTime.Now;
                }
                */


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

        public async Task<BaseResponse<OrderDto>> OrdersCancelById(int id, OrderRequest request)
        {
            var result = new BaseResponse<OrderDto>();
            try
            {
                var orders = GetOrderById(id);

                if (orders.CancelRequest == true)
                {
                    if (orders.Status == "Chờ xử lý" || orders.Status == "Đã duyệt, chờ xử lý")
                    {
                        orders.Status = "Hủy đơn hàng thành công";
                    }
                    else
                    {
                        orders.Status = "Không thể hủy đơn hàng";
                    }
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

        #region properties
        public List<ChangeStatusDto> ListOrder { get; set; } = new List<ChangeStatusDto>();

        public ChangeStatusRequest ChangeStatusRequest { get; set; } = new ChangeStatusRequest();

        public OrderDetailRequest OrderDetailRequest { get; set; } = new OrderDetailRequest();
        public WarehouseOrderRequest warehouseRequest { get; set; } = new WarehouseOrderRequest();
        public WarehouseOrderDto wareHouse { get; set; } = new WarehouseOrderDto();
        public List<OrderDetailDto> ListDetail { get; set; } = new List<OrderDetailDto>();
        #endregion

        #region GetOrderDetailById
        public async Task<BaseResponse<OrderDetailDto>> GetAllOrderDetailById(OrderDetailRequest request)
        {
            var result = new BaseResponse<OrderDetailDto>();
            try
            {
                //setup query
                var query = _dataContext.OrderDetails.AsQueryable();

                query = query.Where(x => x.Order_Id == request.Order_Id);

                //filter
                //if (request.Order_Id > 0)
                //{
                //    query = query.Where(d => d.Order_Id == request.Order_Id);
                //}

               // query = query.OrderByDescending(d => d.Id);

                //var list = _dataContext.OrderDatails.Where(p => p.Order_Id.Equals(request.Order_Id));

                var data = query.ToList();
                result.Data = data.MapTo<OrderDetailDto>();
                //result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }
        #endregion

        #region GetAllAppOrders
        public async Task<BaseResponse<OrderAppDto>> GetAllAppOrders(OrderAppRequest request)
        {
            var result = new BaseResponse<OrderAppDto>();
            try
            {

                //setup query
                var query = _dataContext.Orders.AsQueryable();
                //filter
                query = query.Where(d => d.Customer_Id.Equals(request.Customer_Id));
                query = query.OrderByDescending(d => d.Id);

                var data = await query.ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<OrderAppDto>();


            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        #region AddOrder
        public async Task<CrudResult> AddOrder(OrderAppRequest request)
        {
            var Order = new Order();
            Order.OrderDate = request.OrderDate;
            Order.Status = request.Status;
            Order.DeliveryDate = request.DeliveryDate;
            Order.Address = request.Address;
            Order.Total = request.Total;
            Order.IsRated = request.IsRated;
            Order.Customer_Id = request.Customer_Id;
            Order.CreatedAt = request.CreatedAt;
            Order.Deleted = request.Deleted;

            _dataContext.Orders.Add(Order);

            await _dataContext.SaveChangesAsync();
            //int a = Order.Id;
            return new CrudResult() { IsOk = true };
        }
        #endregion

        #region GetLatestOrder
        public async Task<BaseResponse<OrderAppDto>> GetLatestOrder(OrderAppRequest request)
        {
            var result = new BaseResponse<OrderAppDto>();
            try
            {
                //setup query
                var query = _dataContext.Orders.AsQueryable();

                if (!string.IsNullOrEmpty(request.Address))
                {
                    query = query.Where(d => d.Address.Contains(request.Address));
                }
                query = query.OrderByDescending(d => d.Id);

                var data = await query.FirstAsync();
                result.Record = data.MapTo<OrderAppDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        #region EditOrder
        public async Task<CrudResult> EditOrder(int Id, OrderAppRequest request)
        {
            var Order = _dataContext.Orders.Find(Id);
            Order.IsRated = request.IsRated;

            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
        #endregion
    }
}
