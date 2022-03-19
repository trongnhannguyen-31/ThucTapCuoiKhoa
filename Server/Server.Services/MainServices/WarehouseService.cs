using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.ProductSKU;
using Phoenix.Shared.Warehouse;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IWarehouseService
    {
        Task<BaseResponse<WarehouseDto>> GetAllWarehouses(WarehouseRequest request);
        Task<BaseResponse<WarehouseDto>> CreateWarehouses(WarehouseRequest request);
    }
    public class WarehouseService : IWarehouseService
    {
        private readonly DataContext _dataContext;
        public WarehouseService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public async Task<BaseResponse<WarehouseDto>> GetAllWarehouses(WarehouseRequest request)
        {
            var result = new BaseResponse<WarehouseDto>();
            try
            {
                //setup query
                var query = _dataContext.Warehouses.AsQueryable();

                //filter
                if (request.Id > 0)
                {
                    query = query.Where(d => d.Id == request.Id);
                }

                if (request.ProductSKU_Id > 0)
                {
                    query = query.Where(d => d.ProductSKU_Id == request.ProductSKU_Id);
                }

                if (request.Quantity > 0)
                {
                    query = query.Where(d => d.Quantity == request.Quantity);
                }

                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<WarehouseDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public async Task<BaseResponse<WarehouseDto>> CreateWarehouses(WarehouseRequest request)
        {
            var result = new BaseResponse<WarehouseDto>();
            try
            {
                Warehouse warehouses = new Warehouse
                {
                    ProductSKU_Id = request.ProductSKU_Id,
                    Quantity = request.Quantity
                };
                _dataContext.Warehouses.Add(warehouses);
                await _dataContext.SaveChangesAsync();

                result.success = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
