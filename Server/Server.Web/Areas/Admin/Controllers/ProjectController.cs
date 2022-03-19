using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Falcon.Web.Core.Helpers;
using Falcon.Web.Mvc.Kendoui;
using SpotCheck.Shared.Org;
using SpotCheck.Sub.Services.Org;
using SpotCheck.Sub.Services.Org.Request;
using SpotCheck.Sub.Web.Areas.Admin.Models.Project;

namespace SpotCheck.Sub.Web.Areas.Admin.Controllers
{
    public class ProjectController : BaseController
    {
        #region Fields

        private readonly ProjectService _projectService;

        #endregion

        #region Methods

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }
        // GET: Admin/Project
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        #region Project CRUD
        public ActionResult List()
        {
            var model = new ProjectListModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, ProjectListModel model)
        {
            var projects = await _projectService.SearchProject(new SearchProjectRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                Name = model.Name
            });

            var gridModel = new DataSourceResult
            {
                Data = projects.MapTo<ProjectModel>(),
                Total = projects.Count
            };
            return Json(gridModel);
        }

        public ActionResult Create()
        {
            var model = new ProjectModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProjectModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var res = await _projectService.Create(model.MapTo<ProjectDto>());
            if (!res.IsOk)
            {
                ErrorNotification(res.ErrorDescription);
                return View(model);
            }
            SuccessNotification("Thêm mới đại lý thành công");
            return RedirectToAction("Update", new {id = res.ReturnId});
        }

        public ActionResult Update(int id)
        {
            var projectDto = _projectService.GetProjectById(id);
            if (projectDto == null)
            {
                return RedirectToAction("List");
            }

            var projectModel = projectDto.MapTo<ProjectModel>();
            return View(projectModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ProjectModel model)
        {
            var projetc = _projectService.GetProjectById(model.Id);
            if (projetc == null)
                return RedirectToAction("List");
            if (!ModelState.IsValid)
                return View(model);
            await _projectService.Update(model.MapTo<ProjectDto>());
            SuccessNotification("Chỉnh sửa thông tin chương trình thành công");
            return RedirectToAction("Update", new {id = model.Id});
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var project = _projectService.GetProjectById(id);
            if (project == null)
                //No email account found with the specified id
                return RedirectToAction("List");

            await _projectService.Delete(project.Id);
            SuccessNotification("Xóa đại lý thành công");
            return RedirectToAction("List");
        }

        #endregion


        #endregion

    }
}