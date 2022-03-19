using System;
using System.Net.Http;
using System.Threading.Tasks;
using Phoenix.Mobile.Core.Constants;
using Phoenix.Mobile.Core.Services;
using FreshMvvm;
using Phoenix.Framework.Core;
using Phoenix.Framework.Extensions;

namespace Phoenix.Mobile.Core.Framework
{
    public class BaseProxy
    {
        private IExceptionHandler _exceptionHandler;

        protected IExceptionHandler ExceptionHandler =>
            _exceptionHandler ?? (_exceptionHandler = FreshIOC.Container.Resolve<IExceptionHandler>());
        protected HttpClient GetHttpClient(string apiUrl = "")
        {
#if DEBUG
            var tmp = new Uri(BuildFullUrl(apiUrl));
#endif
            return new HttpClient(new AuthenticatedHttpClientHandler(GetToken))
            {
                BaseAddress = new Uri(BuildFullUrl(apiUrl)),
            };
        }

        private string BuildFullUrl(string apiName)
        {
            var serverUrl = ServerAddress.ServerBaseUrl;
            return serverUrl + (apiName.IsNullOrEmpty() ? "" : "/" + apiName);
        
        }
        private async Task<string> GetToken()
        {
            var workContext = FreshIOC.Container.Resolve<IWorkContext>();
            var authToken = workContext.Token;
            if (authToken.ExpiredAt >= DateTime.Now || authToken.RefreshToken.IsNullOrEmpty())
                return authToken.AccessToken;
            //TODO refresh token

            return "";
        }

    }
}
