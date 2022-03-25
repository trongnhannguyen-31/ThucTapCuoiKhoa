using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.WarehouseMenu;
using Phoenix.Shared.WarehouseMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class WarehouseMenuController : BaseController
    {
        // GET: Admin/WarehouseMenu

        private readonly IWarehouseMenuService _warehouseMenuService;
        public WarehouseMenuController(IWarehouseMenuService warehouseMenuService)
        {
            _warehouseMenuService = warehouseMenuService;
        }

        public ActionResult Index()
        {
            return View();
        }

        // List Warehouse
        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, WarehouseMenuModel model)
        {
            var warehouseMenus = await _warehouseMenuService.GetAllWarehouseMenus(new WarehouseMenuRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
            });

            var gridModel = new DataSourceResult
            {
                Data = warehouseMenus.Data,
                Total = warehouseMenus.DataCount
            };
            return Json(gridModel);
        }
    }
}