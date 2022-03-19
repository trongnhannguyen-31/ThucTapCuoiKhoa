using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Phoenix.Droid
{
    [Activity(MainLauncher = true, Theme = "@style/SplashTheme.Splash"
        , NoHistory = true
        , ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
        }
    }
}