using Phoenix.Mobile.Core.Services;
using Phoenix.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AccountPageModel : BasePageModel
    {
        private readonly AuthService _authService;
        private readonly IWorkContext _workContext;
        public AccountPageModel(AuthService authService, IWorkContext workContext)
        {
            _workContext = workContext;
            _authService = authService;
        }
        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Đăng nhập tài khoản";
            await LoadData();
        }
        private async Task LoadData()
        {
            if (IsBusy) return;
            IsBusy = true;
#if DEBUG
            var token =  _workContext.Token;
            Username = token.UserName;
            FullName = token.FullName;
#endif
            IsBusy = false;
        }
        public string Username { get; set; }
        public string FullName { get; set; }
        #region ViewOrderCommand
        public Command ViewOrderCommand => new Command(async (p) => await ViewOrderExecute(), (p) => !IsBusy);
        private async Task ViewOrderExecute()
        {

            await CoreMethods.PushPageModel<AlertPageModel>();

        }
        #endregion

        #region AccountDetailCommand
        public Command AccountDetailCommand => new Command(async (p) => await AccountDetailExecute(), (p) => !IsBusy);
        private async Task AccountDetailExecute()
        {

            await CoreMethods.PushPageModel<AccountDetailPageModel>();

        }
        #endregion

        #region LogoutCommand
        public Command LogoutCommand => new Command(async (p) => await LogOutExecute(), (p) => !IsBusy);
        private async Task LogOutExecute()
        {
            //_authService.LogOut();
            //await CoreMethods.PushPageModel<Auth.LoginPageModel>();
            Device.BeginInvokeOnMainThread(() =>
            {
                NavigationHelpers.ToLoginPage();
            });
        }
        #endregion
    }
}
