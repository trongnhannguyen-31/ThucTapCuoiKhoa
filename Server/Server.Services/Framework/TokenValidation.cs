using System.Text;
using Falcon.Web.Core.Security;
using Falcon.Web.Core.Settings;

namespace Phoenix.Server.Services.Framework
{
    public class TokenValidation : ITokenValidation
    {
        private readonly ISettingService _settingService;

        public TokenValidation(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public string GetEncryptKey()
        {
            //return _settingService.LoadSetting<SecuritySettings>().EncryptionKey;
            //todo: bo vao setting
            return "3474933548848320";
            //return "authorize.key.token";
        }

        public bool IsUnique(int userId, string token)
        {
            //TODO IsUnique token
            return true;
        }
    }
}
