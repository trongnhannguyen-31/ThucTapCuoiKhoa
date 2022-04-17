using Falcon.Web.Core.Helpers;
using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Product;
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

        #region
        public ActionResult Index(int id)
        {
            var model = new ProductSKUModel();
            model.Id = id;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(DataSourceRequest command, ProductSKUModel model)
        {
            var productSKUs = await _productSKUService.GetAllProductSKUById(model.Id, new ProductSKURequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                Product_Id = model.Product_Id,
                Ram = model.Ram,
            });

            var gridModel = new DataSourceResult
            {
                Data = productSKUs.Data,
                Total = productSKUs.DataCount
            };
            return Json(gridModel);
        }

        public void SetViewBag(long? selected_Id = null)
        {
            DataContext db = new DataContext();

            ViewBag.Product_Id = new SelectList(db.Products.OrderBy(n => n.Name), "Id", "Name", selected_Id);
        }

        #endregion

        // Create ProductSKU
        public ActionResult Create()
        {
            SetViewBag();
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
                Price = model.Price,
                Rating = model.Rating,
                BuyCount = model.BuyCount,
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
                YearOfManufacture = model.YearOfManufacture,
            });
            if (!productSKUs.Success)
            {
                ErrorNotification("Lỗi");
                return View(model);
            }
            SuccessNotification("Thêm mới đại lý thành công");
            return RedirectToAction("Create");
        }

        // Update ProductSKU
        public ActionResult Update(int id)
        {
           
            var projectDto = _productSKUService.GetProductSKUById(id);
            
            DataContext db = new DataContext();

            ViewBag.Product_Id = new SelectList(db.Products.OrderBy(n => n.Name), "Id", "Name", projectDto.Product_Id);

            if (projectDto == null)
            {
                return RedirectToAction("List");
            }

            var projectModel = projectDto.MapTo<ProductSKUModel>();
            return View(projectModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ProductSKUModel model)
        {
            var projetc = _productSKUService.GetProductSKUById(model.Id);

            DataContext db = new DataContext();

            ViewBag.Product_Id = new SelectList(db.Products.OrderBy(n => n.Name), "Id", "Name", projetc.Product_Id);

            if (projetc == null)
                return RedirectToAction("List");
            if (!ModelState.IsValid)
                return View(model);
            var productSKU = await _productSKUService.UpdateProductSKUs(new ProductSKURequest
            {
                Id = model.Id,
                Product_Id = model.Product_Id,
                Price = model.Price,
                // Rating = model.Rating,
                BuyCount = model.BuyCount,
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
                YearOfManufacture = model.YearOfManufacture,
            });
            SuccessNotification("Chỉnh sửa thông tin chương trình thành công");
            return RedirectToAction("Update", new { id = model.Id });
        }


        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var project = _productSKUService.GetProductSKUById(id);
            if (project == null)
                //No email account found with the specified id
                return RedirectToAction("List");

            await _productSKUService.DeleteProductSKUs(project.Id);
            SuccessNotification("Xóa đại lý thành công");
            return RedirectToAction("Index", "Product");
        }
    }
}