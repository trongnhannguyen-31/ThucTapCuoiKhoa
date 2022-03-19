using Phoenix.Mobile.Core.Services;
using Phoenix.Mobile.Helpers;
using FreshMvvm;
using System;
using Phoenix.Mobile.Core.Infrastructure;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class RoutePageModel : BasePageModel
    {
        #region methods
        private IWorkContext workContext;
        public override void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);

        }
        #endregion
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            workContext = FreshIOC.Container.Resolve<IWorkContext>();
            await workContext.Init();
            Device.BeginInvokeOnMainThread(NavigationHelpers.ToLoginPage);
        }

        #region properties

        #endregion

        #region commands

        #endregion


    }
}
