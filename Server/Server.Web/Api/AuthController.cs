using System;
using System.Globalization;
using System.Web.Http;
using Falcon.Web.Core.Auth;

namespace Phoenix.Server.Web.Api
{
    public class AuthController : BaseApiController
    {
        //private readonly IAuthService _authService;
        public AuthController()
        {
            //_authService = authService;
        }

        [HttpGet, AllowAnonymous]
        public string Ping()
        {
            return DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }
    }
}