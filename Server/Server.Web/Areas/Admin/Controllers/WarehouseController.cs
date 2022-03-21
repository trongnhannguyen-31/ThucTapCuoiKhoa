using Falcon.Web.Core.Helpers;
using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Warehouse;
using Phoenix.Shared.Warehouse;
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
        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, WarehouseModel model)
        {
            var warehouses = await _warehouseService.GetAllWarehouses(new WarehouseRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                Id = model.Id,
                Quantity = model.Quantity,
                //ProductSKU_Id = model.ProductSKU_Id
            });

            var gridModel = new DataSourceResult
            {
                Data = warehouses.Data,
                Total = warehouses.DataCount
            };
            return Json(gridModel);
        }

        // Them
        public ActionResult Create()
        {
            DataContext db = new DataContext();
            ViewBag.ProductSKU_Id = new SelectList(db.ProductSKUs.OrderBy(n => n.Id), "Id", "Product_Id");

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
    }
}