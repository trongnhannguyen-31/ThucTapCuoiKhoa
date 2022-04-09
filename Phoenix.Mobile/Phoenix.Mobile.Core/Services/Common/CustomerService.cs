using Phoenix.Mobile.Core.Models.Customer;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface ICustomerService
    {
        Task<CustomerModel> GetCustomerApptById(CustomerRequest request);
    }

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerProxy _customerProxy;
        public CustomerService(ICustomerProxy customerProxy)
        {
            _customerProxy = customerProxy;
        }

        public async Task<CustomerModel> GetCustomerApptById(CustomerRequest request)
        {
            var customer = await _customerProxy.GetCustomerApptById(request);
            return customer.Record.MapTo<CustomerModel>();
        }
    }
}
