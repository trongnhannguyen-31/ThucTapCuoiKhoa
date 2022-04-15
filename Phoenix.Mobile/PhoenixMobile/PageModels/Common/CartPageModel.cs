﻿using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Core.Models.Cart;
using Phoenix.Shared.CartItem;
using Phoenix.Shared.Order;
using Phoenix.Shared.OrderDetail;
using Phoenix.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Phoenix.Mobile.Core.Models.CartItem;
using System.Linq;
using Phoenix.Mobile.Core.Models.Order;
using System.Collections.ObjectModel;
using Phoenix.Mobile.Core.Services;

namespace Phoenix.Mobile.PageModels.Common
{
    public class CartPageModel : BasePageModel
    {
        private readonly ICartItemService _cartItemService;
        private readonly IDialogService _dialogService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IWorkContext _workContext;

        public CartPageModel(ICartItemService cartItemService, IDialogService dialogService, IOrderDetailService orderDetailService, IOrderService orderService, ICustomerService customerService, IWorkContext workContext)
        {
            _cartItemService = cartItemService;
            _dialogService = dialogService;
            _orderDetailService = orderDetailService;
            _orderService = orderService;
            _customerService = customerService;
            _workContext = workContext;
        }
        public ObservableCollection<CartListModel> ItemCart { get; set; }

        public override async void Init(object initData)
        {
            ItemCart = new ObservableCollection<CartListModel>(CartList);
            // base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Giỏ hàng";
        }

        


        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        protected override async void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
            await SaveCart();
            //await _dialogService.AlertAsync("hehehe");
        }

        private async Task LoadData()
        {
            var token = _workContext.Token;
            UserId = token.UserId;
            request.UserID = UserId;
             var data = await _cartItemService.GetAllCartItems(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                CartList = data;
                updateTotalPrice();
                RaisePropertyChanged(nameof(CartList));
            }
        }

        private double totalPrice;

        public double TotalPrice
        {
            get
            {
                return totalPrice;
            }

            set
            {
                totalPrice = value;
            }
        }

        private void updateTotalPrice()
        {
            //using Linq
            double total = CartList.Sum(p => p.Total);

            TotalPrice = total;
        }

        private async Task SaveCart()
        {
            if(CartList != null)
            {
                foreach (var item in CartList)
                {
                    try
                    {
                        if (IsBusy) return;
                        IsBusy = true;

                        var data = await _cartItemService.UpdateCart(item.Id, new CartItemRequest
                        {
                            Id = item.Id,
                            ProductSKU_Id = item.ProductSKUId,
                            Quantity = item.Quantity,
                            User_Id = item.UserID
                        });
                        //await _dialogService.AlertAsync("Cập nhật thành công");
                        IsBusy = false;

                    }
                    catch (Exception e)
                    {
                        await _dialogService.AlertAsync("Cập nhật thất bại");
                    }
                }
            }
            
        }

        #region properties
        public List<CartListModel> CartList { get; set; } = new List<CartListModel>();
        public OrderModel InsertedOrder { get; set; } = new OrderModel();
        public CartListRequest request { get; set; } = new CartListRequest();
        public OrderAppRequest orderRequest { get; set; } = new OrderAppRequest();

        public int Id { get; set; }
        public int UserId { get; set; }
        public int _testNumber1=0;
        public int _testNumber2=0;

        public int TestNumber1
        {
            get
            {
                return _testNumber1;
            }
            set
            {
                if (_testNumber1 != value)
                {
                    _testNumber1 = value;
                }
            }
        }
        public int TestNumber2
        {
            get
            {
                return _testNumber2;
            }
            set
            {
                if(_testNumber2 != value)
                {
                    _testNumber2 = value;
                }
                
            }
        }

        //public int Uaer_Id { get; set; }
        #endregion

        #region AddTest
        public Command AddTest => new Command(async (p) => await AddTestExecute(), (p) => !IsBusy);
        private async Task AddTestExecute()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;
                var data = _orderService.AddOrder(new OrderAppRequest
                {
                    OrderDate = DateTime.Now,
                    Status = "Chờ xử lý",
                    DeliveryDate = null,
                    Address = "abc",
                    Total = CartList.Sum(item => item.Total),
                    Customer_Id = 1,
                    CreatedAt = DateTime.Now,
                    Deleted = false
                });
                //await _dialogService.AlertAsync("Thêm thành công");
                //IsBusy = false;

                var data2 = await _orderService.GetLatestOrder(orderRequest);
                if (data == null)
                {
                    await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
                }
                else
                {
                    InsertedOrder = data2;
                    RaisePropertyChanged(nameof(InsertedOrder));
                }

                foreach (var item in CartList)
                {
                    try
                    {
                        //if (IsBusy) return;
                        //IsBusy = true;

                        var data3 = _orderDetailService.AddOrderDetail(new OrderDetailAppRequest
                        {
                            Order_Id = InsertedOrder.Id,
                            ProductSKU_Id = item.ProductSKUId,
                            Price = item.Price,
                            Quantity = item.Quantity
                        });
                        IsBusy = false;

                    }
                    catch (Exception e)
                    {
                        await _dialogService.AlertAsync("Thêm Order Detail thất bại");

                    }
                }
                IsBusy = false;
            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Thêm Order thất bại");
            }
            await _dialogService.AlertAsync("Thêm thành công");
        }
        #endregion

        private CartListModel _selectedItem;
        public CartListModel SelectedItem
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

        #region RemoveItemCommand
        public Command RemoveItemCommand => new Command(async (p) => await RemoveItemExecute(), (p) => !IsBusy);
        private async Task RemoveItemExecute()
        {
            //await CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn:" + request.Id, "Đóng");
            // await CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn:" + _selectedItem.Id, "Đóng");
            try
            {
                if(SelectedItem == null)
                {
                    await _dialogService.AlertAsync("Vui lòng chọn sản phẩm để xóa");
                }
                else
                {
                    if (IsBusy) return;
                    IsBusy = true;

                    var data = await _cartItemService.RemoveItemFromCart(SelectedItem.Id);
                    //await CoreMethods.PushPageModel<CartPageModel>();
                    //await _dialogService.AlertAsync("Xóa thành công" + request.Id);
                    IsBusy = false;
                    LoadData();
                }
                

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Xóa thất bại");
            }
            //if (IsBusy) return;
            //IsBusy = true;
            //// var result = await _cartItemService.RemoveItemFromCart(Id);

            ////await CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn:" + SelectedItem.Id, "Đóng");
            //IsBusy = false;
        }
        #endregion


        #region ConfirmPageCommand
        public Command ConfirmPageCommand => new Command(async (p) => await ConfirmPageExecute(), (p) => !IsBusy);
        private async Task ConfirmPageExecute()
        {
            if(CartList.Count < 1)
            {
                await _dialogService.AlertAsync("Vui lòng thêm sản phẩm vào giỏ hàng.");
            }
            else
            {
                await CoreMethods.PushPageModel<CheckOutPageModel>();
            }
           

        }
        #endregion

        #region TestCommand
        public Command TestCommand => new Command(async (p) => await TestCommandExecute(), (p) => !IsBusy);

        

        private async Task TestCommandExecute()
        {
            //ait CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn:" + , "Đóng");
            updateTotalPrice();

        }
        #endregion

        ObservableCollection<CartListDto> _listCartItem;
        public ObservableCollection<CartListDto> ListCartItem
        {
            get { return _listCartItem; }
            set { _listCartItem = value; }
        }

        #region Refresh
        public Command Refresh => new Command(async (p) => await RefreshExecute(), (p) => !IsBusy);
        private async Task RefreshExecute()
        {
            LoadData();

        }
        #endregion
    }
}
