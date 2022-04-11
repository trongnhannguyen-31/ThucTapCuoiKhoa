using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.OrderDetail;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Order;
using Phoenix.Shared.Rating;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class RatingPageModel : BasePageModel
    {
        private readonly IDialogService _dialogService;
        private readonly IRatingService _ratingService;
        private readonly IOrderService _orderService;

        public RatingPageModel(IDialogService dialogService, IRatingService ratingService, IOrderService orderService)
        {
            _dialogService = dialogService;
            _ratingService = ratingService;
            _orderService = orderService;
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
            CurrentPage.Title = "Nhà cung cấp";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }



        private async Task LoadData()
        {
            //var data = await _productService.GetProductMenus(menurequest);

            //if (data == null)
            //{
            //    await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            //}
            //else
            //{
            //    ProductMenus = data;
            //    RaisePropertyChanged(nameof(ProductMenus));
            //}
        }

        #region AddRatingCommand
        public Command AddRatingCommand => new Command(async (p) => await AddRatingExecute(), (p) => !IsBusy);
        private async Task AddRatingExecute()
        {
            foreach (var item in OrderDetails)
            {
                try
                {
                    if (IsBusy) return;
                    IsBusy = true;

                    var data = _ratingService.AddRating(new RatingAppRequest
                    {
                        Rate = Rate1,
                        Comment = Comment1,
                        CreatedDate = DateTime.Now,
                        Image1 = 1,
                        Image2 = 1,
                        Image3 = 1,
                        Customer_Id = 1,
                        ProductSKU_Id = item.ProductSKU_Id,
                        Order_Id = item.Order_Id,
                        Deleted = false
                    });
                    var data1 = await _orderService.EditOrder(Id, new OrderAppRequest
                    {
                        Id = item.Order_Id,
                        IsRated = true
                    });
                    IsBusy = false;
                }
                catch (Exception e)
                {
                    await _dialogService.AlertAsync("Thêm Order Detail thất bại");

                }
                IsBusy = false;
                await _dialogService.AlertAsync("Đánh giá thành công");
                await CoreMethods.PushPageModel<AlertPageModel>();
                
            }
        }
        #endregion

        #region properties
        public string Comment1 { get; set; }
        public int Rate1 { get; set; }
        public int Id { get; set; }





        #endregion
    }
}
