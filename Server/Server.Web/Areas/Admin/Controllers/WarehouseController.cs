﻿using Falcon.Web.Core.Helpers;
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

        // List Warehouse
        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, WarehouseModel model)
        {
            var warehouses = await _warehouseService.GetAllWarehouses(new WarehouseRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                Id = model.Id,
                Quantity = model.Quantity,
                ProductSKU_Id = model.ProductSKU_Id
            });

            var gridModel = new DataSourceResult
            {
                Data = warehouses.Data,
                Total = warehouses.DataCount
            };
            return Json(gridModel);
        }

        // Create Warehouse
        public ActionResult Create()
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
                return RedirectToAction("List");
            }

            var projectModel = projectDto.MapTo<WarehouseModel>();
            return View(projectModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(WarehouseModel model)
        {
            var project = _warehouseService.GetWarehousesById(model.Id);
            if (project == null)
                return RedirectToAction("List");
            if (!ModelState.IsValid)
                return View(model);
            var res = await _warehouseService.CreateWarehouses(new WarehouseRequest
            {
                ProductSKU_Id = model.ProductSKU_Id,
                Quantity = model.Quantity
            });
            SuccessNotification("Chỉnh sửa thông tin chương trình thành công");
            return RedirectToAction("Update", new { id = model.Id });
        }
    }
}