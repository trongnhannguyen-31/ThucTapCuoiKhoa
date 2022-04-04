﻿using Falcon.Core;
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

        // Thay đổi trạng thái
        public async Task<BaseResponse<OrderDto>> ChangeStatusById(int id, OrderRequest request)
        {
            var result = new BaseResponse<OrderDto>();
            try
            {
                var orders = GetOrderById(id);

                if (orders.Status == "Chờ duyệt")
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
        }

        /*public async Task<BaseResponse<OrderDto>> ChangeStatusById(int id, OrderRequest request)
        {
            var result = new BaseResponse<OrderDto>();
            try
            {
                var orders = GetOrderById(id);

                if (orders.Status == "Chờ duyệt")
                {
                    orders.Status = "Đã duyệt, chờ giao hàng";
                    OrderDetail orderDetails = new OrderDetail();
                    ProductSKU productSKU = new ProductSKU();
                    orders.Id = orderDetails.Order_Id;
                    if (orderDetails != null)
                    {
                        orderDetails.ProductSKU_Id = productSKU.Id;
                        productSKU.BuyCount = productSKU.BuyCount - 1;
                    }    

                    *//*ProductSKU productSKU = new ProductSKU();
                    productSKU.BuyCount = productSKU.BuyCount + 1;*//*
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
    }
}
