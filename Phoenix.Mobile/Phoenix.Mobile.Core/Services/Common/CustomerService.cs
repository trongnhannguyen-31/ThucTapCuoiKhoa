//using Phoenix.Mobile.Core.Models.Customer;
//using Phoenix.Mobile.Core.Proxies.Common;
//using Phoenix.Mobile.Core.Utils;
//using Phoenix.Shared.Customer;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Phoenix.Mobile.Core.Services.Common
//{
//    public interface ICustomerService
//    {
//        Task<List<CustomerModel>> GetAllCustomers(CustomerRequest request);
//    }

//    public class OrderService : IOrderService
//    {
//        private readonly IOrderProxy _orderProxy;
//        public OrderService(IOrderProxy orderProxy)
//        {
//            _orderProxy = orderProxy;
//        }
//        public async Task<List<OrderModel>> GetAllOrders(OrderRequest request)
//        {
//            var data = await _orderProxy.GetAllOrders(request);
//            return data.MapTo<OrderModel>();
//        }
//    }
//}
