using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
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
        Task<BaseResponse<WarehouseDto>> GetWarehouseByProductSKUId(WarehouseRequest request);
        Task<CrudResult> UpdateWarehouseApp(int Id, WarehouseRequest request);
    }

    public class WarehouseProxy : BaseProxy, IWarehouseProxy
    {
        public async Task<BaseResponse<WarehouseDto>> GetWarehouseByProductSKUId(WarehouseRequest request)
        {
            try
            {
                var api = RestService.For<IWarehouseApi>(GetHttpClient());
                return await api.GetWarehouseByProductSKUId(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public async Task<CrudResult> UpdateWarehouseApp(int Id, WarehouseRequest request)
        {
            try
            {
                var api = RestService.For<IWarehouseApi>(GetHttpClient());
                var result = await api.UpdateWarehouseApp(Id, request);
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CrudResult();
            }
        }

        public interface IWarehouseApi
        {
            [Post("/warehouse/GetWarehouseByProductSKUId")]
            Task<BaseResponse<WarehouseDto>> GetWarehouseByProductSKUId([Body] WarehouseRequest request);

            [Post("/warehouse/UpdateWarehouseApp")]
            Task<CrudResult> UpdateWarehouseApp(int Id, [Body] WarehouseRequest request);
        }
    }
}