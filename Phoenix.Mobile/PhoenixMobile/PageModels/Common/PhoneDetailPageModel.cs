using Phoenix.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
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
using Phoenix.Mobile.Core.Models.Rating;
using Phoenix.Shared.Rating;
using Phoenix.Mobile.Core.Services;
using Phoenix.Mobile.Core.Models.ProductSKU;

namespace Phoenix.Mobile.PageModels.Common
{
    public class PhoneDetailPageModel : BasePageModel
    {
        private readonly IProductSKUService _productSKUService;
        private readonly IDialogService _dialogService;
        private readonly ICartItemService _cartItemService;
        private readonly IProductService _productService;
        private readonly IRatingService _ratingService;
        private readonly IWorkContext _workContext;


        public PhoneDetailPageModel(IProductSKUService productSKUService, IDialogService dialogService, IProductService productService, ICartItemService cartItemService, IRatingService ratingService, IWorkContext workContext)
        {
            _productSKUService = productSKUService;
            _dialogService = dialogService;
            _productService = productService;
            _cartItemService = cartItemService;
            _ratingService = ratingService;
            _workContext = workContext;

        }
        //public ProductDetailPageModel ProductDetail { get; set; }

        public ProductMenuModel Product { get; set; }
        //public List<string> images = new List<string>();

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
            var token = _workContext.Token;
            UserId = token.UserId;
            //Load thông tin chi tiết điện thoại
            request.Id = Product.SKUId;
            var data = await _productSKUService.GetProductById(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                ProductSKUs = data;
                RaisePropertyChanged(nameof(ProductSKUs));

                listImage.Add(ProductSKUs.Image1Path);
                listImage.Add(ProductSKUs.Image2Path);
                listImage.Add(ProductSKUs.Image3Path);
                listImage.Add(ProductSKUs.Image4Path);
                listImage.Add(ProductSKUs.Image5Path);

                ratingRequest.ProductSKU_Id = ProductSKUs.Id;
                var data2 = await _ratingService.GetRatingByProductSKUId(ratingRequest);
                if (data2 == null || data2.Count < 1)
                {
                    LabelVisible = true;
                    RatingListVisible = false;
                }
                else
                {
                    Ratings = data2;
                    LabelVisible = false;
                    RatingListVisible = true;
                    RaisePropertyChanged(nameof(Ratings));
                    
                }
                
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
        public ProductSKURequest request { get; set; } = new ProductSKURequest();
        //public List<ProductModel> SameVendors { get; set; } = new List<ProductModel>();
        //public List<ProductModel> SameTypes { get; set; } = new List<ProductModel>();


        // public ProductRequest sameVendorRequest { get; set; } = new ProductRequest();
        //public ProductRequest sameTypeRequest { get; set; } = new ProductRequest();
        public List<string> listImage { get; set; } = new List<string>();
        public List<RatingModel> Ratings { get; set; } = new List<RatingModel>();
        public RatingAppRequest ratingRequest { get; set; } = new RatingAppRequest();
        public bool LabelVisible { get; set; }
        public bool RatingListVisible { get; set; }

        public int UserId{ get; set; }
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
                    await _dialogService.AlertAsync("Vui lòng chọn số lượng");
                    IsBusy = false;
                    return;
                }

                var data =  _cartItemService.AddItemToCart(new CartItemRequest
                {
                    ProductSKU_Id = ProductSKUs.Id,
                    Quantity = (int)ProductQuantity,
                    User_Id = UserId
                });

                //await CoreMethods.PushPageModel<PhoneDetailPageModel>();
                //await _dialogService.AlertAsync("Thêm thành công");
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
