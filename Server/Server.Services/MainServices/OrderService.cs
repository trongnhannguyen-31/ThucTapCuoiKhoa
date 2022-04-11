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
        Task<BaseResponse<OrderDto>> GetAllOrders(OrderRequest request);
        
        Task<BaseResponse<OrderDetailDto>> GetAllOrderDetailById(OrderDetailRequest request);
        ///
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
                             join d in _dataContext.OrderDetails on o.Id equals d.Order_Id
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
                                 Order_Id = o.Id,
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


        public async Task<BaseResponse<OrderDto>> ChangeStatusById(int id, OrderRequest request)
        {
            //tru so luong warehouse
            OrderDetailRequest.Order_Id = id;
            var data1 = await this.GetAllOrderDetailById(OrderDetailRequest);
            ListDetail = data1.Data.MapTo<OrderDetailDto>();

            foreach (var item in ListDetail)
            {
                warehouseRequest.ProductSKU_Id = item.ProductSKU_Id;
               var data = _warehouseService.GetWarehouseByProductSKUId(warehouseRequest);
               wareHouse = data.Result.Record;
                Warehouse warehouse1 = new Warehouse();
                warehouse1.Id = wareHouse.Id;
                warehouse1.ProductSKU_Id = wareHouse.ProductSKU_Id;
                warehouse1.Quantity = wareHouse.Quantity;


                /*warehouses.Id = warehouseRequest.Id;
                warehouses.ProductSKU_Id = warehouseRequest.ProductSKU_Id;
                warehouses.Quantity = warehouses.Quantity - (int)warehouseRequest.Quantity;
                warehouses.UpdatedAt = DateTime.Now;*/

                await _dataContext.SaveChangesAsync();
            }


            var result = new BaseResponse<OrderDto>();
            //var change = new BaseResponse<ChangeStatusDto>();
            try
            {

                var orders = GetOrderById(id);
                ChangeStatusRequest.Id = id;
                var data = GetListById(ChangeStatusRequest);
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
