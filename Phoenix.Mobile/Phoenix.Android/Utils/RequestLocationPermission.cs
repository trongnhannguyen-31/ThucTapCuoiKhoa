using System;
using Android;
using Android.App;
using Android.Support.V4.App;
using Phoenix.Mobile.Core.Infrastructure;

namespace Phoenix.Droid.Utils
{
    public class RequestLocationPermission: IRequestLocation
    {
        internal static Func<Activity> GetActivity { get; set; }
        public void RequestFindLocation()
        {
            var activity = GetActivity();
            if (activity == null)
                return;

            //fix permission location
            var per = ActivityCompat.ShouldShowRequestPermissionRationale(activity,
                          Manifest.Permission.AccessCoarseLocation)
                      && ActivityCompat.ShouldShowRequestPermissionRationale(activity,
                          Manifest.Permission.AccessFineLocation)
                      && ActivityCompat.ShouldShowRequestPermissionRationale(activity, Manifest.Permission_group.Location);
            if (!per)
            {
                ActivityCompat.RequestPermissions(activity, new string[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation, Manifest.Permission_group.Location }, 999);
            }
        }
    }
}