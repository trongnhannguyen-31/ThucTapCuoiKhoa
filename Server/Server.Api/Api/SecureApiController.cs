using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [Authorize]
    public class SecureApiController : BaseApiController
    {
    }
}