using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.WarehouseMenu;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IWarehouseMenuService
    {
        WarehouseMenu GetWarehouseMenusById(int id);

        Task<BaseResponse<WarehouseMenuDto>> GetAllWarehouseMenus(WarehouseMenuRequest request);
    }

    public class WarehouseMenuService : IWarehouseMenuService
    {
        private readonly DataContext _dataContext;
        public WarehouseMenuService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<BaseResponse<WarehouseMenuDto>> GetAllWarehouseMenus(WarehouseMenuRequest request)
        {
            var result = new BaseResponse<WarehouseMenuDto>();
            try
            {
                //setup query
                var query = _dataContext.WarehouseMenus.AsQueryable();

                //filter
                if (request.WarehouseId > 0)
                {
                    query = query.Where(d => d.WarehouseId == request.WarehouseId);
                }

                if (request.SKUId > 0)
                {
                    query = query.Where(d => d.SKUId == request.SKUId);
                }

                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(d => d.Name.Contains(request.Name));
                }

                /* if (request.ProductSKU_Id > 0)
                 {
                     query = query.Where(d => d.ProductSKU_Id == request.ProductSKU_Id);
                 }

                 if (request.Quantity > 0)
                 {
                     query = query.Where(d => d.Quantity == request.Quantity);
                 }*/

                query = query.OrderByDescending(d => d.WarehouseId);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<WarehouseMenuDto>();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public WarehouseMenu GetWarehouseMenusById(int id) => _dataContext.WarehouseMenus.Find(id);

    }
}
