using Falcon.Web.Core.Helpers;
using Phoenix.Server.Services.MainServices;
using Phoenix.Shared.Common;
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

        Task<List<DropdownDto>> TakeAllWarehouses();

        Task<List<DropdownDto>> TakeAllProductSKUs();
    }

    public class DropdownService : IDropdownService
    {
        private readonly IProductTypeService _productTypeService;
        private readonly IVendorService _vendorService;
        private readonly IWarehouseService _warehouseService;
        private readonly IProductSKUService _productSKUService;

        public DropdownService(IProductTypeService productTypeService, IVendorService vendorService, IWarehouseService warehouseService, IProductSKUService productSKUService)
        {
            _productTypeService = productTypeService;
            _vendorService = vendorService;
            _warehouseService = warehouseService;
            _productSKUService = productSKUService;

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

        public async Task<List<DropdownDto>> TakeAllWarehouses()
        {
            var data = await _warehouseService.GetAllWarehouses(new WarehouseRequest { PageSize = int.MaxValue });
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
    }
}
