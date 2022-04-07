//using Phoenix.Mobile.Core.Models.Warehouse;
//using Phoenix.Mobile.Core.Proxies.Common;
//using Phoenix.Mobile.Core.Utils;
//using Phoenix.Shared.Warehouse;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Phoenix.Mobile.Core.Services.Common
//{
//    public interface IWarehouseService
//    {
//        Task<List<WarehouseModel>> GetAllWarehouses(WarehouseRequest request);
//    }

//    public class WarehouseService : IWarehouseService
//    {
//        private readonly IWarehouseProxy _warehouseProxy;
//        public WarehouseService(IWarehouseProxy warehouseProxy)
//        {
//            _warehouseProxy = warehouseProxy;
//        }
//        public async Task<List<WarehouseModel>> GetAllWarehouses(WarehouserRequest request)
//        {
//            var data = await _warehouseProxy.GetAllWarehouses(request);
//            return data.MapTo<WarehouseModel>();
//        }
//    }
//}
