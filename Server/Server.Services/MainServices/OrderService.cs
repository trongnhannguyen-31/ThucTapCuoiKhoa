using AutoMapper;
using Falcon.Core;
using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Order;
using Phoenix.Shared.OrderDetail;
using Phoenix.Shared.ProductSKU;
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
        private readonly IProductSKUService _productSKUService;

        public OrderService(DataContext dataContext, IWarehouseService warehouseService, IProductSKUService productSKUService)
        {
            _dataContext = dataContext;
            _warehouseService = warehouseService;
            _productSKUService = productSKUService;
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
            var order = _dataContext.Orders.Find(id);
            var result = new BaseResponse<OrderDto>();
            try
            {
                if (order.CancelRequest == false && order.Status == "Đã duyệt, đang xử lý")
                {
                    var orders = GetOrderById(id);

                    // Gán Id(Order) => Order_Id (OrderDeatil);
                    OrderDetail.Order_Id = orders.Id;
                    // Lấy List của OrderDetail
                    var orderDetail_Id = await GetAllOrderDetailByOrderId(OrderDetail);
                    ListOrderDetail = orderDetail_Id.Data;


                    foreach (var item in ListOrderDetail)
                    {
                        // Warehouse
                        var sku = _productSKUService.GetProductSKUById(item.ProductSKU_Id);

                        WarehouseRequest.ProductSKU_Id = item.ProductSKU_Id;

                        var warehouse_Id = await GetAllWarehouseByOrderDetail(WarehouseRequest);
                        ListWarehouse = warehouse_Id.Data;

                        foreach (var warehouses in ListWarehouse)
                        {
                            var warehouses_data = _warehouseService.UpdateWarehouses(new WarehouseRequest
                            {
                                Id = warehouses.Id,
                                ProductSKU_Id = warehouses.ProductSKU_Id,
                                Quantity = warehouses.Quantity,
                                NewQuantity = (int)-item.Quantity
                                
                            });
                        }

                        sku.Id = sku.Id;
                        sku.BuyCount = sku.BuyCount + (int)item.Quantity;

                        orders.Status = "Đang giao hàng";
                    }
                }
                else if (order.Status == "Chờ xử lý")
                {
                    order.Status = "Đã duyệt, đang xử lý";
                }
                else if (order.Status == "Đang giao hàng")
                {
                    order.Status = "Đã giao hàng thành công";
                    order.DeliveryDate = DateTime.Now;
                }
                else if (order.CancelRequest == true)
                {
                    /*if (order.Status == "Chờ xử lý")
                    {
                        order.Status = "Hủy đơn hàng thành công";
                    }
                    else
                    {
                        var orders = GetOrderById(id);

                        // Gán Id(Order) => Order_Id (OrderDeatil);
                        OrderDetail.Order_Id = orders.Id;
                        // Lấy List của OrderDetail
                        var orderDetail_Id = await GetAllOrderDetailByOrderId(OrderDetail);
                        ListOrderDetail = orderDetail_Id.Data;

                        foreach (var item in ListOrderDetail)
                        {

                            WarehouseRequest.ProductSKU_Id = item.ProductSKU_Id;

                            var warehouse_Id = await GetAllWarehouseByOrderDetail(WarehouseRequest);
                            ListWarehouse = warehouse_Id.Data;

                            foreach (var warehouses in ListWarehouse)
                            {
                                var warehouses_data = _warehouseService.UpdateWarehouses(new WarehouseRequest
                                {
                                    Id = warehouses.Id,
                                    ProductSKU_Id = warehouses.ProductSKU_Id,
                                    Quantity = warehouses.Quantity,
                                    NewQuantity = (int)+item.Quantity

                                });
                            }


                            orders.Status = "Hủy đơn hàng thành công!";
                        }
                    }*/

                    order.Status = "Hủy đơn hàng thành công";
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

        #region web
        // Lấy Id của OrderDetail
        public async Task<BaseResponse<OrderDetailDto>> GetAllOrderDetailByOrderId(OrderDetailRequest request)
        {
            var result = new BaseResponse<OrderDetailDto>();
            try
            {
                //setup query
                var query = _dataContext.OrderDetails.AsQueryable();

                query = query.Where(x => x.Order_Id == request.Order_Id);


                var data = query.ToList();
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

        // Lấy Id của Warehouse
        public async Task<BaseResponse<WarehouseDto>> GetAllWarehouseByOrderDetail(WarehouseRequest request)
        {
            var result = new BaseResponse<WarehouseDto>();
            try
            {
                //setup query
                var query = _dataContext.Warehouses.AsQueryable();

                query = query.Where(x => x.ProductSKU_Id == request.ProductSKU_Id);


                var data = query.ToList();
                result.Data = data.MapTo<WarehouseDto>();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        // Lấy Id của ProductSKU
        public async Task<BaseResponse<ProductSKUDto>> GetAllProductSKUByOrderDetail(ProductSKURequest request)
        {
            var result = new BaseResponse<ProductSKUDto>();
            try
            {
                //setup query
                var query = _dataContext.ProductSKUs.AsQueryable();

                query = query.Where(x => x.Id == request.Id);


                var data = query.ToList();
                result.Data = data.MapTo<ProductSKUDto>();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        // Lấy List của OrderDetail
        public List<OrderDetailDto> ListOrderDetail { get; set; } = new List<OrderDetailDto>();

        public List<WarehouseDto> ListWarehouse { get; set; } = new List<WarehouseDto>();

        public List<ProductSKUDto> ListProductSKU { get; set; } = new List<ProductSKUDto>();

        public ProductSKURequest ProductSKURequest { get; set; } = new ProductSKURequest();
        #endregion


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

        public OrderDetailRequest OrderDetail { get; set; } = new OrderDetailRequest();

        public WarehouseRequest WarehouseRequest { get; set; } = new WarehouseRequest();
        public WarehouseOrderDto wareHouse { get; set; } = new WarehouseOrderDto();
        public List<OrderDetailDto> ListDetail { get; set; } = new List<OrderDetailDto>();
        public List<OrderDetailDto> ListDetailOrder { get; set; } = new List<OrderDetailDto>();
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

                //if (!string.IsNullOrEmpty(request.Address))
                //{
                //    query = query.Where(d => d.Address.Contains(request.Address));
                //}
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
            Order.CancelRequest = request.CancelRequest;
            Order.Status = "Yêu cầu hủy đang được xử lý";

            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
        #endregion
    }
}
