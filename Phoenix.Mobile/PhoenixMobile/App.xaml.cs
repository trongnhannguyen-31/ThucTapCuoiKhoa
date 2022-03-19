using System;
using Xamarin.Forms;
using FreshMvvm;
using Plugin.FirebasePushNotification;
using Rg.Plugins.Popup.Services;
using Phoenix.Mobile.Helpers;

namespace Phoenix.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTYwN0AzMTM4MmUzNDJlMzBTQ3k3SGY3K0RNOCtNT3RXenNOblkwSE1TZHFhczRyUmVSUGFCQWFYQXE0PQ==");
            InitializeComponent();
            Bootstrap.Init();
        }
        protected override void OnStart()
        {
            NavigationHelpers.ToLoginPage();

            #region Notification
            /*
            CrossFirebasePushNotification.Current.Subscribe(new string[] { "phoenix_channel"});
            CrossFirebasePushNotification.Current.OnTokenRefresh += async (s, p) =>
             {
                 var settingService = FreshIOC.Container.Resolve<ISettingService>();
                 await settingService.UpdateSettings();
             };
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                try
                {
                    
                    if (p.Data == null) return;
                    DeviceCache.Current.FcmMessage = new Core.Models.Notification.FcmMessage
                    {
                        Message = p.Data["body"].ToString(),
                        Title = p.Data["title"].ToString()
                    };
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {
                            if (!p.Data.ContainsKey("message")) return;
                            
                            //var jsondata =
                           //     JsonConvert.DeserializeObject<FcmMessage>(p.Data["message"].ToString());
                            //var feedItems = new WMS_FeedItemsDto
                            //{
                            //    Title = jsondata.Title,
                            //    Full = jsondata.Body,
                            //    StartDate = jsondata.NgayDiemDanh,
                            //    HocVienCode = jsondata.HocVienCode,
                            //    ImageUrl = jsondata.ImageUrl
                            //};
                            
                        }
                        catch (Exception er)
                        {
                            // ignored
                        }
                    });
                }
                catch (Exception)
                {
                    // ignored
                }
            };
            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                try
                {
                    //DeviceCache.Current.OpenTabIndex = 1;

                    //if (p.Data == null) return;
                    //Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                    //{
                    //    try
                    //    {
                    //        if (!p.Data.ContainsKey("message")) return;
                    //        var dialog = new NotifyPopupPage
                    //        {
                    //            ContentNotify = p.Data["body"].ToString(),
                    //            TitleNotify = p.Data["title"].ToString(),
                    //        };
                    //        await PopupNavigation.Instance.PushAsync(dialog);
                    //        var result = await dialog.GetResult();
                    //        await PopupNavigation.Instance.PopAllAsync();
                    //        //await core.PushPageModel<NotifyFromHostPageModel>();
                    //        //if (!p.Data.ContainsKey("message")) return;
                    //        //var jsondata = JsonConvert.DeserializeObject<NotificationModel>(p.Data["message"].ToString());
                    //        //var feedItems = new WMS_FeedItemsDto
                    //        //{
                    //        //    Title = jsondata.Title,
                    //        //    Full = jsondata.Body,
                    //        //    StartDate = jsondata.NgayDiemDanh,
                    //        //    HocVienCode = jsondata.HocVienCode,
                    //        //    ImageUrl = jsondata.ImageUrl
                    //        //};
                    //        //var navigation = FreshIOC.Container.Resolve<INavigationHelper>();
                    //        //navigation?.LoadNotificationPage(feedItems);
                    //    }
                    //    catch (Exception)
                    //    {
                    //        // ignored
                    //    }
                    //});
                }
                catch (Exception)
                {
                    // ignored
                }
            };

            CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
            {
                //if (p.Data == null) return;
                //DeviceCache.Current.FcmMessage = new Core.Models.Notification.FcmMessage
                //{
                //    Message = p.Data["body"].ToString(),
                //    Title = p.Data["title"].ToString()
                //};
            };

            CrossFirebasePushNotification.Current.OnNotificationDeleted += (s, p) =>
            {
            };
            */
            #endregion
        }
    }
}
