using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Vendor;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IVendorProxy
    {
        Task<List<VendorDto>> GetAllVendors(VendorRequest request);
    }

    public class VendorProxy : BaseProxy, IVendorProxy
    {
        public async Task<List<VendorDto>> GetAllVendors(VendorRequest request)
        {
            try
            {
                var api = RestService.For<IVendorApi>(GetHttpClient());
                var result = await api.GetAllVendors(request);
                if (result == null) return new List<VendorDto>();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new List<VendorDto>();
            }
        }
        public interface IVendorApi
        {
            [Post("/vendor/GetAllVendors")]
            Task<List<VendorDto>> GetAllVendors([Body] VendorRequest request);

        }
    }
}
