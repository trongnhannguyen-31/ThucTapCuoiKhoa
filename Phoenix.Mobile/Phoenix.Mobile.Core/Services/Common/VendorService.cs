using Phoenix.Mobile.Core.Models.Vendor;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Common;
using Phoenix.Shared.Vendor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IVendorService
    {
        Task<List<VendorModel>> GetAllVendors(VendorRequest request);
    }

    public class VendorService : IVendorService
    {
        private readonly IVendorProxy _vendorProxy;
        public VendorService(IVendorProxy vendorProxy)
        {
            _vendorProxy = vendorProxy;
        }
        public async Task<List<VendorModel>> GetAllVendors(VendorRequest request)
        {
            var vendor = await _vendorProxy.GetAllVendors(request);
            return vendor.Data.MapTo<VendorModel>();
        }
    }
}
