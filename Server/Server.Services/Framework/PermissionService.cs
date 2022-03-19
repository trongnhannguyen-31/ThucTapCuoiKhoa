using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices.Auth;
using Falcon.Web.Mvc.Security;
using System.Linq;

namespace Phoenix.Server.Services.Framework
{
    public class PermissionService
    {
        private readonly WebContext _webContext;
        private readonly UserAuthService _userAuthService;
        private readonly DataContext _dataContext;
        public PermissionService(WebContext webContext, UserAuthService userAuthService, DataContext dataContext)
        {
            _webContext = webContext;
            _userAuthService = userAuthService;
            _dataContext = dataContext;
        }
        public string CurrentUserName => _dataContext.Users.Find(_webContext.UserId)?.UserName;
        public bool UnAuthorize(string permission)
        {
            //false is access deny
            var roles = _userAuthService.GetClaimByUserId(_webContext.UserId);
            return !roles.Contains(permission);
        }
        public bool UnAuthExcel(string permission)
        {
            var roles = _userAuthService.GetClaimById(_webContext.UserId);
            var check = roles.Where(s => s == permission);
            return check.Any();
        }
    }
}
