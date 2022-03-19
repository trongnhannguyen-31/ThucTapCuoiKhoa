using System.Web.Http;
using Falcon.Web.Api.ExceptionHandle;

namespace Phoenix.Server.Web.Api
{
    [ApiExceptionFilter]
    [Authorize]
    public class BaseApiController : ApiController
    {
    }
}
