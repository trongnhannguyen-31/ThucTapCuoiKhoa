using Falcon.Web.Core.Helpers;
using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Customer;
using Phoenix.Shared.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Admin/Customer
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, CustomerModel model)
        {
            var customers = await _customerService.GetAllCustomers(new CustomerRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                FullName = model.FullName
            });

            var gridModel = new DataSourceResult
            {
                Data = customers.Data,
                Total = customers.DataCount
            };
            return Json(gridModel);
        }

        // Update Customer
        public ActionResult Update(int id)
        {
            var projectDto = _customerService.GetCustomersById(id);
            if (projectDto == null)
            {
                return RedirectToAction("Index");
            }

            var projectModel = projectDto.MapTo<CustomerModel>();
            return View(projectModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(CustomerModel model)
        {
            var project = _customerService.GetCustomersById(model.Id);
            if (project == null)
                return RedirectToAction("Index");
            if (!ModelState.IsValid)
                return View(model);
            var customers = await _customerService.UpdateCustomers(new CustomerRequest
            {
                Id = model.Id,
                FullName = model.FullName,
                Phone = model.Phone,
                Address = model.Address,
                Email = model.Email,
            });
            SuccessNotification("Chỉnh sửa thông tin chương trình thành công");
            return RedirectToAction("Update", new { id = model.Id });
        }

        // Delete Customer
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var project = _customerService.GetCustomersById(id);
            if (project == null)
                //No email account found with the specified id
                return RedirectToAction("Index");

            await _customerService.DeleteCustomersById(project.Id);
            SuccessNotification("Xóa đại lý thành công");
            return RedirectToAction("Index");
        }
    }
}