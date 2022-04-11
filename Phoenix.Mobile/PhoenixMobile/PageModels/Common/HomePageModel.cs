using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Product;
using Phoenix.Mobile.Core.Models.ProductSKU;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Product;
using Phoenix.Shared.ProductSKU;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Phoenix.Mobile.Core.Models;
using System.Linq;

namespace Phoenix.Mobile.PageModels.Common
{
    public class HomePageModel : BasePageModel
    {
        private readonly IProductService _productService;
        private readonly IProductSKUService _productSKUService;
        private readonly IDialogService _dialogService;
        private readonly IProductMenuService _productMenuService;
        public HomePageModel(IProductService productService, IDialogService dialogService, IProductSKUService productSKUService, IProductMenuService productMenuService)
        {
            _productService = productService;
            _dialogService = dialogService;
            _productSKUService = productSKUService;
            _productMenuService = productMenuService;
        }
        
        public ObservableCollection<ProductMenuModel> Product { get; set; }
        private ProductMenuModel _selectedProduct;
        public ProductMenuModel SelectedProduct
        {
            get
            {
                return _selectedProduct;
            }
            set
            {
                _selectedProduct = value;
                if (value != null)
                    ProductSelected.Execute(value);
            }
        }

        public Command<ProductMenuModel> ProductSelected
        {
            get
            {
                return new Command<ProductMenuModel>(async (product) =>{
                    if(product.ProductType_Id == 5)
                    {
                        await CoreMethods.PushPageModel<PhoneDetailPageModel>(product);
                    }
                    else if (product.ProductType_Id == 7)
                    {
                        await CoreMethods.PushPageModel<LaptopDetailPageModel>(product);
                    }
                    
                });
            }
        }



        public override async void Init(object initData)
        {
            //Product = new ObservableCollection<ProductModel>(Products);
            Product = new ObservableCollection<ProductMenuModel>(ProductMenus);
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

            var data = await _productService.GetProductMenus(menurequest);

            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                ProductMenus = data;
                RaisePropertyChanged(nameof(ProductMenus));
            }
        }

        private double _durationSeconds;
        public double DurationSeconds
        {
            get
            {
                return _durationSeconds;
            }
            set
            {
                if (value != _durationSeconds)
                {
                    _durationSeconds = value;
                    RaisePropertyChanged();
                }
            }
        }



        #region properties
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
        public List<ProductMenuModel> ProductMenus { get; set; } = new List<ProductMenuModel>();
        public List<ProductSKUModel> ProductSKUs { get; set; } = new List<ProductSKUModel>();

        public ProductRequest request { get; set; } = new ProductRequest();
        public ProductSKURequest sKUrequest { get; set; } = new ProductSKURequest();
        public ProductMenuRequest menurequest { get; set; } = new ProductMenuRequest();
        public string SearchText { get; set; }

        #endregion

        #region ProductDetailCommand
        public Command ProductDetailCommand => new Command(async (p) => await ProductDetailExecute(), (p) => !IsBusy);
        private async Task ProductDetailExecute()
        {
            
            await CoreMethods.PushPageModel<PhoneDetailPageModel>();
            
        }
        #endregion

        #region SearchCommand
        public Command SearchCommand => new Command(async (p) => await SearchExecute(), (p) => !IsBusy);
        private async Task SearchExecute()
        {
            menurequest.Name = SearchText;
            var data = await _productService.GetProductMenus(menurequest);

            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                ProductMenus = data;
                RaisePropertyChanged(nameof(ProductMenus));
            }
        }
        #endregion

        #region ClearSearchCommand
        public Command ClearSearchCommand => new Command(async (p) => await ClearExecute(), (p) => !IsBusy);
        private async Task ClearExecute()
        {
            SearchText = "";
            menurequest.Name = SearchText;
            await LoadData();
        }
        #endregion

    }
}
