using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Vendor;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Phoenix.Server.Services.MainServices
{
    public interface IVendorService
    {
        Vendor GetVendorsById(int id);

        Task<BaseResponse<VendorDto>> GetAllVendors(VendorRequest request);

        Task<BaseResponse<VendorDto>> CreateVendors(VendorRequest request);

        Task<BaseResponse<VendorDto>> UpdateVendors(VendorRequest request);

        Task<BaseResponse<VendorDto>> DeleteVendors(int Id);
    }
    public class VendorService : IVendorService
    {
        private readonly DataContext _dataContext;
        public VendorService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public async Task<BaseResponse<VendorDto>> GetAllVendors(VendorRequest request)
        {
            var result = new BaseResponse<VendorDto>();
            try
            {
                //setup query
                var query = _dataContext.Vendors.AsQueryable();
                //filter
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(d => d.Name.Contains(request.Name));
                }

                if (!string.IsNullOrEmpty(request.Phone))
                {
                    query = query.Where(d => d.Phone.Contains(request.Phone));
                }

                if (request.Deleted == false)
                {
                    query = query.Where(d => d.Deleted.Equals(request.Deleted));
                }

                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<VendorDto>();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        // add hinh
        /*private readonly DataContext db = new DataContext();
        public int UploadImageInDataBase(HttpPostedFileBase file, ImageRecord imageRecordModel)
        {
            imageRecordModel.RelativePath = file; // = ConvertToBytes(file);
            var Content = new Content
            {
                Title = contentViewModel.Title,
                Description = contentViewModel.Description,
                Contents = contentViewModel.Contents,
                Image = contentViewModel.Image
            };
            db.Contents.Add(Content);
            int i = db.SaveChanges();
            if (i == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }*/
       /* public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }*/

        // Insert Vendor
        public async Task<BaseResponse<VendorDto>> CreateVendors(VendorRequest request)
        {
            var result = new BaseResponse<VendorDto>();
            try
            {
                Vendor vendors = new Vendor
                {
                    Name = request.Name,
                    Phone = request.Phone,
                    Logo = request.Logo,
                    Nation = request.Nation,
                    Deleted = false,
                    UpdatedAt = DateTime.Now,
                    CreatedAt = DateTime.Now
                };
                _dataContext.Vendors.Add(vendors);
                await _dataContext.SaveChangesAsync();

                result.Success = true;
            }
            catch(Exception ex)
            {
                result.Success = true;
                result.Message = ex.Message;
            }

            return result;
        }

        public Vendor GetVendorsById(int id) => _dataContext.Vendors.Find(id);


        #region Update
        public async Task<BaseResponse<VendorDto>> UpdateVendors(VendorRequest request)
        {
            var result = new BaseResponse<VendorDto>();
            try
            {
                var vendors = GetVendorsById(request.Id);

                vendors.Id = request.Id;
                vendors.Name = request.Name;
                vendors.Logo = request.Logo;
                vendors.Nation = request.Nation;
                vendors.Phone = request.Phone;
                vendors.Deleted = false;
                vendors.UpdatedAt = DateTime.Now;

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
        #endregion

        #region Delete
        public async Task<BaseResponse<VendorDto>> DeleteVendors(int Id)
        {
            var result = new BaseResponse<VendorDto>();
            try
            {
                var vendors = GetVendorsById(Id);

                vendors.Deleted = true;

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
        #endregion
    }
}
