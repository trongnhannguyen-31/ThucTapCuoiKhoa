using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms.PancakeView.Droid;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using MediaManager;
using Android.Support.V4.App;
using Android;
using FreshMvvm;
using Phoenix.Mobile.Core.Infrastructure;
using Android.Support.V4.Content;
using PanCardView.Droid;
using Plugin.FirebasePushNotification;
using Android.Content;
using Phoenix.Droid.Utils;
using Phoenix.Mobile;

namespace Phoenix.Droid
{
    [Activity(Label = "Phoenix Mobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.SetFlags(new string[] { "CollectionView_Experimental", "RadioButton_Experimental" });
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            InitPlugins(savedInstanceState);
            DependencyRegistraSpecificPlatform();
            RequestLocationPermission.GetActivity = () => this;

            LoadApplication(new App());
            FirebasePushNotificationManager.ProcessIntent(this, Intent);

            App.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            FirebasePushNotificationManager.ProcessIntent(this, intent);
        }
        private void InitPlugins(Bundle savedInstanceState)
        {
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            UserDialogs.Init(this);
            PancakeViewRenderer.Init();
            CrossMediaManager.Current.Init(this);
            XamEffects.Droid.Effects.Init();            
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            CardsViewRenderer.Preserve();
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, savedInstanceState);
        }
        private void DependencyRegistraSpecificPlatform()
        {
            FreshIOC.Container.Register<IRequestLocation, RequestLocationPermission>();            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnStart()
        {
            base.OnStart();
            if (
                ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation) != Permission.Granted
                || ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) != Permission.Granted
                || ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != Permission.Granted
                || ContextCompat.CheckSelfPermission(this, Manifest.Permission.Flashlight) != Permission.Granted
                || ContextCompat.CheckSelfPermission(this, Manifest.Permission.Internet) != Permission.Granted
                )
            {
                ActivityCompat.RequestPermissions(this, new string[] {
                    Manifest.Permission.AccessCoarseLocation
                    , Manifest.Permission.AccessFineLocation
                    , Manifest.Permission.Camera
                    , Manifest.Permission.Flashlight
                    , Manifest.Permission.Internet
                }
                , 0);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Permission Granted!!!");
            }
        }       
    }
}