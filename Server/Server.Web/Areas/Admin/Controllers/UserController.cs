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
    }
}