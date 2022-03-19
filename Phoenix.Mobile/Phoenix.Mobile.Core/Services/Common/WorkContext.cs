using Akavache;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.Mobile.Core.Models.Auth;
using Xamarin.Forms;
using Phoenix.Framework.Extensions;

namespace Phoenix.Mobile.Core.Services
{
    public interface IWorkContext
    {
        string DeviceId { get; }
        AuthToken Token { get; }

        Task DeleteContext();
        Task Init();
        bool IsAuth();
        Task SetToken(AuthToken token);
    }

    public class WorkContext : IWorkContext
    {
        private readonly IBlobCache _dataStore = BlobCache.Secure;
        public async Task Init()
        {
            Token = await _dataStore.GetOrCreateObject(TokenKey, () => new AuthToken());
        }


        #region DeviceId
        private const string DeviceIdKey = "DeviceIdKey";
        public string DeviceId
        {
            get
            {
                if (!Application.Current.Properties.ContainsKey(DeviceIdKey))
                {
                    Application.Current.Properties.Add(DeviceIdKey, Guid.NewGuid().ToString());

                }
                return (string)Application.Current.Properties[DeviceIdKey];
            }
        }
        #endregion


        //token
        public bool IsAuth()
        {
            return Token != null
                   && !Token.AccessToken.IsNullOrEmpty() && Token.ExpiredAt > DateTime.Now;
        }
        public async Task SetToken(AuthToken token)
        {
            await _dataStore.InsertObject(TokenKey, token);
            Token = token;
        }

        public async Task DeleteContext()
        {
            await _dataStore.Invalidate(TokenKey);
            await Init();
        }

        private const string TokenKey = "WorkContext.Token";
        public AuthToken Token { get; private set; }
    }
}
