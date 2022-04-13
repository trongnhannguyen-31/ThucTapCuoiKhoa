using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Customer;
using Phoenix.Mobile.Core.Services;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Customer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AccountDetailPageModel : BasePageModel
    {
        private readonly ICustomerService _customerDetailService;
        private readonly IDialogService _dialogService;
        private readonly IWorkContext _workContext;

        public AccountDetailPageModel(ICustomerService orderDetailService, IDialogService dialogService, IWorkContext workContext)
        {
            _customerDetailService = orderDetailService;
            _dialogService = dialogService;
            _workContext = workContext;
        }

        public override async void Init(object initData)
        {
            //base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Account";
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            var token = _workContext.Token;
            UserId = token.UserId;
            base.ViewIsAppearing(sender, e);
            await LoadData();
            if (IsBusy) return;
            IsBusy = true;
#if DEBUG
            Id = Customer.Id;
            FullName = Customer.FullName;
            Gender = Customer.Gender;
            Birthday = Customer.Birthday;
            Phone = Customer.Phone;
            Email = Customer.Email;
            Address = Customer.Address;
            zUser_Id = Customer.zUser_Id;

            
#endif
            IsBusy = false;
        }

        private async Task LoadData()
        {


            request.zUser_Id = UserId;
            var data = await _customerDetailService.GetCustomerApptById(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Customer = data;
                RaisePropertyChanged(nameof(Customer));
            }
        }

        #region properties
        public CustomerModel Customer { get; set; } = new CustomerModel();
        public CustomerRequest request { get; set; } = new CustomerRequest();
        public bool IsEnabled { get; set; } = false;
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int zUser_Id { get; set; }
        public int UserId { get; set; }




        #endregion

        #region EditCommand
        public Command EditCommand => new Command(async (p) => await EditExecute(), (p) => !IsBusy);
        private async Task EditExecute()
        {
            IsEnabled = true;

        }
        #endregion

        #region UpdateCustomerDetailCommand
        public Command UpdateCustomerDetailCommand => new Command(async (p) => await UpdateCustomerDetailExecute(), (p) => !IsBusy);
        private async Task UpdateCustomerDetailExecute()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;

                var data = await _customerDetailService.UpdateCustomerDetail(Id, new CustomerRequest
                {
                    Id = Id,
                    FullName = FullName,
                    Gender = Gender,
                    Birthday = Birthday,
                    Phone = Phone,
                    Email = Email,
                    Address = Address,
                    zUser_Id = zUser_Id
                });
                await _dialogService.AlertAsync("Cập nhật thành công");
                await CoreMethods.PushPageModel<AccountPageModel>();                
                IsBusy = false;

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Cập nhật thất bại");
            }
        }
        #endregion
    }
}
