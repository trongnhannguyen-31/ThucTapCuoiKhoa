using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Order;
using Phoenix.Mobile.Core.Models.OrderDetail;
using Phoenix.Mobile.Core.Models.Rating;
using Phoenix.Mobile.Core.Services;
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
        private readonly IWorkContext _workContext;

        public RatingPageModel(IDialogService dialogService, IRatingService ratingService, IOrderService orderService, IWorkContext workContext)
        {
            _dialogService = dialogService;
            _ratingService = ratingService;
            _orderService = orderService;
            _workContext = workContext;
        }



        public OrderModel Order { get; set; }

        public override async void Init(object initData)
        {
            if (initData != null)
            {
                Order = (OrderModel)initData;
            }
            else
            {
                Order = new OrderModel();
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
            var token = _workContext.Token;
            UserId = token.UserId;
           // var data = await _orderDetailService.GetOrderDetailHistory(request);
        }

        #region AddRatingCommand
        public Command AddRatingCommand => new Command(async (p) => await AddRatingExecute(), (p) => !IsBusy);
        private async Task AddRatingExecute()
        {
            try
            {
                    await _dialogService.AlertAsync(Rate1 + "va" + Comment1);
                    //if (IsBusy) return;
                    //IsBusy = true;

                    //var data = _ratingService.AddRating(new RatingAppRequest
                    //{
                    //    Rate = Rate1,
                    //    Comment = Comment1,
                    //    CreatedDate = DateTime.Now,
                    //    Image1 = 1,
                    //    Image2 = 1,
                    //    Image3 = 1,
                    //    Customer_Id = 1,
                    //    ProductSKU_Id = item.ProductSKU_Id,
                    //    Order_Id = item.Order_Id,
                    //    Deleted = false
                    //});
                    //var data1 = await _orderService.EditOrder(item.Order_Id, new OrderAppRequest
                    //{
                    //    Id = item.Order_Id,
                    //    IsRated = true
                    //});
                    //IsBusy = false;
                }
                catch (Exception e)
                {
                    await _dialogService.AlertAsync("Thêm Order Detail thất bại");

                }
                IsBusy = false;
                await _dialogService.AlertAsync("Đánh giá thành công");
                await CoreMethods.PushPageModel<AlertPageModel>();
                
        }
        #endregion

        #region properties
        public RatingModel Rating { get; set; } = new RatingModel();
        public int UserId { get; set; }
        public int Rate1 { get; set; }
        public string Comment1 { get; set; }
        //public OrderDetailHistoryRequest request { get; set; } = new OrderDetailHistoryRequest();





        #endregion
    }
}
