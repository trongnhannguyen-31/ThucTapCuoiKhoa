using Phoenix.Framework.Extensions;
using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Customer;
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
        private readonly ICustomerService _customerService;

        public RegisterPageModel(IDialogService dialogService, IUserService userService, ICustomerService customerService)
        {
            _dialogService = dialogService;
            _userService = userService;
            _customerService = customerService;
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
                if (FullName.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập họ tên");
                    IsBusy = false;
                    return;
                }
                if (Gender.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng chọn giới tính");
                    IsBusy = false;
                    return;
                }
                if (Phone.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập số điện thoại");
                    IsBusy = false;
                    return;
                }
                if (Address.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập địa chỉ");
                    IsBusy = false;
                    return;
                }

                var data = await _userService.CreateUser(new UserRequest
                {
                    UserName = UserName,
                    DisplayName = UserName,
                    Password = Password
                });
                var data2 = await _userService.GetLatestUser(userRequest);

                var data3 = _customerService.AddCustomerDetail(new CustomerRequest
                {
                    FullName = FullName,
                    Gender = Gender,
                    Birthday = Birthday,
                    Phone = Phone,
                    Email = Email,
                    Address = Address,
                    zUser_Id = data2.Id
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
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public UserRequest userRequest { get; set; } = new UserRequest();

        #endregion
    }
}
