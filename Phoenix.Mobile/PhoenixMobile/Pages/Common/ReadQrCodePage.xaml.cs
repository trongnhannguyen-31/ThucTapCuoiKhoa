using Phoenix.Mobile.PageModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Phoenix.Mobile.Pages.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReadQrCodePage : ContentPage
    {
        public ReadQrCodePage()
        {
            InitializeComponent();
            scannerView.Options = new ZXing.Mobile.MobileBarcodeScanningOptions
            {
                PossibleFormats =
                new List<ZXing.BarcodeFormat>
                    {
                        ZXing.BarcodeFormat.QR_CODE
                    }
            };

            InitRedLine();
            scannerView.OnScanResult += ScannerView_OnScanResult;
            scannerOverlay.FlashButtonClicked += ScannerOverlay_FlashButtonClicked;
        }
        protected override void OnAppearing()
        {
            scannerView.IsScanning = true;

            base.OnAppearing();
        }
        private void InitRedLine()
        {
            //remove redline
            var redLine = scannerOverlay.Children.First(x => x.BackgroundColor == Color.Red);
            scannerOverlay.Children.Remove(redLine);
            //convert blue line
            var blueLine = redLine;
            blueLine.BackgroundColor = Color.Blue;
            //animate blue line
            scannerOverlay.Children.Add(blueLine);
            if (scannerView.IsScanning)
            {
                Task.Run(async () =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        int x = -1000, y = 1000, z = 0;
                        bool moveDown = false;
                        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                        {
                            // Do something
                            if (moveDown)
                            {
                                z = z + 50;
                                if (z >= 150)
                                {
                                    moveDown = !moveDown;
                                }
                            }
                            else
                            {
                                z = z - 50;
                                if (z <= -150)
                                {
                                    moveDown = !moveDown;
                                }
                            }
                            blueLine.TranslateTo(0, z, 1000);
                            if (!scannerView.IsScanning) return false;
                            return true; // True = Repeat again, False = Stop the timer
                        });
                    });
                });
            }
        }
        private void ScannerOverlay_FlashButtonClicked(Button sender, EventArgs e)
        {
            scannerView.ToggleTorch();
        }

        private void ScannerView_OnScanResult(ZXing.Result result)
        {
            var model = this.BindingContext as ReadQrCodePageModel;
            if (model == null)
                return;

            scannerView.IsScanning = false;

            if (model.ScanResultCommand.CanExecute(result))
                model.ScanResultCommand.Execute(result);

        }
    }
}