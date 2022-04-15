using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.OrderDetail;
using Phoenix.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class MultipleRatingPageModel : BasePageModel
    {
        private readonly IDialogService _dialogService;

        public MultipleRatingPageModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public List<OrderDetailHistoryModel> OrderDetails { get; set; }

        public override async void Init(object initData)
        {
            if (initData != null)
            {
                OrderDetails = (List<OrderDetailHistoryModel>)initData;
            }
            else
            {
                OrderDetails = new List<OrderDetailHistoryModel>();
            }
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            //CurrentPage.Title = "Chi tiết đơn hàng";
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
            //request.Order_Id = Order.Id;
            //var data = await _orderDetailService.GetOrderDetailHistory(request);
            //if (data == null)
            //{
            //    await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            //}
            //else
            //{
            //    OrderDetails = data;
                
            //    RaisePropertyChanged(nameof(OrderDetails));
                
                
            //}
        }

        private OrderDetailHistoryModel _selectedItem;
        public OrderDetailHistoryModel SelectedItem
        {


            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
            }
        }

        #region RatingPageCommand
        public Command RatingPageCommand => new Command(async (p) => await RatingPageExecute(), (p) => !IsBusy);
        private async Task RatingPageExecute()
        {
            await _dialogService.AlertAsync(SelectedItem.ProductName);
        }
        #endregion
    }
}
