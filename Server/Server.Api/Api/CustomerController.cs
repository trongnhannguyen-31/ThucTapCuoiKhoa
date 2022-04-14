using Phoenix.Server.Services.MainServices;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Customer;
using Phoenix.Shared.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/customer")]
    public class CustomerController : BaseApiController
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [Route("GetCustomerApptById")]
        public async Task<BaseResponse<CustomerDto>> GetCustomerApptById(CustomerRequest request)
        {
            return await _customerService.GetCustomerApptById(request);
        }

        [HttpPost]
        [Route("UpdateCustomerDetail")]
        public Task<CrudResult> UpdateCustomerDetail(int Id, [FromBody] CustomerRequest request)
        {
            return _customerService.UpdateCustomerDetail(Id, request);
        }

        [HttpPost]
        [Route("AddCustomerDetail")]
        public Task<CrudResult> AddCustomerDetail([FromBody] CustomerRequest request)

        {
            return _customerService.AddCustomerDetail(request);
        }
    }
}