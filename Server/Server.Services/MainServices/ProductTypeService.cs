using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.ProductType;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IProductTypeService
    {
        ProductType GetProductTypesById(int id);

        Task<BaseResponse<ProductTypeDto>> GetAllProductTypes(ProductTypeRequest request);

        Task<BaseResponse<ProductTypeDto>> CreateProductTypes(ProductTypeRequest request);

        Task<BaseResponse<ProductTypeDto>> UpdateProductTypes(ProductTypeRequest request);
    }
    public class ProductTypeService : IProductTypeService
    {
        private readonly DataContext _dataContext;
        public ProductTypeService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Lấy danh sách loại sản phẩm
        public async Task<BaseResponse<ProductTypeDto>> GetAllProductTypes(ProductTypeRequest request)
        {
            var result = new BaseResponse<ProductTypeDto>() { Success = true };
            try
            {
                // setup query
                var query = _dataContext.ProductTypes.AsQueryable();

                // filter

                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(d => d.Name.Contains(request.Name));
                }

                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<ProductTypeDto>();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        // Create ProductType
        public async Task<BaseResponse<ProductTypeDto>> CreateProductTypes(ProductTypeRequest request)
        {
            var result = new BaseResponse<ProductTypeDto>();
            try
            {
                ProductType productTypes = new ProductType
                {
                    Name = request.Name,
                    Deleted = false,
                    UpdatedAt = request.UpdatedAt,
                    CreatedAt = DateTime.Now
                };
                _dataContext.ProductTypes.Add(productTypes);
                await _dataContext.SaveChangesAsync();

                result.Success = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        // Get Product By Id
        public ProductType GetProductTypesById(int id) => _dataContext.ProductTypes.Find(id);

        public async Task<BaseResponse<ProductTypeDto>> UpdateProductTypes(ProductTypeRequest request)
        {
            var result = new BaseResponse<ProductTypeDto>();
            try
            {
                ProductType productTypes = new ProductType
                {
                    Name = request.Name,
                    Deleted = false,
                    UpdatedAt = request.UpdatedAt,
                    CreatedAt = DateTime.Now
                };
                //_dataContext.ProductTypes.Add(productTypes);
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
