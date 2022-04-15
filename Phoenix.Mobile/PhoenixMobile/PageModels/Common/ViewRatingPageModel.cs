using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Services;
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
        private readonly IWorkContext _workContext;

        public ViewRatingPageModel(IDialogService dialogService, IWorkContext workContext)
        {
            _dialogService = dialogService;
            _workContext = workContext;
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

        }
    }
}
