using Falcon.Web.Core.Helpers;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Customer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface ICustomerService
    {
        Task<BaseResponse<CustomerDto>> GetAllCustomers(CustomerRequest request);
    }
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _dataContext;
        public CustomerService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Lấy danh sách khách hàng
        public async Task<BaseResponse<CustomerDto>> GetAllCustomers(CustomerRequest request)
        {
            var result = new BaseResponse<CustomerDto>();
            try
            {
                // setup query
                var query = _dataContext.Customers.AsQueryable();

                // filter
                if (!string.IsNullOrEmpty(request.FullName))
                {
                    query = query.Where(d => d.FullName.Contains(request.FullName));
                }

                if (!string.IsNullOrEmpty(request.Address))
                {
                    query = query.Where(d => d.Address.Contains(request.Address));
                }

                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<CustomerDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
