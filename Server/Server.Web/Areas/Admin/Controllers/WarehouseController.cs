using Falcon.Web.Core.Helpers;
using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Warehouse;
using Phoenix.Server.Web.Areas.Admin.Models.WarehouseMenu;
using Phoenix.Shared.Warehouse;
using Phoenix.Shared.WarehouseMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class WarehouseController : BaseController
    {
        // GET: Admin/Warehouse
        private readonly IWarehouseService _warehouseService;
        private readonly IWarehouseMenuService _warehouseMenuService;

        public WarehouseController(IWarehouseService warehouseService, IWarehouseMenuService warehouseMenuService)
        {
            _warehouseService = warehouseService;
            _warehouseMenuService = warehouseMenuService;
        }

        /*public ActionResult Index()
        {
            return View();
        }

        // List Warehouse
        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, WarehouseModel model)
        {
            var warehouseMenus = await _warehouseMenuService.GetAllWarehouseMenus(new WarehouseMenuRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                *//*Id = model.Id,
                Quantity = model.Quantity,
                ProductSKU_Id = model.ProductSKU_Id*//*
            });

            var gridModel = new DataSourceResult
            {
                Data = warehouseMenus.Data,
                Total = warehouseMenus.DataCount
            };
            return Json(gridModel);
        }*/

        // Create Warehouse
        public ActionResult Create(int id)
        {

            var model = new WarehouseModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(WarehouseModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var res = await _warehouseService.CreateWarehouses(new WarehouseRequest
            {
                ProductSKU_Id = model.ProductSKU_Id,
                Quantity = model.Quantity
            });

            if (!res.Success)
            {
                ErrorNotification("Thêm mới không thành công");
                return View(model);
            }
            SuccessNotification("Thêm mới đại lý thành công");
            return RedirectToAction("Create");
        }

        // Update Warehouse
        public ActionResult Update(int id)
        {
            var projectDto = _warehouseService.GetWarehousesById(id);
            if (projectDto == null)
            {
                return RedirectToAction("Index");
            }

            var projectModel = projectDto.MapTo<WarehouseModel>();
            return View(projectModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(WarehouseModel model)
        {
            var project = _warehouseService.GetWarehousesById(model.Id);
            if (project == null)
                return RedirectToAction("Index");
            if (!ModelState.IsValid)
                return View(model);
            var warehouses = await _warehouseService.UpdateWarehouses(new WarehouseRequest
            {
                Id = model.Id,
                ProductSKU_Id = model.ProductSKU_Id,
                Quantity = model.Quantity,
                NewQuantity = model.NewQuantity
            });
            SuccessNotification("Chỉnh sửa thông tin chương trình thành công");
            return RedirectToAction("Update", new { id = model.Id });
        }

        // Delete Warehouse
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var project = _warehouseService.GetWarehousesById(id);
            if (project == null)
                //No email account found with the specified id
                return RedirectToAction("Index");

            await _warehouseService.DeleteWarehousesById(project.Id);
            SuccessNotification("Đã xóa thành công");
            return RedirectToAction("Index");
        }

        #region
        public ActionResult Index(int Id)
        {
            var model = new WarehouseMenuModel();
            model.SKUId = Id;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(DataSourceRequest command, WarehouseMenuModel model)
        {
            var warehouseMenus = await _warehouseMenuService.GetAllWarehouseMenusById(model.SKUId, new WarehouseMenuRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                SKUId = model.SKUId,
            });

            var gridModel = new DataSourceResult
            {
                Data = warehouseMenus.Data,
                Total = warehouseMenus.DataCount
            };
            return Json(gridModel);
        }
        #endregion
    }
}