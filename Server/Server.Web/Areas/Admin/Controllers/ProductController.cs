using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Product;
using Phoenix.Shared.Product;
using Phoenix.Shared.ProductSKU;
using System;
using System.Collections.Generic;
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
                Model = model.ModelCode,
            });

            if (!res.Success)
            {
                ErrorNotification("Thêm mới không thành công");
                return View(model);
            }
            SuccessNotification("Thêm mới đại lý thành công");
            return RedirectToAction("Index");
        }




    }
}