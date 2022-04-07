using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Warehouse;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IWarehouseProxy
    {
        Task<List<WarehouseDto>> GetAllWarehouses(WarehouseRequest request);
    }

    public class WarehouseProxy : BaseProxy, IWarehouseProxy
    {
        public async Task<List<WarehouseDto>> GetAllWarehouses(WarehouseRequest request)
        {
            try
            {
                var api = RestService.For<IWarehouseApi>(GetHttpClient());
                var result = await api.GetAllWarehouses(request);
                if (result == null) return new List<WarehouseDto>();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }
        public interface IWarehouseApi
        {
            [Post("/warehouse/GetAllWarehouses")]
            Task<List<WarehouseDto>> GetAllWarehouses([Body] WarehouseRequest request);

        }
    }
}