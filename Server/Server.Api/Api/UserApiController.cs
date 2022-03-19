using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Core;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/user")]
    public class UserApiController : BaseApiController
    {
        private readonly UserAuthService _userAuthService;
        public UserApiController(UserAuthService userAuthService)
        {
            _userAuthService = userAuthService;
        }

        [HttpPost]
        [Authorize]
        [Route("changepwd")]
        public async Task<CrudResult> ChangePassword(string phone, string oldPwd, string newPwd) => await _userAuthService.ChangePasswordNew(phone, oldPwd, newPwd);

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("forgotpwd")]
        //public async Task<CrudResult> ForgotPassword(string phone, string newPwd) => await _userAuthService.ForgotPassword(phone, newPwd);
    }
}