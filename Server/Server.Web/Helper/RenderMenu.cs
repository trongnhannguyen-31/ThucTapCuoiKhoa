using System.Linq;
using System.Web.Mvc;
using Falcon.Web.Core.Infrastructure;
using Falcon.Web.Mvc.Security;
using Phoenix.Server.Services.MainServices.Auth;

namespace Phoenix.Server.Web.Helper
{
    public static class RenderMenu
    {
        public static string IsHidden(this HtmlHelper html, string permission)
        {
            var stringPermission = permission.Split(',');
            if (!stringPermission.Any()) return "";

            var userAuthService = EngineContext.Current.Resolve<UserAuthService>();
            var claim = userAuthService.GetClaimByUserId(EngineContext.Current.Resolve<WebContext>().UserId);
            return claim.Any(d => stringPermission.Contains(d)) ? "" : "hidden";
        }
    }
}