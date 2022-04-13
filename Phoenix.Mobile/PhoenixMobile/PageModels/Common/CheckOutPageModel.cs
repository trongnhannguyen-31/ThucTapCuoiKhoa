using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Cart;
using Phoenix.Mobile.Core.Models.CartItem;
using Phoenix.Mobile.Core.Models.Customer;
using Phoenix.Mobile.Core.Models.Order;
using Phoenix.Mobile.Core.Services;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.CartItem;
using Phoenix.Shared.Customer;
using Phoenix.Shared.Order;
using Phoenix.Shared.OrderDetail;
using Phoenix.Shared.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class CheckOutPageModel : BasePageModel
    {
        private readonly ICartItemService _cartItemService;
        private readonly IDialogService _dialogService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderService _orderService;
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;
        private readonly IWarehouseService _warehouseService;

        public CheckOutPageModel(ICartItemService cartItemService, IDialogService dialogService, IOrderDetailService orderDetailService, IOrderService orderService, IWorkContext workContext, ICustomerService customerService, IWarehouseService warehouseService)
        {
            _cartItemService = cartItemService;
            _dialogService = dialogService;
            _orderDetailService = orderDetailService;
            _orderService = orderService;
            _workContext = workContext;
            _customerService = customerService;
            _warehouseService = warehouseService;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Thanh toán";
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

        #region properties
        public List<CartListModel> CartList { get; set; } = new List<CartListModel>();
        public OrderModel InsertedOrder { get; set; } = new OrderModel();
        public CartListRequest request { get; set; } = new CartListRequest();
        public OrderAppRequest orderRequest { get; set; } = new OrderAppRequest();
        public CustomerRequest customerRequest { get; set; } = new CustomerRequest();
        public WarehouseRequest warehouseRequest { get; set; } = new WarehouseRequest();
        public CustomerModel Customer { get; set; } = new CustomerModel();

        public int Id { get; set; }
        public int UserId { get; set; }
        #endregion

        #region AddOrder
        public Command AddOrder => new Command(async (p) => await AddOrderExecute(), (p) => !IsBusy);
        private async Task AddOrderExecute()
        {            
            try
            {
                if (IsBusy) return;
                IsBusy = true;
                customerRequest.zUser_Id = UserId;
                var data6 = await _customerService.GetCustomerApptById(customerRequest);
                var data = _orderService.AddOrder(new OrderAppRequest
                {
                    OrderDate = DateTime.Now,
                    Status = "Chờ xử lý",
                    DeliveryDate = null,
                    Address = "abc",
                    //Total = CartList.Sum(item => item.Total),
                    Total = TotalPrice,
                    IsRated = false,
                    Customer_Id = data6.Id,
                    CreatedAt = DateTime.Now,
                    Deleted = false
                });
                IsBusy = false;

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
                        if (IsBusy) return;
                        IsBusy = true;

                        var data3 = _orderDetailService.AddOrderDetail(new OrderDetailAppRequest
                        {
                            Order_Id = InsertedOrder.Id,
                            ProductSKU_Id = item.ProductSKUId,
                            Price = item.Price,
                            Quantity = item.Quantity
                        });
                        warehouseRequest.ProductSKU_Id = item.ProductSKUId;
                        var data5 = await _warehouseService.GetWarehouseByProductSKUId(warehouseRequest);
                        var data7 = await _warehouseService.UpdateWarehouseApp(data5.Id, new WarehouseRequest
                        {
                            Id = data5.Id,
                            ProductSKU_Id = data5.ProductSKU_Id,
                            Quantity = data5.Quantity,
                            NewQuantity = item.Quantity//,
                           // UpdatedAt = DateTime.Now
                        });
                        IsBusy = false;
                    }
                    catch (Exception e)
                    {
                        await _dialogService.AlertAsync("Thêm Order Detail thất bại");

                    }
                }
                IsBusy = false;
                if (IsBusy) return;
                IsBusy = true;
                //request.UserID = 1;
                var data4 = await _cartItemService.ClearCart(request.UserID = UserId);
                await CoreMethods.PushPageModel<CartPageModel>();
                IsBusy = false;

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Thêm Order thất bại");
            }
            await _dialogService.AlertAsync("Thêm thành công");
        }
        #endregion
    }
}
