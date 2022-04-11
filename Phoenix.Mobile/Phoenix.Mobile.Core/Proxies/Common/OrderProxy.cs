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
        Task<BaseResponse<OrderAppDto>> GetAllAppOrders(OrderAppRequest request);
        Task<OrderAppDto> AddOrder(OrderAppRequest request);
        Task<BaseResponse<OrderAppDto>> GetLatestOrder(OrderAppRequest request);
    }

    public class OrderProxy : BaseProxy, IOrderProxy
    {
        public async Task<BaseResponse<OrderAppDto>> GetAllAppOrders(OrderAppRequest request)
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

        public async Task<OrderAppDto> AddOrder(OrderAppRequest request)
        {
            try
            {
                var api = RestService.For<IOrderApi>(GetHttpClient());
                var result = await api.AddOrder(request);
                if (result == null) return new OrderAppDto();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new OrderAppDto();
            }
        }

        public async Task<BaseResponse<OrderAppDto>> GetLatestOrder(OrderAppRequest request)
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
            Task<BaseResponse<OrderAppDto>> GetAllAppOrders([Body] OrderAppRequest request);
            [Post("/order/AddOrder")]
            Task<OrderAppDto> AddOrder([Body] OrderAppRequest request);
            [Post("/order/GetLatestOrder")]
            Task<BaseResponse<OrderAppDto>> GetLatestOrder([Body] OrderAppRequest request);
        }
    }
}