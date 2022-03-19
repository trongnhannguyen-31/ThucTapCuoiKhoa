using Phoenix.Mobile.Helpers;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing;

namespace Phoenix.Mobile.PageModels.Common
{
    public class ReadQrCodePageModel : BasePageModel
    {
        #region methods
        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Quét QR Code";
        }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            NavigatingAway = false;
            base.ViewIsAppearing(sender, e);
        }

        #endregion

        #region command

        private Command<object> _scanResultCommand;
        public Command<object> ScanResultCommand =>
            _scanResultCommand ?? (_scanResultCommand = new Command<object>(async (p) => await ScanResultAction(p), (p) => !IsBusy));
        private async Task ScanResultAction(object obj)
        {
            // Prevent multiple event triggers from triggering the navigation multiple times
            if (NavigatingAway)
                return;

            NavigatingAway = true;

            var result = obj as ZXing.Result;
            var format = result?.BarcodeFormat.ToString() ?? string.Empty;
            var value = result?.Text ?? string.Empty;

            Device.BeginInvokeOnMainThread(async () =>
            {
                // Navigate to a page based on value
                await CoreMethods.PopPageModel(value);
            });
        }
        #endregion

        #region properties
        public Result ScanResult { get; set; }
        private bool NavigatingAway = false;
        #endregion
    }
}
