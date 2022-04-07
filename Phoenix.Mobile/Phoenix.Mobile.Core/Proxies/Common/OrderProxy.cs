using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.Order;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IOrderProxy
    {
        Task<BaseResponse<OrderDto>> GetAllAppOrders(OrderRequest request);
        Task<OrderDto> AddOrder(OrderRequest request);
        Task<BaseResponse<OrderDto>> GetLatestOrder(OrderRequest request);
    }

    public class OrderProxy : BaseProxy, IOrderProxy
    {
        public async Task<BaseResponse<OrderDto>> GetAllAppOrders(OrderRequest request)
        {
            try
            {
                var api = RestService.For<IOrderApi>(GetHttpClient());
                return await api.GetAllAppOrders(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public async Task<OrderDto> AddOrder(OrderRequest request)
        {
            try
            {
                var api = RestService.For<IOrderApi>(GetHttpClient());
                var result = await api.AddOrder(request);
                if (result == null) return new OrderDto();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new OrderDto();
            }
        }

        public async Task<BaseResponse<OrderDto>> GetLatestOrder(OrderRequest request)
        {
            try
            {
                var api = RestService.For<IOrderApi>(GetHttpClient());
                return await api.GetLatestOrder(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }



        public interface IOrderApi
        {
            [Post("/order/GetAllAppOrders")]
            Task<BaseResponse<OrderDto>> GetAllAppOrders([Body] OrderRequest request);
            [Post("/order/AddOrder")]
            Task<OrderDto> AddOrder([Body] OrderRequest request);
            [Post("/order/GetLatestOrder")]
            Task<BaseResponse<OrderDto>> GetLatestOrder([Body] OrderRequest request);
        }
    }
}