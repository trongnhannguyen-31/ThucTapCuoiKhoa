using Phoenix.Framework.Extensions;
using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Auth
{
    public class RegisterPageModel : BasePageModel
    {
        private readonly IDialogService _dialogService;
        private readonly IUserService _userService;

        public RegisterPageModel(IDialogService dialogService, IUserService userService)
        {
            _dialogService = dialogService;
            _userService = userService;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Đăng ký tài khoản";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

        }

        #region CreateUserCommand
        public Command CreateUserCommand => new Command(async (p) => await CreateUserExecute(), (p) => !IsBusy);
        private async Task CreateUserExecute()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;
                if (UserName.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập tên đăng nhập");
                    IsBusy = false;
                    return;
                }

                if (Password.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập mật khẩu");
                    IsBusy = false;
                    return;
                }
                if (Password != RetypePassword)
                {
                    await _dialogService.AlertAsync("Mật khẩu và mật khẩu nhập lại không giống nhau");
                    IsBusy = false;
                    return;
                }

                var data = await _userService.CreateUser(new UserRequest
                {
                    UserName = UserName,
                    DisplayName = UserName,
                    Password = Password
                });
                await CoreMethods.PopPageModel();
                _dialogService.Toast("Đăng ký thành công");
                //await CoreMethods.PushPageModel<LoginPageModel>();
                //await _dialogService.AlertAsync("Đăng ký thành công");
                IsBusy = false;

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Đăng ký thất bại");

            }
        }
        #endregion


        #region Properties

        public string UserName { get; set; }
        public string Password { get; set; }
        public string RetypePassword { get; set; }
        public string Name { get; set; }

        #endregion
    }
}
