﻿using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Order;
using Phoenix.Mobile.Core.Models.OrderDetail;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.CartItem;
using Phoenix.Shared.OrderDetail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class OrderDetailPageModel : BasePageModel
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IDialogService _dialogService;
        private readonly ICartItemService _cartItemService;

        public OrderDetailPageModel(IOrderService orderService, IDialogService dialogService, IOrderDetailService orderDetailService, ICartItemService cartItemService)
        {
            _orderService = orderService;
            _dialogService = dialogService;
            _orderDetailService = orderDetailService;
            _cartItemService = cartItemService;
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
            CurrentPage.Title = "Chi tiết đơn hàng";
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
            //Load thông tin chi tiết điện thoại
            request.Order_Id = Order.Id;
            var data = await _orderDetailService.GetOrderDetailHistory(request);
            //var sameVendor = await _productService.GetAllProducts(sameVendorRequest);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                OrderDetails = data;
                ListViewHeight = 60 * OrderDetails.Count;
                RaisePropertyChanged(nameof(OrderDetails));
                if(Order.IsRated == true)
                {
                    RatingButton = false;
                    ViewRatingButton = true;
                }
                else
                {
                    RatingButton = true;
                    ViewRatingButton = false;
                }
            }
        }

        #region properties
        public List<OrderDetailHistoryModel> OrderDetails { get; set; } = new List<OrderDetailHistoryModel>();

        public OrderDetailHistoryRequest request { get; set; } = new OrderDetailHistoryRequest();
        public bool RatingButton { get; set; }
        public bool ViewRatingButton { get; set; }
        public int ListViewHeight { get; set; }

        #endregion

        #region ReBuyCommand
        public Command ReBuyCommand => new Command(async (p) => await ReBuyExecute(), (p) => !IsBusy);
        private async Task ReBuyExecute()
        {
            try
            {
                foreach (var item in OrderDetails)
                {
                    if (IsBusy) return;
                    IsBusy = true;

                    var data = await _cartItemService.AddItemToCart(new CartItemRequest
                    {
                        ProductSKU_Id = item.ProductSKU_Id,
                        Quantity = item.Quantity,
                        User_Id = 1
                    });
                    await CoreMethods.PushPageModel<CartPageModel>();
                    //await _dialogService.AlertAsync("ProductSKU_Id: " + item.ProductSKU_Id + ", Quantity: " + item.Quantity + ", User_Id: ");
                    IsBusy = false;
                }
                

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Thêm thất bại");

            }
        }
        #endregion
    }
}
