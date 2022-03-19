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
    public class CustomerController : Controller
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
    }
}