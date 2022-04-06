using Falcon.Web.Core.Helpers;
using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Vendor;
using Phoenix.Shared.Vendor;
using Phoenix.Server.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Web.Areas.Admin.Models.ImageRecord;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class VendorController : BaseController
    {
        // GET: Admin/Vendor
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, VendorModel model)
        {
            var vendors = await _vendorService.GetAllVendors(new VendorRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                Name = model.Name,
                Phone = model.Phone
            });

            var gridModel = new DataSourceResult
            {
                Data = vendors.Data,
                Total = vendors.DataCount
            };
            return Json(gridModel);
        }

        // Create Vendor
        public ActionResult Create()
        {
            var model = new VendorModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(VendorModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var vendors = await _vendorService.CreateVendors(new VendorRequest
            {
                Name = model.Name,
                Phone = model.Phone,
                Nation = model.Nation,
            });
            if (!vendors.Success)
            {
                ErrorNotification("Thêm mới không thành công");
                return View(model);
            }
            SuccessNotification("Thêm mới thành công");
            return RedirectToAction("Create");
        }

        #region Update
        public ActionResult Update(int id)
        {
            var projectDto = _vendorService.GetVendorsById(id);
            if (projectDto == null)
            {
                return RedirectToAction("List");
            }

            var projectModel = projectDto.MapTo<VendorModel>();
            return View(projectModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(VendorModel model)
        {
            var projetc = _vendorService.GetVendorsById(model.Id);
            if (projetc == null)
                return RedirectToAction("List");
            if (!ModelState.IsValid)
                return View(model);
            var vendors = await _vendorService.UpdateVendors(new VendorRequest
            {
                Id = model.Id,
                Name = model.Name,
                Phone = model.Phone,
                Logo = model.Logo,
                Nation = model.Nation,
                UpdatedAt = model.UpdatedAt,
            });
            SuccessNotification("Chỉnh sửa thông tin chương trình thành công");
            return RedirectToAction("Update", new { id = model.Id });
        }
        #endregion

        #region
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var project = _vendorService.GetVendorsById(id);
            if (project == null)
                //No email account found with the specified id
                return RedirectToAction("List");

            await _vendorService.DeleteVendors(project.Id);
            SuccessNotification("Xóa hãng sản xuất thành công");
            return RedirectToAction("List");
        }
        #endregion










        // Add Hình Ảnh
        /*[Route("Create")]
        [HttpPost]
        public ActionResult Create(ImageRecordModel model)
        {
            HttpPostedFileBase file = Request.Files["ImageLogo"];
            ContentRepository service = new ContentRepository();
            int i = service.UploadImageInDataBase(file, model);
            if (i == 1)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }*/





        /* // Upload hình ảnh
         public class UploadOneFile
         {
             [Required(ErrorMessage = "Phai chon upload file")]
             [DataType(DataType.Upload)]
             [FileExtensions(Extensions = "pgn, jpg, jpeg, gif")]
             [Display(Name = "Chon upload file")]

             public IFromFile FileUpload { get; set; }
         }

         // Upload Image Vendor
         [HttpGet]
         public IActionResult UploadPhoto (int id)
         {
             // Lay ra san pham co id bang
             var vendors = _context.Vendor.Where(e => e.Id == id)
                 .Include(p => p.Photos)
                 .FirstOrDefault();

             if (vendors == null)
             {
                 return NotFound("Khong co hang san xuat");
             }

             ViewData["vendors"] = vendors;

             return View(); // Create View UploadPhoto, VendorModel -> Create Model VendorPhoto
         }

         // Upload hình ảnh
         [HttpPost, ActionName("UploadPhoto")]
         public async Task<IActionResult> UploadPhotoAsync (int id, [Bind("FileUpload")]UploadOneFile f)
         {
             // Lay ra san pham co id bang
             var vendors = _context.Vendor.Where(e => e.Id == id).FirstOrDefault();

             if (vendors == null)
             {
                 return NotFound("Khong co hang san xuat");
             }

             ViewData["vendors"] = vendors;

             if (f != null)
             {
                 var file1 = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                     + Path.GetExtension(f.FileUpload.FileName);

                 var file = Path.Combine("Uploads", "thu muc: Products", file1);

                 using (var filestream = new FileStream(file, FileMode.Create))
                 {
                     await f.FileUpload.CopyToAsync(filestream);
                 }

                 _context.Add(new PhotoProduct() {
                     ProductId = product.ProductId,
                     fileName = file1
                 });

                 await _context.SaveChangesAsync();
             }

             return View(new UploadOneFile());
         }*/


    }
}