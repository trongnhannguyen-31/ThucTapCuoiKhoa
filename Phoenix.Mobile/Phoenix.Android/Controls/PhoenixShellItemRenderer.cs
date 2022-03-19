using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Phoenix.Mobile.Controls;
using Phoenix.Droid.Extensions;
using Xamarin.Forms.Platform.Android;

namespace Phoenix.Droid.Controls
{
    public class PhoenixShellItemRenderer : ShellItemRenderer
    {
        FrameLayout _shellOverlay;
        BottomNavigationView _bottomView;

        public PhoenixShellItemRenderer(IShellContext shellContext) : base(shellContext)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var outerlayout = base.OnCreateView(inflater, container, savedInstanceState);
            _bottomView = outerlayout.FindViewById<BottomNavigationView>(Resource.Id.bottomtab_tabbar);
            _shellOverlay = outerlayout.FindViewById<FrameLayout>(Resource.Id.bottomtab_tabbar_container);

            if (ShellItem is PhoenixTabBar todoTabBar && todoTabBar.PhoenixTab != null)
                SetupLargeTab();

            return outerlayout;
        }

        private async void SetupLargeTab()
        {
            var phoenixTabBar = ShellItem as PhoenixTabBar;
            var layout = new FrameLayout(Context);

            var imageHandler = phoenixTabBar.PhoenixTab.Icon.GetHandler();
            Bitmap bitmap = await imageHandler.LoadImageAsync(phoenixTabBar.PhoenixTab.Icon, Context);
            var image = new ImageView(Context);
            image.SetImageBitmap(bitmap);

            layout.AddView(image);

            var lp = new FrameLayout.LayoutParams(240, 240);
            _bottomView.Measure((int)MeasureSpecMode.Unspecified, (int)MeasureSpecMode.Unspecified);
            lp.BottomMargin = _bottomView.MeasuredHeight / 2;

            layout.LayoutParameters = lp;

            _shellOverlay.RemoveAllViews();
            _shellOverlay.AddView(layout);
        }
    }
}