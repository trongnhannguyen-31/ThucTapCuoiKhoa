using System.Threading.Tasks;
using System.Web.Http;
using Phoenix.Shared.Auth;
using Phoenix.Server.Services.MainServices.Auth;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/auth")]
    public class AuthController : BaseApiController
    {
        private readonly UserAuthService _authService;
        public AuthController(UserAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<TokenResponse> Login([FromBody] TokenRequest request)
        {
            var res = await _authService.LoginAsync(request);
            return res;
        }     
    }
}