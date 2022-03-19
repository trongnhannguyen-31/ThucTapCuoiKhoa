using Phoenix.Mobile.Core.Proxies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services
{
    public interface IAuthService
    {
        Task<bool> ExternalAuth(string provider, string email, string avatar, string lastName);
        Task<bool> Login(string farmerCode, string password);
        void LogOut();
    }

    public class AuthService : IAuthService
    {
        private readonly IWorkContext _workContext;
        private readonly IAuthProxy _authProxy;
        private static readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);
        public AuthService(IAuthProxy authProxy, IWorkContext workContext)
        {
            _authProxy = authProxy;
            _workContext = workContext;
        }



        public Task<bool> Login(string farmerCode, string password)
        {
            return _authProxy.Authenticate(farmerCode, password);
        }

        private bool _isBusy;
        public void LogOut()
        {
            if (!_workContext.IsAuth() || _isBusy) return;
            SemaphoreSlim.Wait();
            try
            {
                _isBusy = true;
                _workContext.DeleteContext();
            }
            finally
            {
                _isBusy = false;
                SemaphoreSlim.Release();
            }
        }

        public Task<bool> ExternalAuth(string provider, string email, string avatar, string lastName)
        {
            return _authProxy.ExternalAuth(provider, email, avatar, lastName);
        }
    }
}
