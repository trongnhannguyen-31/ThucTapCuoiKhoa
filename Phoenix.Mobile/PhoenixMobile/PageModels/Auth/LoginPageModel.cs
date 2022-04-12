using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Services;
using Phoenix.Mobile.Helpers;
using Phoenix.Framework.Extensions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Auth
{
    public class LoginPageModel : BasePageModel
    {
        private readonly IDialogService _dialogService;
        private readonly IAuthService _authService;
        public LoginPageModel(IDialogService dialogService, 
            IAuthService authService)
        {
            _dialogService = dialogService;
            _authService = authService;
        }

        #region methods

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
            Username = "admin";
            Password = "123456";
#endif
            IsBusy = false;
        }

        #endregion

        #region properties
        public string Username { get; set; }
        public string Password { get; set; }
        #endregion

        #region commands


        #region LoginCommand
        public Command LoginCommand => new Command(async (p) => await LoginExecute(), (p) => !IsBusy);
        private async Task LoginExecute()
        {
            if (IsBusy) return;
            IsBusy = true;
            if (Username.IsNullOrEmpty())
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
            if (Password.Length < 6)
            {
                //await _dialogService.AlertAsync("Chưa đúng chuẩn yêu cầu");
                await CoreMethods.DisplayAlert("Lỗi", "Mật khẩu phải lớn hơn 6 ký tự", "OK");
                IsBusy = false;
                return;
            }
            var result = await _authService.Login(Username, Password);
            if (result)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    
                    NavigationHelpers.ToMainPage();
                });
            }
            else await _dialogService.AlertAsync("Đăng nhập thất bại");
            IsBusy = false;
        }
        #endregion


        #region ForgotPassCommand

        public Command ForgotPassCommand => new Command(async (p) => await ForgotPassExecute(), (p) => !IsBusy);

        private async Task ForgotPassExecute()
        {
            IsBusy = true;
            await CoreMethods.PushPageModel<ForgotPasswordPageModel>();
            IsBusy = false;
        }
        #endregion

        #region RegisterCommand
        public Command RegisterCommand => new Command(async (p) => await RegisterExecute(), (p) => !IsBusy);

        private async Task RegisterExecute()
        {
            IsBusy = true;
            await CoreMethods.PushPageModel<RegisterPageModel>();
            IsBusy = false;
        }
        #endregion


        #endregion


    }
}
