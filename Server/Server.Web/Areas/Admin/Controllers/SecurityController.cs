using System.Web.Mvc;
using Falcon.Core;
using Falcon.Core.Domain.Users;
using Falcon.Web.Core.Log;
using Falcon.Web.Mvc.Security;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class SecurityController : BaseController
    {
        private readonly ILogger _logger;
        private readonly WebContext _webContext;
        public SecurityController(ILogger logger, WebContext webContext)//, IWorkContext workContext)
        {
            _logger = logger;
            //_workContext = workContext;
            _webContext = webContext;
        }
        public ActionResult AccessDenied(string pageUrl)
        {
            var currentFalconUser = _webContext.UserId;
            if (currentFalconUser == 0)
            {
                _logger.Info(string.Format("Access denied to anonymous request on {0}", pageUrl));
                return View();
            }

            //_logger.Info(string.Format("Access denied to user #{0} '{1}' on {2}", currentFalconUser.Email, currentFalconUser.Email, pageUrl));


            return View();
        }
    }
}