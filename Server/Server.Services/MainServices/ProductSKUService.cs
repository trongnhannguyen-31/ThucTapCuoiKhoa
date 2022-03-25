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
        ProductSKU GetProductSKUById(int id);

        Task<BaseResponse<ProductSKUDto>> GetAllProductSKUs(ProductSKURequest request);

        Task<BaseResponse<ProductSKUDto>> CreateProductSKUs(ProductSKURequest request);

        Task<BaseResponse<ProductSKUDto>> UpdateProductSKUs(ProductSKURequest request);
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
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        // Create ProductSKU
        public async Task<BaseResponse<ProductSKUDto>> CreateProductSKUs(ProductSKURequest request)
        {
            var result = new BaseResponse<ProductSKUDto>();
            try
            {
                ProductSKU productSKUs = new ProductSKU
                {
                    Product_Id = request.Product_Id,
                    Price = request.Price,
                    Rating = 0,
                    BuyCount = 0,
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
                    YearOfManufacture = request.YearOfManufacture,
                    Deleted = false,
                    UpdatedAt = DateTime.Now,
                    CreatedAt = DateTime.Now
                };
                _dataContext.ProductSKUs.Add(productSKUs);
                await _dataContext.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        // Get ProducutSKU ById
        public ProductSKU GetProductSKUById(int id) => _dataContext.ProductSKUs.Find(id);

        // Update ProductSKU
        public async Task<BaseResponse<ProductSKUDto>> UpdateProductSKUs(ProductSKURequest request)
        {
            var result = new BaseResponse<ProductSKUDto>();
            try
            {
                var productSKU = GetProductSKUById(request.Id);
                productSKU.Id = request.Id;
                productSKU.Product_Id = request.Product_Id;
                productSKU.Price = request.Price;
                productSKU.Screen = request.Screen;
                productSKU.OperationSystem = request.OperationSystem;
                productSKU.Processor = request.Processor;
                productSKU.Ram = request.Ram;
                productSKU.Storage = request.Storage;
                productSKU.Battery = request.Battery;
                productSKU.BackCamera = request.BackCamera;
                productSKU.FrontCamera = request.FrontCamera;
                productSKU.SimSlot = request.SimSlot;
                productSKU.GraphicCard = request.GraphicCard;
                productSKU.ConnectionPort = request.ConnectionPort;
                productSKU.Design = request.Design;
                productSKU.Size = request.Size;
                productSKU.YearOfManufacture = request.YearOfManufacture;
                productSKU.Deleted = false;
                productSKU.UpdatedAt = DateTime.Now;

                await _dataContext.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

    }
}
