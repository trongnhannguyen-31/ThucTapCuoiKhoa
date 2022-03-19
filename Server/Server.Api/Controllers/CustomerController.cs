using System.Threading.Tasks;
using System.Web.Http;
using Queenland.Server.Api.Api;
using Queenland.Server.Services.MainServices.Auth;
using Queenland.Server.Services.MainServices.Customer;
using Queenland.Shared.Auth;
using Queenland.Shared.Core;
using Queenland.Shared.Customer;

namespace Queenland.Server.Api.Controllers
{
    [RoutePrefix("api/customer")]
    public class CustomerController : BaseApiController
    {
        private readonly CustomerService _customerService;
        private readonly UserAuthService _userAuthService;

        public CustomerController(CustomerService customerService, UserAuthService userAuthService)
        {
            _customerService = customerService;
            _userAuthService = userAuthService;
        }

        [HttpGet]
        [Route("validatecustomerphone")]
        public async Task<CustomerMembershipDto> ValidateCustomerPhone(string phone) => await _customerService.ValidateCustomerPhone(phone);

        [HttpPost]
        [Route("register")]
        public async Task<CrudResult> Register(RegisterCustomerRequest request)
        {
            return await _customerService.Register(request);
        }
    }
}