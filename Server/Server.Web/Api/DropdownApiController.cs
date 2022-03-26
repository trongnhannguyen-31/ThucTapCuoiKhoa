using Phoenix.Server.Services.ApiServices;
using Phoenix.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Phoenix.Server.Web.Api
{
    [RoutePrefix("api/dropdown")]
    public class DropdownApiController : BaseApiController
    {
        private readonly IDropdownService _dropdownService;
        public DropdownApiController(IDropdownService dropdownService)
        {
            _dropdownService = dropdownService;
        }

        [Route("TakeAllProductTypes")]
        [HttpPost]
        public async Task<List<DropdownDto>> TakeAllProductTypes() => await _dropdownService.TakeAllProductTypes();

        [Route("TakeAllVendors")]
        [HttpPost]
        public async Task<List<DropdownDto>> TakeAllVendors() => await _dropdownService.TakeAllVendors();

        [Route("TakeAllProductSKUs")]
        [HttpPost]
        public async Task<List<DropdownDto>> TakeAllProductSKUs() => await _dropdownService.TakeAllProductSKUs();

        [Route("TakeAllProducts")]
        [HttpPost]
        public async Task<List<DropdownDto>> TakeAllProducts() => await _dropdownService.TakeAllProducts();

        [Route("TakeAllWarehouseMenus")]
        [HttpPost]
        public async Task<List<DropdownDto>> TakeAllWarehouseMenus() => await _dropdownService.TakeAllWarehouseMenus();

    }
}