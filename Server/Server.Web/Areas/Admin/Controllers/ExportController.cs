using System;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using Phoenix.Server.Services.Export;
using Falcon.Core;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class ExportController : BaseController
    {
        private readonly ExportService _exportService;
        private readonly ExportManager _exportManager;
        public ExportController(ExportService exportService, ExportManager exportManager)
        {
            _exportService = exportService;
            _exportManager = exportManager;
        }
        [HttpPost]
        public ActionResult ExportCustomerToExcel(CustomerModel model)
        {
            try
            {
                var customerType = model.CustomerTypeId ?? 0;
                ListCustomerDtoResponse customers = _exportService.PrepareCustomerDataToExportExcel(model.Phone, model.Name, model.Code,
                customerType, DateTime.Parse(model.DateFrom, new CultureInfo("vi-VN")),
                DateTime.Parse(model.DateTo, new CultureInfo("vi-VN")), model.AllData);
                byte[] bytes;
                string templateName = System.Web.HttpContext.Current.Server.MapPath("~/Templates/Template_ExportCustomer.xlsx");
                using (MemoryStream stream = new MemoryStream())
                {
                    _exportManager.ExportCustomerToXlsx(stream, templateName, customers);
                    bytes = stream.ToArray();
                }
                DateTime now = DateTime.Now;
                string nameFileExport = $"DanhSachKhachHang_{DateTime.Now.Ticks}.xlsx";

                string relPath = "/" + Path.Combine("Templates", now.Year.ToString() + now.Month.ToString()).Replace('\\', '/');
                string fullPath = CommonHelper.MapPath(relPath);
                Directory.CreateDirectory(fullPath);
                System.IO.File.WriteAllBytes(Path.Combine(fullPath, nameFileExport), bytes);
                return Json(new { Result = true, FileName = nameFileExport });
            }
            catch (Exception exc)
            {
                ErrorNotification(exc.Message);
                return Json(new { Result = false });
            }
        }

    }
}