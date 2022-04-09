using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Customer;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface ICustomerProxy
    {
        Task<BaseResponse<CustomerDto>> GetCustomerApptById(CustomerRequest request);
        Task<CrudResult> UpdateCustomerDetail(int Id, CustomerRequest request);
    }

    public class CustomerProxy : BaseProxy, ICustomerProxy
    {
        public async Task<BaseResponse<CustomerDto>> GetCustomerApptById(CustomerRequest request)
        {
            try
            {
                var api = RestService.For<ICustomerApi>(GetHttpClient());
                return await api.GetCustomerApptById(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public async Task<CrudResult> UpdateCustomerDetail(int Id, CustomerRequest request)
        {
            try
            {
                var api = RestService.For<ICustomerApi>(GetHttpClient());
                var result = await api.UpdateCustomerDetail(Id, request);
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CrudResult();
            }
        }

        public interface ICustomerApi
        {
            [Post("/customer/GetCustomerApptById")]
            Task<BaseResponse<CustomerDto>> GetCustomerApptById([Body] CustomerRequest request);

            [Post("/customer/UpdateCustomerDetail")]
            Task<CrudResult> UpdateCustomerDetail(int Id, [Body] CustomerRequest request);
        }
    }
}
