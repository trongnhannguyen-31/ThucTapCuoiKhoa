using Phoenix.Mobile.Core.Models.Vendor;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AlertPageModel : BasePageModel
    {
        public AlertPageModel()
        {
            
        }
        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "AlertPage";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            TestString = "hello world";
        }

        #region commands

        #endregion

        #region properties
        public string TestString { get; set; }
        #endregion
    }
}
