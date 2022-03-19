using System;
using System.Collections.Generic;
using System.Linq;
using CongDongBau.Core.Infrastructure;
using CongDongBau.iOS.Utils;
using FFImageLoading.Svg.Forms;
using Foundation;
using FreshMvvm;
using MediaManager;
using Naxam.Controls.Platform.iOS;
using PanCardView.iOS;
using Plugin.FirebasePushNotification;
using SegmentedControl.FormsPlugin.iOS;
using UIKit;
using UserNotifications;
using Xamarin.Forms.PancakeView.iOS;

namespace CongDongBau.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            Xamarin.Forms.Forms.SetFlags(new string[] { "CollectionView_Experimental", "RadioButton_Experimental"});
            global::Xamarin.Forms.Forms.Init();
            InitPlugins();
            DependencyRegistraSpecificPlatform();
            LoadApplication(new App());
            FirebasePushNotificationManager.Initialize(options);
            FirebasePushNotificationManager.CurrentNotificationPresentationOption = UNNotificationPresentationOptions.Alert | UNNotificationPresentationOptions.Badge | UNNotificationPresentationOptions.Sound;
            UIUserNotificationType userNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
            UIUserNotificationSettings settings = UIUserNotificationSettings.GetSettingsForTypes(userNotificationTypes, null);
            UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            CrossFirebasePushNotification.Current.RegisterForPushNotifications();
            return base.FinishedLaunching(app, options);
        }

        private void InitPlugins()
        {
            TopTabbedRenderer.Init();
            Xamarin.FormsMaps.Init();
            PancakeViewRenderer.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            var ignore = typeof(SvgCachedImage);
            CrossMediaManager.Current.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            XamEffects.iOS.Effects.Init();
            CardsViewRenderer.Preserve();
            SegmentedControlRenderer.Init();
            Xamarin.Auth.Presenters.XamarinIOS.AuthenticationConfiguration.Init();
        }
        private void DependencyRegistraSpecificPlatform()
        {
            FreshIOC.Container.Register<IRequestLocation, RequestLocationPermission>();
        }
        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            FirebasePushNotificationManager.RemoteNotificationRegistrationFailed(error);
        }
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken);
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            // If you are receiving a notification message while your app is in the background,
            // this callback will not be fired 'till the user taps on the notification launching the application.

            // If you disable method swizzling, you'll need to call this method. 
            // This lets FCM track message delivery and analytics, which is performed
            // automatically with method swizzling enabled.
            FirebasePushNotificationManager.DidReceiveMessage(userInfo);
            // Do your magic to handle the notification data
            System.Console.WriteLine(userInfo);

            completionHandler(UIBackgroundFetchResult.NewData);
        }

        public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
        {
            //return base.ContinueUserActivity(application, userActivity, completionHandler);
            if (userActivity.ActivityType == NSUserActivityType.BrowsingWeb)
            {
                NSUrl url = userActivity.WebPageUrl;
                // other code
            }
            return true;
        }
    }
}
