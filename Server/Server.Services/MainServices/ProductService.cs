using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IProductService
    {
        Task<BaseResponse<ProductDto>> GetAllProducts(ProductRequest request);

        Task<BaseResponse<ProductDto>> CreateProducts(ProductRequest request);
    }

    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;

        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Lấy danh sách nhà cung cấp
        public async Task<BaseResponse<ProductDto>> GetAllProducts(ProductRequest request)
        {
            var result = new BaseResponse<ProductDto>();
            try
            {
                //setup query
                var query = _dataContext.Products.AsQueryable();
                
                //filter
                /*if (!string.IsNullOrEmpty(request.Id.ToString()))
                {
                    query = query.Where(d => d.Id.ToString().Contains(request.Id.ToString()));
                }*/

                /*if (!string.IsNullOrEmpty(request.Vendor_Id.ToString()))
                {
                    query = query.Where(d => d.Vendor_Id.ToString().Contains(request.Vendor_Id.ToString()));
                }*/

                /*if (!string.IsNullOrEmpty(request.ProductType_Id.ToString()))
                {
                    query = query.Where(d => d.ProductType_Id.ToString().Contains(request.ProductType_Id.ToString()));
                }*/

                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(d => d.Name.Contains(request.Name));
                }

                /*if (!string.IsNullOrEmpty(request.Model))
                {
                    query = query.Where(d => d.Model.Contains(request.Model));
                }

                if (!string.IsNullOrEmpty(request.Price.ToString()))
                {
                    query = query.Where(d => d.Price.ToString().Contains(request.Price.ToString()));
                }*/

                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<ProductDto>();

                result.Success = true;

            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }


        // Create Product
        public async Task<BaseResponse<ProductDto>> CreateProducts(ProductRequest request)
        {
            var result = new BaseResponse<ProductDto>();
            try
            {
                Product products = new Product
                {
                    Vendor_Id = request.Vendor_Id,
                    ProductType_Id = request.ProductType_Id,
                    Name = request.Name,
                    Model = request.Model,
                    Image1 = request.Image1,
                    Image2 = request.Image2,
                    Image3 = request.Image3,
                    Image4 = request.Image4,
                    Image5 = request.Image5,
                    Deleted = false,
                    UpdatedAt = request.UpdatedAt,
                    CreatedAt = DateTime.Now
                };
                _dataContext.Products.Add(products);
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
