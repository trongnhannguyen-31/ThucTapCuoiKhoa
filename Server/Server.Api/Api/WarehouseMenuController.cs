using Phoenix.Server.Services.MainServices;
using Phoenix.Shared.Common;
using Phoenix.Shared.Warehouse;
using Phoenix.Shared.WarehouseMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/warehousemenu")]
    public class WarehouseMenuController : BaseApiController
    {
        private readonly IWarehouseMenuService _warehouseMenuService;
        public WarehouseMenuController(IWarehouseMenuService warehouseMenuService)
        {
            _warehouseMenuService = warehouseMenuService;
        }

        [HttpPost]
        [Route("GetAllWarehouseMenus")]
        public async Task<BaseResponse<WarehouseMenuDto>> GetAllWarehouseMenus(WarehouseMenuRequest request)
        {
            return await _warehouseMenuService.GetAllWarehouseMenus(request);
        }
    }
}