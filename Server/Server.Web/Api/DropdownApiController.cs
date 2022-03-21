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

        [Route("TakeAllWarehouses")]
        [HttpPost]
        public async Task<List<DropdownDto>> TakeAllWarehouses() => await _dropdownService.TakeAllWarehouses();
    }
}