using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.ProductSKU;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IProductSKUService
    {
        Task<BaseResponse<ProductSKUDto>> GetAllProductSKUs(ProductSKURequest request);

        Task<BaseResponse<ProductSKUDto>> CreateProductSKUs(ProductSKURequest request);
    }

    public class ProductSKUService : IProductSKUService
    {
        private readonly DataContext _dataContext;

        public ProductSKUService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Lấy danh sách nhà cung cấp
        public async Task<BaseResponse<ProductSKUDto>> GetAllProductSKUs(ProductSKURequest request)
        {
            var result = new BaseResponse<ProductSKUDto>();
            try
            {
                //setup query
                var query = _dataContext.ProductSKUs.AsQueryable();

                //filter
                if (request.Product_Id > 0)
                {
                    query = query.Where(d => d.Product_Id == request.Product_Id);
                }

                if (!string.IsNullOrEmpty(request.Screen))
                {
                    query = query.Where(d => d.Screen.Contains(request.Screen));
                }


                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<ProductSKUDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public async Task<BaseResponse<ProductSKUDto>> CreateProductSKUs(ProductSKURequest request)
        {
            var result = new BaseResponse<ProductSKUDto>();
            try
            {
                ProductSKU productSKUs = new ProductSKU
                {
                    Product_Id = request.Product_Id,
                    Screen = request.Screen,
                    OperationSystem = request.OperationSystem,
                    Processor = request.Processor,
                    Ram = request.Ram,
                    Storage = request.Storage,
                    Battery = request.Battery,
                    BackCamera = request.BackCamera,
                    FrontCamera = request.FrontCamera,
                    SimSlot = request.SimSlot,
                    GraphicCard = request.GraphicCard,
                    ConnectionPort = request.ConnectionPort,
                    Design = request.Design,
                    Size = request.Size,
                    Special = request.Special,
                    Deleted = false,
                    UpdatedAt = request.UpdatedAt,
                    CreatedAt = DateTime.Now
                };
                _dataContext.ProductSKUs.Add(productSKUs);
                await _dataContext.SaveChangesAsync();

                result.Success = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }


    }
}
