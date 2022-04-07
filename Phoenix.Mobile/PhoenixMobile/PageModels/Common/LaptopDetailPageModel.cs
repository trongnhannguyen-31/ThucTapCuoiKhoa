using Phoenix.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Phoenix.Mobile.Core.Models.ProductSKU;
using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Product;
using Phoenix.Mobile.Core.Models.Cart;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Shared.ProductSKU;
using Phoenix.Shared.Product;
using Phoenix.Shared.CartItem;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Phoenix.Mobile.Core.Models;

namespace Phoenix.Mobile.PageModels.Common
{
    public class LaptopDetailPageModel : BasePageModel
    {
        private readonly IProductSKUService _productSKUService;
        private readonly IDialogService _dialogService;
        private readonly ICartItemService _cartItemService;
        private readonly IProductService _productService;
        //Thêm vô giỏ
        //
        public LaptopDetailPageModel(IProductSKUService productSKUService, IDialogService dialogService, IProductService productService, ICartItemService cartItemService)
        {
            _productSKUService = productSKUService;
            _dialogService = dialogService;
            _productService = productService;
            _cartItemService = cartItemService;

        }
        //public ProductDetailPageModel ProductDetail { get; set; }

        public ProductMenuModel Product { get; set; }

        public override async void Init(object initData)
        {
            if (initData != null)
            {
                Product = (ProductMenuModel)initData;
            }
            else
            {
                Product = new ProductMenuModel();
            }
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Chi tiết sản phẩm";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
            //Load thông tin chi tiết điện thoại
            request.Id = Product.SKUId;
            var data = await _productSKUService.GetProductById(request);
            //var sameVendor = await _productService.GetAllProducts(sameVendorRequest);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                //Vendors = data;

                ProductSKUs = data;
                //SameVendors = sameVendor;
                //RaisePropertyChanged("Customers");
                RaisePropertyChanged(nameof(ProductSKUs));
                //RaisePropertyChanged(nameof(SameVendors));
            }
        }

        private double _productQuantity;
        public double ProductQuantity
        {
            get
            {
                return _productQuantity;
            }
            set
            {
                if (value != _productQuantity)
                {
                    _productQuantity = value;
                    RaisePropertyChanged();
                }
            }
        }


        #region properties
        //public List<ProductSKUModel> ProductSKUs { get; set; } = new List<ProductSKUModel>();
        public ProductSKUModel ProductSKUs { get; set; } = new ProductSKUModel();
        public List<ProductModel> SameVendors { get; set; } = new List<ProductModel>();
        public List<ProductModel> SameTypes { get; set; } = new List<ProductModel>();

        public ProductSKURequest request { get; set; } = new ProductSKURequest();
        public ProductRequest sameVendorRequest { get; set; } = new ProductRequest();
        public ProductRequest sameTypeRequest { get; set; } = new ProductRequest();

        #endregion

        #region QuantityButton
        public Command QuantityButton => new Command(async (p) => await QuantityExecute(), (p) => !IsBusy);
        private async Task QuantityExecute()
        {
            await _dialogService.AlertAsync("Hello" + ProductQuantity + ProductSKUs.Id, "Lỗi", "OK");
        }
        #endregion

        #region AddItemToCart
        public Command AddItemToCart => new Command(async (p) => await AddItemExecute(), (p) => !IsBusy);
        private async Task AddItemExecute()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;
                if (ProductQuantity <= 0)
                {
                    await _dialogService.AlertAsync("Vui chọn số lượng");
                    IsBusy = false;
                    return;
                }

                var data = await _cartItemService.AddItemToCart(new CartItemRequest
                {
                    ProductSKU_Id = ProductSKUs.Id,
                    Quantity = (int)ProductQuantity,
                    User_Id = 1
                });
                //await CoreMethods.PushPageModel<PhoneDetailPageModel>();
                await _dialogService.AlertAsync("Thêm thành công");
                IsBusy = false;

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Thêm thất bại");

            }
        }
        #endregion
    }
}
