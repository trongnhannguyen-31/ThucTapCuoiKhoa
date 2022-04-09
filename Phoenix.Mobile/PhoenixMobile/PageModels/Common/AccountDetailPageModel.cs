using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Customer;
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

        public AccountDetailPageModel(ICustomerService orderDetailService, IDialogService dialogService)
        {
            _customerDetailService = orderDetailService;
            _dialogService = dialogService;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Account";
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
            request.zUser_Id = 1;
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
        #endregion
    }
}
