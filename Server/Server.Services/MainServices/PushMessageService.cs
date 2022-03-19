using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IPushMessageService
    {
        Task<string> Push(string title, string message, string deviceToken);
        Task<string> PushToChannel(string title, string message);
    }

    public class PushMessageService : IPushMessageService
    {
        //private FcmServiceNET.ExpoService expoService;
        private FcmServiceNET.FcmService fcmService;
        private readonly string applicationId;
        private readonly string senderId;
        private readonly string channel;
        private readonly string expo;
        public PushMessageService()
        {
            //expoService = new FcmServiceNET.ExpoService();
            //Cấu hình android cho app mới
            applicationId = "firebase_server_key";
            senderId = "appsenderid";
            channel = "phoenix_channel";
            expo = "@ttdavid/phoenix";
            fcmService = new FcmServiceNET.FcmService(applicationId, senderId, channel);
        }
        public async Task<string> Push(string title, string message, string deviceToken)
        {
            return await fcmService.PushFCM(title, message, expo, new List<string> { deviceToken });
        }
        public async Task<string> PushToChannel(string title, string message)
        {
            return await fcmService.PushFCM(title, message, expo, new List<string> { });
        }
    }
}
