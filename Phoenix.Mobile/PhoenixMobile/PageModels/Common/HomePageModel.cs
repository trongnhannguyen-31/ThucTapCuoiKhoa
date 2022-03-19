using Phoenix.Mobile.Core.Models.Vendor;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class HomePageModel : BasePageModel
    {
        private readonly IVendorService _vendorService;
        public HomePageModel(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "HomePage";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            var data = await _vendorService.GetAllVendors(new Shared.Vendor.VendorRequest 
            { 
                Name = "",
                Phone = ""
            });

            var data2 = new List<VendorModel>()
            {
                new VendorModel
                {
                    Id= 1,
                    TenNhaCungCap = "Apple",
                    Phone = "1411"
                },
                new VendorModel
                {
                    Id= 2,
                    TenNhaCungCap = "Samsung",
                    Phone = "1411"
                },
            };
        }
    }
}
