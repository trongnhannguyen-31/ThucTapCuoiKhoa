using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
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
        Task<BaseResponse<VendorDto>> GetAllVendors(VendorRequest request);
    }

    public class VendorProxy : BaseProxy, IVendorProxy
    {
        public async Task<BaseResponse<VendorDto>> GetAllVendors(VendorRequest request)
        {
            try
            {
                var api = RestService.For<IVendorApi>(GetHttpClient());
                //List<VendorDto> result = await api.GetAllVendors(request);
                //if (result == null) return new List<VendorDto>();
                return await api.GetAllVendors(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }
        public interface IVendorApi
        {
            [Post("/vendor/GetAllVendors")]
            Task<BaseResponse<VendorDto>> GetAllVendors([Body] VendorRequest request);

        }
    }
}