using Phoenix.Server.Services.Constants;
using Phoenix.Server.Services.Framework;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Admin/Dashboard
        private readonly PermissionService _permissionService;
        public DashboardController(PermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        public ActionResult Index()
        {
            if (_permissionService.UnAuthorize(ServerPermissions.AccessAdminPanel))
            {
                return AccessDeniedView();
            }
            return View();
        }
    }
}