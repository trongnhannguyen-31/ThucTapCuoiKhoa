using Falcon.Web.Core.Settings;

namespace CongDongBau.Server.Services.Settings
{
    public class MobileSettings : ISettings
    {
        public bool IsProductServer { get; set; }
        public string ApnsProductFile { get; set; }
        public string ApnsSandboxFile { get; set; }
        public string ApnsCertificatePassword { get; set; }
        public string FcmAppId { get; set; }
        public string FcmSender { get; set; }
    }
}
