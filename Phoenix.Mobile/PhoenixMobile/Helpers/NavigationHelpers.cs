using Phoenix.Mobile.Controls;
using Phoenix.Mobile.PageModels.Auth;
using Phoenix.Mobile.PageModels.Common;
using FreshMvvm;
using Xamarin.Forms;

namespace Phoenix.Mobile.Helpers
{
    public static class NavigationHelpers
    {
        public const string LoginNavigationStack = "LoginNavigationStack";
        public const string MainNavigationStack = "MainNavigationStack";
        public static void ToLoginPage()
        {
            var loginPage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            var nav = new FreshNavigationContainer(loginPage, LoginNavigationStack);
            Application.Current.MainPage = nav;
        }
        public static void ToMainPage()
        {
            #region [Top Tabbed Page]

            if (Device.RuntimePlatform == Device.Android)
            {
                var mainPage = new FreshTabbedNavigationContainer(MainNavigationStack);
                mainPage.AddTab<HomePageModel>(string.Empty, "ic_act_home");
                mainPage.AddTab<AlertPageModel>(string.Empty, "ic_act_notify");
                mainPage.AddTab<CartPageModel>(string.Empty, "ic_act_code");
                mainPage.AddTab<AccountPageModel>(string.Empty, "ic_act_idea");
                //mainPage.AddTab<AccountPageModel>(string.Empty, "ic_act_more");
                mainPage.BarTextColor = Color.White;
                mainPage.BarBackgroundColor = Color.FromHex("#168dc3");
                Application.Current.MainPage = mainPage;
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                var mainPage = new iOSNavigation(MainNavigationStack);
                mainPage.AddTab<HomePageModel>(string.Empty, "ic_act_home");
                mainPage.AddTab<AlertPageModel>(string.Empty, "ic_act_notify");
                //mainPage.AddTab<EndowPageModel>(string.Empty, "ic_act_code");
                //mainPage.AddTab<PluginPageModel>(string.Empty, "ic_act_idea");
                //mainPage.AddTab<AccountPageModel>(string.Empty, "ic_act_more");
                mainPage.BarTextColor = Color.White;
                mainPage.BarBackgroundColor = Color.FromHex("#4AACC5");
                mainPage.BackgroundColor = Color.FromHex("#4AACC5");
                mainPage.FontFamily = "Material Design Icons";
                Application.Current.MainPage = mainPage;
            }
            #endregion
            //Application.Current.MainPage = new AppShell();
        }

    }
}
