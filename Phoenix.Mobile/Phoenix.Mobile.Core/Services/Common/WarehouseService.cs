using Phoenix.Mobile.Core.Models.Warehouse;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Warehouse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IWarehouseService
    {
        Task<WarehouseModel> GetWarehouseByProductSKUId(WarehouseRequest request);
    }

    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseProxy _warehouseProxy;
        public WarehouseService(IWarehouseProxy warehouseProxy)
        {
            _warehouseProxy = warehouseProxy;
        }

        public async Task<WarehouseModel> GetWarehouseByProductSKUId(WarehouseRequest request)
        {
            var customer = await _warehouseProxy.GetWarehouseByProductSKUId(request);
            return customer.Record.MapTo<WarehouseModel>();
        }
    }
}
