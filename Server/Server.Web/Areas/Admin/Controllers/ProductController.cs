using Falcon.Web.Core.Helpers;
using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Product;
using Phoenix.Server.Data.Entity;
using Phoenix.Shared.Product;
using Phoenix.Shared.ProductSKU;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Phoenix.Shared.Vendor;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        private readonly IProductService _productService;
        private readonly IProductSKUService _productSKUService;
        private readonly IVendorService _vendorService;
        public ProductController(IProductService productService, IProductSKUService productSKUService, IVendorService vendorService)
        {
            _productService = productService;
            _productSKUService = productSKUService;
            _vendorService = vendorService;
        }

        public async Task SetViewBag(long? selected_Id = null)    
        {
            DataContext db = new DataContext();
            var query = db.Vendors.AsQueryable();
            var data = await query.Where(d => d.Deleted == false).ToListAsync();
            // var data1 = data.MapTo<VendorDto>();
            //ViewBag.Vendor_Id = new SelectList(db.Vendors., "Id", "Name", selected_Id);
            //ViewBag.ProductType_Id = new SelectList(db.ProductTypes.OrderBy(n => n.Name), "Id", "Name", selected_Id);'
            ViewBag.ProductType_Id = new SelectList(db.ProductTypes.OrderBy(n => n.Name), "Id", "Name");
            ViewBag.Vendor_Id = new SelectList(data, "Id", "Name", selected_Id);
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
            DataContext db = new DataContext();
            var query = db.Vendors.AsQueryable();
            var data = await query.Where(d => d.Deleted == false).ToListAsync();
            // var data1 = data.MapTo<VendorDto>();
            //ViewBag.Vendor_Id = new SelectList(db.Vendors., "Id", "Name", selected_Id);
            //ViewBag.ProductType_Id = new SelectList(db.ProductTypes.OrderBy(n => n.Name), "Id", "Name", selected_Id);'
            ViewBag.ProductType_Id = new SelectList(db.ProductTypes.OrderBy(n => n.Name), "Id", "Name");
            ViewBag.Vendor_Id = new SelectList(data, "Id", "Name");

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
            //SetViewBag();
            var model = new ProductModel();
            DataContext db = new DataContext();

            var projectDto = _productService.GetProductsById(id);
            ViewBag.ProductType_Id = new SelectList(db.ProductTypes.OrderBy(n => n.Name), "Id", "Name", projectDto.ProductType_Id);
            ViewBag.Vendor_Id = new SelectList(db.Vendors.OrderBy(n => n.Name), "Id", "Name", projectDto.Vendor_Id);

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

        #region Upload
        public static System.Drawing.Image ResizeMyImage(System.Drawing.Image image, int maxHeight)
        {
            var ratio = (double)maxHeight / image.Height;
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string dirFullPath = HttpContext.Server.MapPath("~/UploadImage/");

            //string dirFullPath = HttpContext.Current.Server.MapPath("~/MediaUploader/");
            string[] files;
            int numFiles;
            files = System.IO.Directory.GetFiles(dirFullPath);
            numFiles = files.Length;
            numFiles = numFiles + 1;

            string str_image = "";

            foreach (string s in context.Request.Files)
            {
                HttpPostedFile file = context.Request.Files[s];
                string fileName = file.FileName;
                string fileExtension = file.ContentType;

                if (!string.IsNullOrEmpty(fileName))
                {
                    fileExtension = Path.GetExtension(fileName);
                    str_image = "MyPHOTO_" + numFiles.ToString() + fileExtension;
                    string pathToSave = HttpContext.Server.MapPath("~/UploadImage/") + str_image;
                    //string pathToSave = HttpContext.Current.Server.MapPath("~/MediaUploader/") + str_image;
                    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(file.InputStream);

                    //ResizeMyImage method call
                    System.Drawing.Image objImage = ResizeMyImage(bmpPostedImage, 200);
                    objImage.Save(pathToSave, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            context.Response.Write(str_image);
        }
        #endregion

        public ActionResult AddImages(int id)
        {
            var model = new ProductModel();
            model.Id = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult UploadProductImages(int id, HttpPostedFileBase file)
        {
            return AddImages(id);
        }
    }
}