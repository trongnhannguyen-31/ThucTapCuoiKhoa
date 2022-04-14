﻿using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
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
        Customer GetCustomersById(int id);

        Task<BaseResponse<CustomerDto>> GetAllCustomers(CustomerRequest request);

        Task<BaseResponse<CustomerDto>> UpdateCustomers(CustomerRequest request);

        Task<BaseResponse<CustomerDto>> DeleteCustomersById(int Id);

        Task<BaseResponse<CustomerDto>> GetCustomerApptById(CustomerRequest request);

        Task<CrudResult> UpdateCustomerDetail(int Id, CustomerRequest request);
        Task<CrudResult> AddCustomerDetail(CustomerRequest request);

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

        // Get Customer By Id
        public Customer GetCustomersById(int id) => _dataContext.Customers.Find(id);

        // Update Customer
        public async Task<BaseResponse<CustomerDto>> UpdateCustomers(CustomerRequest request)
        {
            var result = new BaseResponse<CustomerDto>();
            try
            {

                var customers = GetCustomersById(request.Id);
                customers.FullName = request.FullName;
                customers.Address = request.Address;
                customers.Phone = request.Phone;
                customers.Email = request.Email;

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

        // Deleted Customer
        public async Task<BaseResponse<CustomerDto>> DeleteCustomersById(int Id)
        {
            var result = new BaseResponse<CustomerDto>();
            try
            {

                var customers = GetCustomersById(Id);
                _dataContext.Customers.Remove(customers);
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

        #region GetCustomerApptById
        public async Task<BaseResponse<CustomerDto>> GetCustomerApptById(CustomerRequest request)
        {
            var result = new BaseResponse<CustomerDto>();
            try
            {

                //setup query
                var query = _dataContext.Customers.AsQueryable();
                //filter
                //var data = await query.FirstOrDefaultAsync(d => d.Id == request.Id);
                var data = await query.FirstOrDefaultAsync(d => d.zUser_Id == request.zUser_Id);

                //var data = await query.FindAsync(request.Id);
                result.Record = data.MapTo<CustomerDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        #region UpdateCustomerDetail
        public async Task<CrudResult> UpdateCustomerDetail(int Id, CustomerRequest request)
        {
            var Customer = _dataContext.Customers.Find(Id);
            Customer.FullName = request.FullName;
            Customer.Gender = request.Gender;
            Customer.Birthday = request.Birthday;
            Customer.Phone = request.Phone;
            Customer.Email = request.Email;
            Customer.Address = request.Address;
            Customer.zUser_Id = request.zUser_Id;

            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
        #endregion

        #region AddUserDetail
        public async Task<CrudResult> AddCustomerDetail(CustomerRequest request)
        {
            var Customer = new Customer();
            Customer.FullName = request.FullName;
            Customer.Gender = request.Gender;
            Customer.Birthday = request.Birthday;
            Customer.Phone = request.Phone;
            Customer.Email = request.Email;
            Customer.Address = request.Address;
            Customer.zUser_Id = request.zUser_Id;

            _dataContext.Customers.Add(Customer);

            await _dataContext.SaveChangesAsync();
            //int a = Order.Id;
            return new CrudResult() { IsOk = true };
        }
        #endregion
    }
}
