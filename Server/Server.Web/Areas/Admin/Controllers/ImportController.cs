using System.Linq;
using System.Web.Mvc;
using Falcon.Web.Mvc.Kendoui;
using System;
using System.IO;
using Phoenix.Server.Web.Areas.Admin.Models.Import;
using Phoenix.Server.Services.Import;
using Phoenix.Shared.Common;
using Phoenix.Server.Services.Framework;
using Phoenix.Server.Services.Constants;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class ImportController : BaseController
    {
        private readonly ImportExcelManager _importExcelManager;
        private readonly PermissionService _permissionService;
        public ImportController(ImportExcelManager importExcelManager, PermissionService permissionService)
        {
            _importExcelManager = importExcelManager;
            _permissionService = permissionService;
        }
        #region [Download Excel Template]
        [HttpPost]
        public ActionResult DownloadTemplateAsExcel(ImportExcelModel model)
        {
            try
            {
                string templateName = LookupTemplate(model.FileName);
                if(string.IsNullOrEmpty(templateName))
                {
                    Response.StatusCode = 500;
                    return Json(new { Error = "Không tìm thấy file"});
                }    
                return Json(new { fileName = templateName });
            }
            catch (Exception e)
            {
                return Json(new { Error = e.Message });
            }
        }
        private string LookupTemplate(string templateName)
        {
            string fileName = string.Empty;
            switch(templateName)
            {
                case "ImportMemberCard": return TemplatesCollection.ImportMemberCard;
                case "ImportRenewCode": return TemplatesCollection.ImportRenewCode;
                case "ImportHospital": return TemplatesCollection.ImportHospital;
                case "ImportPromotion": return TemplatesCollection.ImportPromotion;
            }    
            return fileName;
        }
        #endregion

        #region Card
        public ActionResult Card()
        {
            if (_permissionService.UnAuthorize(ServerPermissions.ManageCard))
            {
                return AccessDeniedView();
            }
            return View();
        }
        [HttpPost]
        public ActionResult Card(ImportExcelModel model)
        {
            var file = Request.Files["excel"];
            if(file != null && file.ContentLength > 0)
            {
                if (Path.GetExtension(file.FileName) != ".xlsx")
                {
                    Response.StatusCode = 500;
                    return Json(new { Error = "Tập tin excel không đúng định dạng xlsx. Vui lòng chọn tập tin khác" });
                }
                var data = _importExcelManager.ImportMemberCard(file.InputStream);
                var gridModel = new DataSourceResult
                {
                    Data = data,
                    Total = data.Count
                };
                if (model.SaveToDb)
                {
                    if (data.Count(s => !s.IsValid) == 0)
                    {
                        var save = _importExcelManager.ImportMemberCard(file.InputStream, false);
                        return Json(new { Message = "Upload thành công!", Code = "Success" });
                    }
                    else
                    {
                        Response.StatusCode = 500;
                        return Json(new { Error = "Tập tin excel còn data chưa hợp lệ!" });
                    }
                }
                return Json(gridModel);
            }
            Response.StatusCode = 500;
            return Json(new { Error = "Tập tin excel không đúng định dạng xlsx. Vui lòng chọn tập tin khác" });
        }
        #endregion
      
    }
}