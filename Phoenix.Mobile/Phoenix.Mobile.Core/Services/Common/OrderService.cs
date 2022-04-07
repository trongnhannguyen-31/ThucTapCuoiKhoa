using Phoenix.Mobile.Core.Models.Order;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IOrderService
    {
        Task<List<OrderModel>> GetAllAppOrders(OrderRequest request);
        Task<OrderModel> AddOrder(OrderRequest request);
        Task<OrderModel> GetLatestOrder(OrderRequest request);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderProxy _orderProxy;
        public OrderService(IOrderProxy orderProxy)
        {
            _orderProxy = orderProxy;
        }
        public async Task<List<OrderModel>> GetAllAppOrders(OrderRequest request)
        {
            var productMenu = await _orderProxy.GetAllAppOrders(request);
            return productMenu.Data.MapTo<OrderModel>();
        }

        public async Task<OrderModel> AddOrder(OrderRequest request)
        {
            var data = await _orderProxy.AddOrder(request);            
            return data.MapTo<OrderModel>();
        }

        public async Task<OrderModel> GetLatestOrder(OrderRequest request)
        {
            var data = await _orderProxy.GetLatestOrder(request);
            return data.Record.MapTo<OrderModel>();
        }
    }
}
