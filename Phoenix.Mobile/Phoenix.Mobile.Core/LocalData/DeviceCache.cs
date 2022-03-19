using Akavache;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.LocalData
{
    public class DeviceCache
    {
        private static DeviceCache _current;
        public static DeviceCache Current => _current ?? (_current = new DeviceCache());
        private readonly IBlobCache _mainCache = BlobCache.UserAccount;
        private readonly ISecureBlobCache _secureCache = BlobCache.Secure;
        private DeviceCache()
        {
        }
        #region private methods

        async void AddOrUpdateValue<T>(string key, T val)
        {
            await _mainCache.InsertObject(key, val);
        }

        T GetValueOrDefault<T>(string key, T defaultValue)
        {
            return Task.Run(async () =>
            {
                try
                {
                    var obj = await _mainCache.GetObject<T>(key);
                    return obj;
                }
                catch (KeyNotFoundException)
                {
                    await _mainCache.InsertObject(key, defaultValue);
                    return defaultValue;
                }
            }).Result;
        }
        async void AddOrUpdateValueSecure<T>(string key, T val)
        {
            await _secureCache.InsertObject(key, val);
        }

        T GetValueOrDefaultSecure<T>(string key, T defaultValue)
        {
            return Task.Run(async () =>
            {
                try
                {
                    var obj = await _secureCache.GetObject<T>(key);
                    return obj;
                }
                catch (KeyNotFoundException)
                {
                    await _secureCache.InsertObject(key, defaultValue);
                    return defaultValue;
                }
            }).Result;
        }
        #endregion
      
        #region OpenTabIndex
        private const string OpenTabIndexKey = "OpenTabIndex_Key";
        public int OpenTabIndex
        {
            get => GetValueOrDefaultSecure(OpenTabIndexKey, 0);
            set => AddOrUpdateValueSecure(OpenTabIndexKey, value);
        }
        #endregion

    }
}
