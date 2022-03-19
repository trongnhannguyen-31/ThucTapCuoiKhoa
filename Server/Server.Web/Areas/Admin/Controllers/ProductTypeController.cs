using Falcon.Web.Core.Helpers;
using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.ProductType;
using Phoenix.Shared.ProductType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class ProductTypeController : BaseController
    {
        // GET: Admin/ProductType

        private readonly IProductTypeService _productTypeService;
        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, ProductTypeModel model)
        {
            var productTypes = await _productTypeService.GetAllProductTypes(new ProductTypeRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                Name = model.Name
            });

            var gridModel = new DataSourceResult
            {
                Data = productTypes.Data.Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    CreatedAt = s.CreatedAt.ToString()
                }),
                Total = productTypes.DataCount
            };
            return Json(gridModel);
        }

        // Create ProductType
        public ActionResult Create()
        {            
            var model = new ProductTypeModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductTypeModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var productTypes = await _productTypeService.CreateProductTypes(new ProductTypeRequest
            {
                Name = model.Name,
            });
            if (!productTypes.success)
            {
                ErrorNotification("Thêm mới không thành công");
                return View(model);
            }
            SuccessNotification("Thêm mới thành công");
            return RedirectToAction("Create");
        }

        // Update ProductType
        /*public ActionResult Update(int id)
        {
            var projectDto = _productTypeService.GetProductTypesById(id);
            if (projectDto == null)
            {
                return RedirectToAction("List");
            }

            var projectModel = projectDto.MapTo<ProductTypeModel>();
            return View(projectModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ProductTypeModel model)
        {
            var project = _productTypeService.GetProductTypeById(model.Id);
            if (project == null)
                return RedirectToAction("List");
            if (!ModelState.IsValid)
                return View(model);
            await _productTypeService.Update(model.MapTo<ProductTypeDto>());
            SuccessNotification("Chỉnh sửa thông tin chương trình thành công");
            return RedirectToAction("Update", new { id = model.Id });
        }*/

        // Delete ProductType
        /*[HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var project = _productTypeService.GetProductTypeById(id);
            if (project == null)
                //No email account found with the specified id
                return RedirectToAction("List");

            await _productTypeService.Delete(project.Id);
            SuccessNotification("Xóa đại lý thành công");
            return RedirectToAction("List");
        }*/
    }
}