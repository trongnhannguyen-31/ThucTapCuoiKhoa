using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Server.Services.MainServices.Users;
using Phoenix.Shared.Core;
using Phoenix.Shared.User;
using Phoenix.Shared.z_User;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/user")]
    public class UserApiController : BaseApiController
    {
        private readonly UserAuthService _userAuthService;
        private readonly UserService _userService;
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

        //[HttpPost]
        //[Route("CreateAccount")]
        //public Task<CrudResult> CreateAccount([FromBody] z_UserRequest request)
        //{
        //    return _userService.CreateAccount(request);
        //}

        [HttpPost]
        [Route("CreateUser")]
        public Task<CrudResult> CreateUser([FromBody] UserRequest request)
        {
            return _userService.CreateUser(request);
        }
    }
}