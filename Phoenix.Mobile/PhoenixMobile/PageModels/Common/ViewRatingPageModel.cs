using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Order;
using Phoenix.Mobile.Core.Models.Rating;
using Phoenix.Mobile.Core.Services;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Rating;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class ViewRatingPageModel : BasePageModel
    {
        private readonly IDialogService _dialogService;
        private readonly IWorkContext _workContext;
        private readonly IRatingService _ratingService;

        public ViewRatingPageModel(IDialogService dialogService, IWorkContext workContext, IRatingService ratingService)
        {
            _dialogService = dialogService;
            _workContext = workContext;
            _ratingService = ratingService;
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
            request.Order_Id = Order.Id;
            var data = await _ratingService.GetRatingByProductSKUId(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Ratings = data;
                RaisePropertyChanged(nameof(Ratings));
            }
        }

        #region Properties
        public RatingAppRequest request { get; set; } = new RatingAppRequest();
        public List<RatingModel> Ratings { get; set; } = new List<RatingModel>();
        #endregion
    }
}
