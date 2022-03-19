using Refit;
using System;
using System.Threading.Tasks;
using Phoenix.Mobile.Core.Models.Auth;
using Phoenix.Mobile.Core.Services;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Framework.Core;
using Phoenix.Shared.Auth;
using Phoenix.Framework.Extensions;

namespace Phoenix.Mobile.Core.Proxies
{
    public interface IAuthProxy
    {
        Task<bool> Authenticate(string userName, string password);
        Task<bool> ExternalAuth(string provider, string email, string avatar, string lastName);
    }

    public class AuthProxy : BaseProxy, IAuthProxy
    {
        private readonly IWorkContext _workContext;
        public AuthProxy(IWorkContext workContext)
        {
            _workContext = workContext;
        }

        public async Task<bool> Authenticate(string userName, string password)
        {
            try
            {
                var api = RestService.For<IAuthApi>(GetHttpClient());
                var tokenResponse = await api.FarmerLogin(new TokenRequest()
                {
                    UserName = userName,
                    Password = password,
                });
                if (tokenResponse.IsError || tokenResponse.AccessToken.IsNullOrEmpty()) return false;

                // var authToken = tokenResponse.MapTo<AuthToken>();
                var authToken = new AuthToken()
                {
                    AccessToken = tokenResponse.AccessToken,
                    ExpiresIn = tokenResponse.ExpiresIn,
                    //RefreshToken = tokenResponse.RefreshToken,
                    UserId = tokenResponse.UserId,
                    UserName = tokenResponse.UserName,
                    FullName = tokenResponse.FullName,
                    //buffer 1 min
                    ExpiredAt = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn)
                };

                //authToken.UserId = tokenResponse.Result.UserId;
                //update userID for AppCenter Push
                //AppCenter.SetUserId(authToken.UserId.ToString());
                //buffer 1 min
                //authToken.ExpiredAt = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn);
                await _workContext.SetToken(authToken);
                return true;
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(new NetworkException(e), true);
                return false;
            }
        }

        public async Task<bool> ExternalAuth(string provider, string email, string avatar, string lastName)
        {
            try
            {
                var api = RestService.For<IAuthApi>(GetHttpClient());
                var tokenResponse = await api.ExternalLogin(new ExternalTokenRequest()
                {
                    Provider = provider,
                    Avatar = avatar,
                    Email = email,
                    LastName = lastName
                });
                if (tokenResponse.IsError || tokenResponse.AccessToken.IsNullOrEmpty()) return false;
                var authToken = new AuthToken()
                {
                    AccessToken = tokenResponse.AccessToken,
                    ExpiresIn = tokenResponse.ExpiresIn,
                    UserId = tokenResponse.UserId,
                    UserName = tokenResponse.UserName,
                    FullName = tokenResponse.FullName,
                    //buffer 1 min
                    ExpiredAt = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn)
                };
                await _workContext.SetToken(authToken);
                return true;
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(new NetworkException(e), true);
                return false;
            }
        }

        public interface IAuthApi
        {
            [Post("/auth/login")]
            Task<TokenResponse> FarmerLogin([Body] TokenRequest request);

            [Post("/auth/externallogin")]
            Task<TokenResponse> ExternalLogin([Body] ExternalTokenRequest request);
        }
    }
}
