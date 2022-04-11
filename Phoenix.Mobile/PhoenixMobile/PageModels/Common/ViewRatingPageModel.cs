using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Helpers;
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

        public ViewRatingPageModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public override async void Init(object initData)
        {
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
    }
}
