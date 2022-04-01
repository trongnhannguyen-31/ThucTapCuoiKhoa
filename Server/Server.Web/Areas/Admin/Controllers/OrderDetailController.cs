using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.OrderDetail;
using Phoenix.Shared.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class OrderDetailController : Controller
    {
        // GET: Admin/OrderDetail
        private readonly IOrderDetailService _orderDetailService;
        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        /*public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, OrderDetailModel model)
        {
            var orderDetails = await _orderDetailService.GetAllOrderDetails(new OrderDetailRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                Order_Id = model.Order_Id,
                Price = model.Price,
                Quantity = model.Quantity
            });

            var gridModel = new DataSourceResult
            {
                Data = orderDetails.Data,
                Total = orderDetails.DataCount
            };
            return Json(gridModel);
        }*/

        public ActionResult Index(int id)
        {
            var model = new OrderDetailModel();
            model.Id = id;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(DataSourceRequest command, OrderDetailModel model)
        {
            var orderDetails = await _orderDetailService.GetAllOrderDetailById(model.Id, new OrderDetailRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                Order_Id = model.Order_Id,
                Price = model.Price,
                Quantity = model.Quantity
            });

            var gridModel = new DataSourceResult
            {
                Data = orderDetails.Data,
                Total = orderDetails.DataCount
            };
            return Json(gridModel);
        }
    }
}