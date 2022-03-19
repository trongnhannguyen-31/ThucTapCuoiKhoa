using CongDongBau.Core.Services.Common;
using CongDongBau.iOS.Utils;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(StatusBar))]
namespace CongDongBau.iOS.Utils
{
    class StatusBar : IStatusBar
    {
        public int GetHeight()
        {
            return (int)UIApplication.SharedApplication.StatusBarFrame.Height;
        }
    }
}