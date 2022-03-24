using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Product;
using Phoenix.Shared.Product;
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
        public ProductController(IProductService productService)
        {
            _productService = productService;
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

        public void SetViewBag(long? selectedId = null)
        {
            DataContext db = new DataContext();
            ViewBag.ProductType_Id = new SelectList(db.ProductTypes.OrderBy(n => n.Name), "Id", "Name", selectedId);
            ViewBag.Vendor_Id = new SelectList(db.Vendors.OrderBy(n => n.Name), "Id", "Name", selectedId);
        }

        // Create Product
        public ActionResult Create()
        {
            SetViewBag();
            var model = new ProductModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductModel model)
        {
            SetViewBag();
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

        /*// Thêm sản phẩm
        public ActionResult Create()
        {
            //DataContext db = new DataContext();
            //ViewBag.ProductSKU_Id = new SelectList(db.ProductSKUs.OrderBy(n => n.Id), "Id", "Product_Id");

            var model = new ProductModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var res = await _productService.CreateWarehouses(new ProductRequest
            {
                Name = model.Name,


                ProductSKU_Id = model.ProductSKU_Id,
                Quantity = model.Q
            });

            if (!res.success)
            {
                ErrorNotification("Thêm mới không thành công");
                return View(model);
            }
            SuccessNotification("Thêm mới đại lý thành công");
            return RedirectToAction("Create");
        }*/
    }
}