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



        public OrderDetailHistoryModel OrderDetail { get; set; }

        public override async void Init(object initData)
        {
            if (initData != null)
            {
                OrderDetail = (OrderDetailHistoryModel)initData;
            }
            else
            {
                OrderDetail = new OrderDetailHistoryModel();
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
                if(!One && !Two && !Three && !Four && !Five)
                {
                    await _dialogService.AlertAsync("Vui lòng chọn số sao.");
                    IsBusy = false;
                    return;
                }
                if (One){Rate = 1;}
                if (Two){Rate = 2;}
                if (Three){Rate = 3;}
                if (Four){Rate = 4;}
                if (Five){Rate = 5;}
                
                if (IsBusy) return;
                IsBusy = true;

                var data = _ratingService.AddRating(new RatingAppRequest
                {
                    Rate = Rate,
                    Comment = Comment,
                    CreatedDate = DateTime.Now,
                    Image1 = 13,
                    Image2 = 13,
                    Image3 = 13,
                    Customer_Id = UserId,
                    ProductSKU_Id = OrderDetail.ProductSKU_Id,
                    Order_Id = OrderDetail.Order_Id,
                    Deleted = false
                });
                var data1 = await _orderService.EditOrder(OrderDetail.Order_Id, new OrderAppRequest
                {
                    Id = OrderDetail.Order_Id,
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
            await CoreMethods.PopPageModel();

        }
        #endregion

        #region properties
        public int UserId { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public bool One { get; set; }
        public bool Two { get; set; }
        public bool Three { get; set; }
        public bool Four { get; set; }
        public bool Five { get; set; }
        #endregion
    }
}
