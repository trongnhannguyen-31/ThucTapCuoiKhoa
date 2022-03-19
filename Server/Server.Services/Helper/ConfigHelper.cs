using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.Helper
{
    public class ConfigHelper<T>
    {
        private const string _appSettingName = "applicationSettings";
        public static T GetConfig(string key)
        {
            var lstSetting = ConfigurationManager.GetSection(_appSettingName) as NameValueCollection;
            if (lstSetting == null) return (T)Convert.ChangeType(string.Empty, typeof(T));
            var findKey = lstSetting.AllKeys.FirstOrDefault(x => x == key);
            return (T)Convert.ChangeType(findKey, typeof(T));
        }        
    }
}
