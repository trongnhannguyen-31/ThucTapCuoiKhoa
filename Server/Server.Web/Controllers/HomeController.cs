using System.Web.Mvc;
using Falcon.Web.Mvc.Security;

namespace Phoenix.Server.Web.Controllers
{
	[AllowAnonymous]
	public class HomeController : Controller
    {
  
		public HomeController()
        {         
	       
        }

        public ActionResult Index()
        {
            return Redirect("~/admin");
        }
        [AllowAnonymous]
        [Route("privacy")]
        public ActionResult Privacy()
        {
            return View();
        }
    }
}
