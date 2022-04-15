using Falcon.Web.Core.Helpers;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Server.Data.Entity;
using System.Data.Entity;
using Phoenix.Shared.Core;
using Phoenix.Shared.OrderDetail;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Phoenix.Server.Services.MainServices
{

    public interface IOrderDetailService
    {
        Task<BaseResponse<OrderDetailDto>> GetAllOrderDetails(OrderDetailRequest request);
        
        Task<BaseResponse<OrderDetailDto>> GetAllOrderDetailById(int id, OrderDetailRequest request);


        Task<CrudResult> AddOrderDetail(OrderDetailAppRequest request);

        Task<BaseResponse<OrderDetailHistoryDto>> GetOrderDetailHistory(OrderDetailHistoryRequest request);
        Task<BaseResponse<OrderDetailHistoryDto>> GetOrderDetailHistoryById(OrderDetailHistoryRequest request);
    }

    public class OrderDetailService : IOrderDetailService
    {
        private readonly DataContext _dataContext;
        public OrderDetailService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region GetAllOrderDetails
        // Lấy danh sách chi tiết đơn hàng
        public async Task<BaseResponse<OrderDetailDto>> GetAllOrderDetails(OrderDetailRequest request)
        {
            var result = new BaseResponse<OrderDetailDto>();
            try
            {

                //setup query
                var query = _dataContext.OrderDetails.AsQueryable();

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
        #endregion

        #region GetOrderDetailById
        public async Task<BaseResponse<OrderDetailDto>> GetAllOrderDetailById(int id, OrderDetailRequest request)
        {
            var result = new BaseResponse<OrderDetailDto>();
            try
            {
                //setup query
                var query = _dataContext.OrderDetails.AsQueryable();

                //filter
                if (request.Order_Id > 0)
                {
                    query = query.Where(d => d.Order_Id == request.Order_Id);
                }

                query = query.OrderByDescending(d => d.Id);

                var list = _dataContext.OrderDetails.Where(p => p.Order_Id.Equals(id));

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

        #region AddOrderDetail
        public async Task<CrudResult> AddOrderDetail(OrderDetailAppRequest request)
        {
            var OrderDetail = new OrderDetail();
            OrderDetail.Order_Id = request.Order_Id;
            OrderDetail.ProductSKU_Id = request.ProductSKU_Id;
            OrderDetail.Price = request.Price;
            OrderDetail.Quantity = request.Quantity;
            OrderDetail.Total = (request.Price * request.Quantity);

            _dataContext.OrderDetails.Add(OrderDetail);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
        #endregion

        #region GetOrderDetailHistory
        public async Task<BaseResponse<OrderDetailHistoryDto>> GetOrderDetailHistory(OrderDetailHistoryRequest request)
        {
            var result = new BaseResponse<OrderDetailHistoryDto>();
            try
            {
                var query = (from o in _dataContext.Orders
                             join d in _dataContext.OrderDetails on o.Id equals d.Order_Id
                             join s in _dataContext.ProductSKUs on d.ProductSKU_Id equals s.Id
                             join p in _dataContext.Products on s.Product_Id equals p.Id
                             select new
                             {
                                 Image = p.Image1,
                                 Name = p.Name,
                                 ProductTypeId = p.ProductType_Id,
                                 ModelCode = p.ModelCode,
                                 Ram = s.Ram,
                                 Storage = s.Storage,
                                 Quantity = d.Quantity,
                                 Price = d.Price,
                                 ProductSKU_Id = d.ProductSKU_Id,
                                 Order_Id = o.Id
                             }).AsQueryable();

                var congfig = new MapperConfiguration(cfg => cfg.CreateMissingTypeMaps = true);
                var mapper = congfig.CreateMapper();
                var detail = query.Select(mapper.Map<OrderDetailHistoryDto>).Where(d => d.Order_Id == request.Order_Id);
                result.Data = detail.MapTo<OrderDetailHistoryDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        #region GetOrderDetailHistoryById
        public async Task<BaseResponse<OrderDetailHistoryDto>> GetOrderDetailHistoryById(OrderDetailHistoryRequest request)
        {
            var result = new BaseResponse<OrderDetailHistoryDto>();
            try
            {
                var query = (from o in _dataContext.Orders
                             join d in _dataContext.OrderDetails on o.Id equals d.Order_Id
                             join s in _dataContext.ProductSKUs on d.ProductSKU_Id equals s.Id
                             join p in _dataContext.Products on s.Product_Id equals p.Id
                             select new
                             {
                                 Image = p.Image1,
                                 Name = p.Name,
                                 ProductTypeId = p.ProductType_Id,
                                 ModelCode = p.ModelCode,
                                 Ram = s.Ram,
                                 Storage = s.Storage,
                                 Quantity = d.Quantity,
                                 Price = d.Price,
                                 ProductSKU_Id = d.ProductSKU_Id,
                                 Order_Id = o.Id
                             }).AsQueryable();

                var congfig = new MapperConfiguration(cfg => cfg.CreateMissingTypeMaps = true);
                var mapper = congfig.CreateMapper();
                var detail = query.Select(mapper.Map<OrderDetailHistoryDto>).Where(d => d.Order_Id == request.Order_Id).FirstOrDefault();
                result.Record = detail.MapTo<OrderDetailHistoryDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion
    }
}
