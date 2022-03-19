using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices.Log
{
    public interface ISmsService
    {
        Task<bool> SendOtpAsync(string phone,string msgBody);
    }
}