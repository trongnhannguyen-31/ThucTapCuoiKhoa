using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Firebase;
using Plugin.FirebasePushNotification;

namespace Phoenix.Droid
{
    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
        {
        }

        public object CoreMethods { get; private set; }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
        }

        public void OnActivityStopped(Activity activity)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            //var options = new FirebaseOptions.Builder()
            //                                 .SetApplicationId("congdongbauapp")
            //                                 .SetApiKey("AIzaSyDXeT7vY9HsomVklDTdrEXSexNd2QKg1Zs")
            //                                 .SetDatabaseUrl("https://congdongbauapp.firebaseio.com")
            //                                 .SetStorageBucket("congdongbauapp.appspot.com")
            //                                 .SetGcmSenderId("701886551645").Build();
            //FirebaseApp.InitializeApp(this, options);
            //FirebasePushNotificationManager.Initialize(this, false);
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }
    }
}