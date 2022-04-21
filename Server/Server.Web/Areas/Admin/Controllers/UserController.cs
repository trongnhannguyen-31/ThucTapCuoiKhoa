using Falcon.Web.Core.Helpers;
using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.MainServices.Users;
using Phoenix.Server.Web.Areas.Admin.Models.Account;
using Phoenix.Shared.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, UserModel model)
        {
            var users = await _userService.GetAllUsers(new UserRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                UserName = model.UserName,
                DisplayName = model.DisplayName,
            });

            var gridModel = new DataSourceResult
            {
                Data = users.Data,
                Total = users.DataCount
            };
            return Json(gridModel);
        }

        // Create User
        public ActionResult Create()
        {
            var model = new UserModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var usersAdmin = await _userService.CreateUsersAdmin(new UserRequest
            {
                UserName = model.UserName,
                DisplayName = model.DisplayName,
                Password = model.Password,
                Salt = model.Salt
            });
            if (!usersAdmin.Success)
            {
                ErrorNotification("Thêm mới không thành công");
                return View(model);
            }
            SuccessNotification("Thêm mới thành công");
            return RedirectToAction("Create");
        }

        #region Delete
        public ActionResult Update(int id)
        {
            var projectDto = _userService.GetUserById(id);
            if (projectDto == null)
            {
                return RedirectToAction("Index");
            }

            var projectModel = projectDto.MapTo<UserModel>();
            return View(projectModel);
        }

        // Delete Rating
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var project = _userService.GetUserById(id);
            if (project == null)
                //No email account found with the specified id
                return RedirectToAction("Index");

            await _userService.DeleteUserById(project.Id);
            SuccessNotification("Xóa tài khoản thành công");
            return RedirectToAction("Index");
        }
        #endregion
    }
}