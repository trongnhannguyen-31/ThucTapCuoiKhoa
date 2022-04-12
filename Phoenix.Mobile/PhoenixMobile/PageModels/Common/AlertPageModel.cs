 using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.ImageRecord;
using Phoenix.Mobile.Core.Models.Order;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.ImageRecord;
//using Phoenix.Shared.ImageRecord;
using Phoenix.Shared.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AlertPageModel : BasePageModel
    {
        private readonly IOrderService _orderService;
        private readonly IDialogService _dialogService;
        private readonly IImageRecordService _imageRecordService;
        public AlertPageModel(IOrderService orderService, IDialogService dialogService, IImageRecordService imageRecordService)
        {
            _orderService = orderService;
            _dialogService = dialogService;
            _imageRecordService = imageRecordService;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Nhà cung cấp";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
            var data = await _imageRecordService.GetAllImages(imageRequest);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {

                Images = data;
                RaisePropertyChanged(nameof(Images));
            }
            //request.Customer_Id = 1;
            //var data = await _orderService.GetAllAppOrders(request);
            //if (data == null)
            //{
            //    await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            //}
            //else
            //{

            //    Orders = data;
            //    RaisePropertyChanged(nameof(Orders));
            //}
        }

        public ObservableCollection<OrderModel> Order { get; set; }
        private OrderModel _selectedOrder;
        public OrderModel SelectedOrder
        {
            get
            {
                return _selectedOrder;
            }
            set
            {
                _selectedOrder = value;
                if (value != null)
                    OrderSelected.Execute(value);
            }
        }

        public Command<OrderModel> OrderSelected
        {
            get
            {
                return new Command<OrderModel>(async (order) => {
                        await CoreMethods.PushPageModel<OrderDetailPageModel>(order);
                });
            }
        }

        #region properties
        public List<OrderModel> Orders { get; set; } = new List<OrderModel>();

        public OrderAppRequest request { get; set; } = new OrderAppRequest();

        public List<ImageRecordModel> Images { get; set; } = new List<ImageRecordModel>();

        public ImageRecordRequest imageRequest { get; set; } = new ImageRecordRequest();

        #endregion
    }



}
