using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Framework.Extensions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Auth
{
    public class ForgotPasswordPageModel : BasePageModel
    {
        private readonly IDialogService _dialogService;
        private readonly IUserService _userService;
        public ForgotPasswordPageModel(IDialogService dialogService, IUserService userService)
        {
            _dialogService = dialogService;
            _userService = userService;
        }

        #region methods

        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            await LoadData();
        }
        private async Task LoadData()
        {
            if (IsBusy) return;
            InputPhoneNumberVisible = true;
            CurrentPage.Title = "Nhập số điện thoại";
            IsBusy = true;

            IsBusy = false;
        }

        private void SetupDefault()
        {
            InputPhoneNumberVisible = false;
            CheckOtpVisible = false;
            ChangePassVisible = false;
        }

        #endregion

        #region properties
        public bool InputPhoneNumberVisible { get; set; }
        public bool CheckOtpVisible { get; set; }
        public bool ChangePassVisible { get; set; }
        public string OtpCode { get; set; }
        public string Phone { get; set; }
        public string NewPass { get; set; }
        public string RetypePass { get; set; }
        #endregion

        #region commands

        #region RenewPassCommand

        private Command _renewPassCommand;

        public Command RenewPassCommand =>
            _renewPassCommand ?? (_renewPassCommand = new Command(async (p) => await RenewPassExecute(), (p) => !IsBusy));

        private async Task RenewPassExecute()
        {
            if (NewPass.IsNullOrEmpty() || RetypePass.IsNullOrEmpty())
            {
                await _dialogService.AlertAsync("Vui lòng điền đầy đủ thông tin");
                return;
            }
            if (NewPass != RetypePass)
            {
                await _dialogService.AlertAsync("Mật khẩu phải nhập giống nhau");
                return;
            }
            IsBusy = true;
            var res = await _userService.ForgotPassword(Phone, RetypePass);
            IsBusy = false;
            if (res.IsOk)
            {
                await CoreMethods.PopPageModel();
                _dialogService.Toast("Đổi mật khẩu thành công");
            }
            else
            {
                await _dialogService.AlertAsync(res.ErrorDescription);
            }
        }
        #endregion


        #endregion


    }
}
