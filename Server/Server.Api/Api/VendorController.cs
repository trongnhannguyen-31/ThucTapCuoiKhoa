using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Vendor;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/vendor")]
    public class VendorController : BaseApiController
    {
        private readonly IVendorService _vendorService;
        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpPost]
        [Route("GetAllVendors")]
        public async Task<BaseResponse<VendorDto>> GetAllVendors(VendorRequest request)
        {
            return await _vendorService.GetAllVendors(request);
        }
    }
}