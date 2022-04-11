using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.OrderDetail;
using Phoenix.Shared.Order;
using Phoenix.Mobile.Core.Models.OrderDetail;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Phoenix.Shared.Common;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IOrderDetailProxy
    {
        Task<List<OrderDetailAppDto>> GetAllOrderDetails(OrderDetailAppRequest request);

        Task<OrderDetailAppDto> AddOrderDetail(OrderDetailAppRequest request);
        Task<BaseResponse<OrderDetailHistoryDto>> GetOrderDetailHistory(OrderDetailHistoryRequest request);
    }

    public class OrderDetailProxy : BaseProxy, IOrderDetailProxy
    {
        public async Task<List<OrderDetailAppDto>> GetAllOrderDetails(OrderDetailAppRequest request)
        {
            try
            {
                var api = RestService.For<IOrderDetailApi>(GetHttpClient());
                var result = await api.GetAllOrderDetails(request);
                if (result == null) return new List<OrderDetailAppDto>();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public async Task<OrderDetailAppDto> AddOrderDetail(OrderDetailAppRequest request)
        {
            try
            {
                var api = RestService.For<IOrderDetailApi>(GetHttpClient());
                var result = await api.AddOrderDetail(request);
                if (result == null) return new OrderDetailAppDto();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new OrderDetailAppDto();
            }
        }

        public async Task<BaseResponse<OrderDetailHistoryDto>> GetOrderDetailHistory(OrderDetailHistoryRequest request)
        {
            try
            {
                var api = RestService.For<IOrderDetailApi>(GetHttpClient());
                return await api.GetOrderDetailHistory(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public interface IOrderDetailApi
        {
            [Post("/orderDetail/GetAllOrderDetails")]
            Task<List<OrderDetailAppDto>> GetAllOrderDetails([Body] OrderDetailAppRequest request);

            [Post("/orderDetail/AddOrderDetail")]
            Task<OrderDetailAppDto> AddOrderDetail([Body] OrderDetailAppRequest request);

            [Post("/orderDetail/GetOrderDetailHistory")]
            Task<BaseResponse<OrderDetailHistoryDto>> GetOrderDetailHistory([Body] OrderDetailHistoryRequest request);
        }
    }
}