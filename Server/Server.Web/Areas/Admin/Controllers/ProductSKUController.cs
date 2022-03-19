using Falcon.Web.Core.Helpers;
using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.ProductSKU;
using Phoenix.Shared.ProductSKU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class ProductSKUController : BaseController
    {
        // GET: Admin/ProductSKU

        private readonly IProductSKUService _productSKUService;
        public ProductSKUController(IProductSKUService productSKUService)
        {
            _productSKUService = productSKUService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, ProductSKUModel model)
        {
            var productSKUs = await _productSKUService.GetAllProductSKUs(new ProductSKURequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                Product_Id = model.Product_Id,
            });

            var gridModel = new DataSourceResult
            {
                Data = productSKUs.Data,
                Total = productSKUs.DataCount
            };
            return Json(gridModel);
        }

        public ActionResult Create()
        {
            DataContext db = new DataContext();
            ViewBag.Product_Id = new SelectList(db.Products.OrderBy(n => n.Name), "Id", "Name");

            var model = new ProductSKUModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductSKUModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var productSKUs = await _productSKUService.CreateProductSKUs(new ProductSKURequest
            {
                Product_Id = model.Product_Id,
                Screen = model.Screen,
                OperationSystem = model.OperationSystem,
                Processor = model.Processor,
                Ram = model.Ram,
                Storage = model.Storage,
                Battery = model.Battery,
                BackCamera = model.BackCamera,
                FrontCamera = model.FrontCamera,
                SimSlot = model.SimSlot,
                GraphicCard = model.GraphicCard,
                ConnectionPort = model.ConnectionPort,
                Design = model.Design,
                Size = model.Size,
                Special = model.Special,
            });
            if (!productSKUs.success)
            {
                ErrorNotification("Lỗi");
                return View(model);
            }
            SuccessNotification("Thêm mới đại lý thành công");
            return RedirectToAction("Create");
        }
    }
}