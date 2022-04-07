using Phoenix.Mobile.Core.Models.OrderDetail;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.OrderDetail;
using Phoenix.Shared.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IOrderDetailService
    {
        Task<List<OrderDetailModel>> GetAllOrderDetails(OrderDetailRequest request);
        Task<OrderDetailModel> AddOrderDetail(OrderDetailRequest request);
        Task<List<OrderDetailHistoryModel>> GetOrderDetailHistory(OrderDetailHistoryRequest request);
    }

    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailProxy _orderDetailProxy;
        public OrderDetailService(IOrderDetailProxy orderDetailProxy)
        {
            _orderDetailProxy = orderDetailProxy;
        }
        public async Task<List<OrderDetailModel>> GetAllOrderDetails(OrderDetailRequest request)
        {
            var data = await _orderDetailProxy.GetAllOrderDetails(request);
            return data.MapTo<OrderDetailModel>();
        }

        public async Task<OrderDetailModel> AddOrderDetail(OrderDetailRequest request)
        {
            var data = await _orderDetailProxy.AddOrderDetail(request);
            return data.MapTo<OrderDetailModel>();
        }

        public async Task<List<OrderDetailHistoryModel>> GetOrderDetailHistory(OrderDetailHistoryRequest request)
        {
            var data = await _orderDetailProxy.GetOrderDetailHistory(request);
            return data.Data.MapTo<OrderDetailHistoryModel>();
        }
    }
}
