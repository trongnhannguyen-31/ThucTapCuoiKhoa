using Phoenix.Server.Services.MainServices;
using Phoenix.Shared.Common;
using Phoenix.Shared.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/warehouse")]
    public class WarehouseController : BaseApiController
    {
        private readonly IWarehouseService _warehouseService;
        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpPost]
        [Route("GetAllWarehouses")]
        public async Task<BaseResponse<WarehouseDto>> GetAllWarehouses(WarehouseRequest request)
        {
            return await _warehouseService.GetAllWarehouses(request);
        }
    }
}