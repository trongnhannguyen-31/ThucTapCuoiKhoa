using Falcon.Web.Core.Helpers;
using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Product;
using Phoenix.Shared.Product;
using Phoenix.Shared.ProductSKU;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        private readonly IProductService _productService;
        private readonly IProductSKUService _productSKUService;
        public ProductController(IProductService productService, IProductSKUService productSKUService)
        {
            _productService = productService;
            _productSKUService = productSKUService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, ProductModel model)
        {
            var products = await _productService.GetAllProducts(new ProductRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                Name = model.Name
            });

            var gridModel = new DataSourceResult
            {
                Data = products.Data,
                Total = products.DataCount
            };
            return Json(gridModel);
        }


        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                string filePath = Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath("~/UploadImage"), filePath));
                //Here you can write code for save this information in your database if you want
            }

            return Json("file uploaded successfully");
        }

        #region
        // Create Product
        public ActionResult Create()
        {
            var model = new ProductModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var res = await _productService.CreateProducts(new ProductRequest
            {
                Vendor_Id = model.Vendor_Id,
                ProductType_Id = model.ProductType_Id,
                Name = model.Name,
                ModelCode = model.ModelCode,
            });

            if (!res.Success)
            {
                ErrorNotification("Thêm mới không thành công");
                return View(model);
            }
            SuccessNotification("Thêm mới đại lý thành công");
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public ActionResult Update(int id)
        {
            var projectDto = _productService.GetProductsById(id);
            if (projectDto == null)
            {
                return RedirectToAction("List");
            }

            var projectModel = projectDto.MapTo<ProductModel>();
            return View(projectModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ProductModel model)
        {
            var project = _productService.GetProductsById(model.Id);
            if (project == null)
                return RedirectToAction("List");
            if (!ModelState.IsValid)
                return View(model);
            var products = await _productService.UpdateProducts(new ProductRequest
            {
                Id = model.Id,
                Vendor_Id = model.Vendor_Id,
                ProductType_Id = model.ProductType_Id,
                Name = model.Name,
                ModelCode = model.ModelCode,
                Image1 = model.Image1,
                Image2 = model.Image2,
                Image3 = model.Image3,
                Image4 = model.Image4,
                Image5 = model.Image5,
            });
            SuccessNotification("Chỉnh sửa thông tin chương trình thành công");
            return RedirectToAction("Update", new { id = model.Id });
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var project = _productService.GetProductsById(id);
            if (project == null)
                //No email account found with the specified id
                return RedirectToAction("List");

            await _productService.DeleteProducts(project.Id);
            SuccessNotification("Xóa đại lý thành công");
            return RedirectToAction("Index");
        }
        #endregion
    }
}