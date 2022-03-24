using Falcon.Web.Core.Helpers;
using Phoenix.Server.Services.MainServices;
using Phoenix.Shared.Common;
using Phoenix.Shared.Product;
using Phoenix.Shared.ProductSKU;
using Phoenix.Shared.ProductType;
using Phoenix.Shared.Vendor;
using Phoenix.Shared.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.ApiServices
{
    public interface IDropdownService
    {
        Task<List<DropdownDto>> TakeAllProductTypes();

        Task<List<DropdownDto>> TakeAllVendors();

        Task<List<DropdownDto>> TakeAllProductSKUs();

        Task<List<DropdownDto>> TakeAllProducts();
    }

    public class DropdownService : IDropdownService
    {
        private readonly IProductTypeService _productTypeService;
        private readonly IVendorService _vendorService;
        private readonly IProductSKUService _productSKUService;
        private readonly IProductService _productService;

        public DropdownService(IProductTypeService productTypeService, IVendorService vendorService, IProductSKUService productSKUService, IProductService productService)
        {
            _productTypeService = productTypeService;
            _vendorService = vendorService;
            _productSKUService = productSKUService;
            _productService = productService;
        }

        public async Task<List<DropdownDto>> TakeAllProductTypes()
        {
            var data = await _productTypeService.GetAllProductTypes(new ProductTypeRequest { PageSize = int.MaxValue });
            if (data.Success)
            {
                return data.Data.MapTo<DropdownDto>();
            }
            return null;
        }

        public async Task<List<DropdownDto>> TakeAllVendors()
        {
            var data = await _vendorService.GetAllVendors(new VendorRequest { PageSize = int.MaxValue });
            if (data.Success)
            {
                return data.Data.MapTo<DropdownDto>();
            }
            return null;
        }

        public async Task<List<DropdownDto>> TakeAllProductSKUs()
        {
            var data = await _productSKUService.GetAllProductSKUs(new ProductSKURequest { PageSize = int.MaxValue });
            if (data.Success)
            {
                return data.Data.MapTo<DropdownDto>();
            }
            return null;
        }

        public async Task<List<DropdownDto>> TakeAllProducts()
        {
            var data = await _productService.GetAllProducts(new ProductRequest { PageSize = int.MaxValue });
            if (data.Success)
            {
                return data.Data.MapTo<DropdownDto>();
            }
            return null;
        }
    }
}
