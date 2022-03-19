using System.Threading.Tasks;
using System.Web.Mvc;

namespace Phoenix.Server.Api.Controllers
{
    public class HomeController:Controller
    {
       
        // GET: Home
        public async Task<ActionResult> Index()
        {
            //using (AsyncScopedLifestyle.BeginScope(SimpleContainer.Container))
            //{
            //    var lookupService = EngineContext.Current.Resolve<LookupService>();
            //    var lookups= await lookupService.GetLookupCached(LookupTypes.PostType);
            //}
            return View();
        }
    }
}
