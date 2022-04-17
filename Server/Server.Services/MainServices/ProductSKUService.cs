using AutoMapper;
using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
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
        // Nhap
        Product GetProductById(int id);
        ProductSKU GetProductSKUById(int id);
        Task<BaseResponse<ProductSKUDto>> GetAllProductSKUs(ProductSKURequest request);
        Task<BaseResponse<ProductSKUDto>> CreateProductSKUs(ProductSKURequest request);
        Task<BaseResponse<ProductSKUDto>> UpdateProductSKUs(ProductSKURequest request);

        Task<BaseResponse<ProductSKUDto>> UpdateBuyCountSKUs(ProductSKURequest request);

        Task<BaseResponse<ProductSKUDto>> GetAllProductSKUById(int id, ProductSKURequest request);
        Task<BaseResponse<ProductSKUDto>> DeleteProductSKUs(int Id);
        ///
        //Task<BaseResponse<ProductSKUDto>> GetProductById(ProductSKURequest request);
        Task<BaseResponse<ProductSKUAppDto>> GetProductById(ProductSKURequest request);
        Task<CrudResult> UpdateProductSKUApp(int Id, ProductSKURequest request);
    }

    public class ProductSKUService : IProductSKUService
    {
        private readonly DataContext _dataContext;

        public ProductSKUService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Get List ProductSKU
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

                if (request.Deleted == false)
                {
                    query = query.Where(d => d.Deleted.Equals(request.Deleted));
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
                Warehouse warehouses = new Warehouse
                {
                    ProductSKU_Id = request.Id,
                    Quantity = 0,
                };

                _dataContext.ProductSKUs.Add(productSKUs);
                _dataContext.Warehouses.Add(warehouses);

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

        //public async Task<BaseResponse<ProductSKUDto>> UpdateBuyCountSKUs(ProductSKURequest request)
        //{
        //    var result = new BaseResponse<ProductSKUDto>();
        //    try
        //    {
        //        var productSKU = GetProductSKUById(request.Id);

        //        productSKU.Id = request.Id;
        //        productSKU.BuyCount = request.BuyCount + request.NewBuy;
        //        productSKU.UpdatedAt = DateTime.Now;

        //        await _dataContext.SaveChangesAsync();
        //        result.Success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Success = false;
        //        result.Message = ex.Message;
        //    }

        //    return result;
        //}
        public async Task<BaseResponse<ProductSKUDto>> UpdateBuyCountSKUs(ProductSKURequest request)
        {
            var result = new BaseResponse<ProductSKUDto>();
            try
            {
                var warehouses = GetProductSKUById(request.Id);

                warehouses.Id = warehouses.Id;
                warehouses.BuyCount = warehouses.BuyCount + request.NewBuy;

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

        public async Task<BaseResponse<ProductSKUDto>> DeleteProductSKUs(int Id)
        {
            var result = new BaseResponse<ProductSKUDto>();
            try
            {
                var productSKUs = GetProductSKUById(Id);

                productSKUs.Deleted = true;

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
        public Product GetProductById(int id) => _dataContext.Products.Find(id);

        public async Task<BaseResponse<ProductSKUDto>> GetAllProductSKUById(int id, ProductSKURequest request)
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

                if (!string.IsNullOrEmpty(request.Ram))
                {
                    query = query.Where(d => d.Ram.Contains(request.Ram));
                }

                if (!string.IsNullOrEmpty(request.Screen))
                {
                    query = query.Where(d => d.Screen.Contains(request.Screen));
                }

                if (request.Deleted == false)
                {
                    query = query.Where(d => d.Deleted.Equals(request.Deleted));
                }

                query = query.OrderByDescending(d => d.Id);

                var list = _dataContext.ProductSKUs.Where(p => p.Product_Id.Equals(id));

                var data = await list.ToListAsync();
                result.Data = data.MapTo<ProductSKUDto>();
                result.Success = true;
            
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        #endregion

        #region GetProductById
        public async Task<BaseResponse<ProductSKUAppDto>> GetProductById(ProductSKURequest request)
        {
            var result = new BaseResponse<ProductSKUAppDto>();
            try
            {
                var query = (from p in _dataContext.Products
                             join s in _dataContext.ProductSKUs on p.Id equals s.Product_Id
                             join i1 in _dataContext.ImageRecords on p.Image1 equals i1.Id
                             join i2 in _dataContext.ImageRecords on p.Image2 equals i2.Id
                             join i3 in _dataContext.ImageRecords on p.Image3 equals i3.Id
                             join i4 in _dataContext.ImageRecords on p.Image4 equals i4.Id
                             join i5 in _dataContext.ImageRecords on p.Image5 equals i5.Id
                             select new
                             {
                                 Id = s.Id,
                                 Product_Id = s.Product_Id,
                                 Image1Path = i1.AbsolutePath,
                                 Image2Path = i2.AbsolutePath,
                                 Image3Path = i3.AbsolutePath,
                                 Image4Path = i4.AbsolutePath,
                                 Image5Path = i5.AbsolutePath,
                                // NameProduct = p.Name,
                                 Price = s.Price,
                                 Rating = s.Rating,
                                 BuyCount = s.BuyCount,
                                 Screen = s.Screen,
                                 OperationSystem = s.OperationSystem,
                                 Processor = s.Processor,
                                 Ram = s.Rating,
                                 Storage = s.Storage,
                                 Battery = s.Battery,
                                 BackCamera = s.BackCamera,
                                 FrontCamera = s.FrontCamera,
                                 SimSlot = s.SimSlot,
                                 GraphicCard = s.GraphicCard,
                                 ConnectionPort = s.ConnectionPort,
                                 Design = s.Design,
                                 Size = s.Size,
                                 YearOfManufacture = s.YearOfManufacture,
                                 CreatedAt = s.CreatedAt,
                                 UpdatedAt = s.UpdatedAt,
                                 Deleted = s.Deleted
                             }).AsQueryable();

                var config = new MapperConfiguration(cfg => cfg.CreateMissingTypeMaps = true);
                var mapper = config.CreateMapper();
               var listcart = query.Select(mapper.Map<ProductSKUAppDto>);
                var data = listcart.First(d=> d.Id == request.Id);
                result.Record = data.MapTo<ProductSKUAppDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        #region UpdateProductSKUApp
        public async Task<CrudResult> UpdateProductSKUApp(int Id, ProductSKURequest request)
        {
            var productSKU = _dataContext.ProductSKUs.Find(Id);
            productSKU.BuyCount += request.NewBuy;

            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
        #endregion
    }
}
