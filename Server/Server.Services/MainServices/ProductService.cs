using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Product;
using Phoenix.Shared.ProductSKU;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IProductService
    {
        Product GetProductsById(int id);

        Task<BaseResponse<ProductDto>> GetAllProducts(ProductRequest request);

        Task<BaseResponse<ProductDto>> CreateProducts(ProductRequest request);

        Task<BaseResponse<ProductDto>> UpdateProducts(ProductRequest request);

        Task<BaseResponse<ProductDto>> DeleteProducts(int Id);
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

                if (request.Deleted == false)
                {
                    query = query.Where(d => d.Deleted.Equals(request.Deleted));
                }

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
                    ModelCode = request.ModelCode,
                    Image1 = request.Image1,
                    Image2 = request.Image2,
                    Image3 = request.Image3,
                    Image4 = request.Image4,
                    Image5 = request.Image5,
                    Deleted = false,
                    UpdatedAt = DateTime.Now,
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

        // Create Product
        /*public async Task<BaseResponse<ProductDto>> CreateProducts(ProductRequest request)
        {
            var result = new BaseResponse<ProductDto>();
            try
            {
                Product products = new Product
                {
                    Vendor_Id = request.Vendor_Id,
                    ProductType_Id = request.ProductType_Id,
                    Name = request.Name,
                    ModelCode = request.ModelCode,
                    Image1 = request.Image1,
                    Image2 = request.Image2,
                    Image3 = request.Image3,
                    Image4 = request.Image4,
                    Image5 = request.Image5,
                    Deleted = false,
                    UpdatedAt = DateTime.Now,
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
        }*/

        public Product GetProductsById(int id) => _dataContext.Products.Find(id);

        // Update Product
        public async Task<BaseResponse<ProductDto>> UpdateProducts(ProductRequest request)
        {
            var result = new BaseResponse<ProductDto>();
            try
            {
                var products = GetProductsById(request.Id);

                products.Vendor_Id = request.Vendor_Id;
                products.ProductType_Id = request.ProductType_Id;
                products.Name = request.Name;
                products.ModelCode = request.ModelCode;
                products.Image1 = request.Image1;
                products.Image2 = request.Image2;
                products.Image3 = request.Image3;
                products.Image4 = request.Image4;
                products.Image5 = request.Image5;
                products.Deleted = false;
                products.UpdatedAt = DateTime.Now;

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

        // Delete Prodcuct
        public async Task<BaseResponse<ProductDto>> DeleteProducts(int Id)
        {
            var result = new BaseResponse<ProductDto>();
            try
            {
                var productTypes = GetProductsById(Id);

                productTypes.Deleted = true;

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

        #region
        // Create Product
        public async Task<BaseResponse<ProductDto>> CreateProducts(ProductRequest request, ImageRecord record)
        {
            var result = new BaseResponse<ProductDto>();
            try
            {
                ImageRecord imageRecords = new ImageRecord
                {
                    FileName = record.FileName,
                    RelativePath = record.RelativePath,
                    AbsolutePath = record.AbsolutePath,
                    IsExternal = false,
                    CreatedAt = DateTime.Now,
                    IsUsed = false,
                    Deleted = false,
                };
                _dataContext.ImageRecords.Add(imageRecords);

                Product products = new Product
                {
                    Vendor_Id = request.Vendor_Id,
                    ProductType_Id = request.ProductType_Id,
                    Name = request.Name,
                    ModelCode = request.ModelCode,
                    Image1 = record.Id,
                    Image2 = record.Id,
                    Image3 = record.Id,
                    Image4 = record.Id,
                    Image5 = record.Id,
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
        #endregion
    }
}
