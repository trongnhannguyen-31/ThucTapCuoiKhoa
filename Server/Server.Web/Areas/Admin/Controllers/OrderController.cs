using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Order;
using Phoenix.Server.Web.Areas.Admin.Models.OrderDetail;
using Phoenix.Server.Web.Areas.Admin.Models.ProductSKU;
using Phoenix.Shared.Order;
using Phoenix.Shared.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;

        public OrderController(IOrderService orderService, IOrderDetailService orderDetailService)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, OrderModel model)
        {
            var orders = await _orderService.GetAllOrders(new OrderRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                Id = model.Id,
                Address = model.Address,
            });

            var gridModel = new DataSourceResult
            {
                Data = orders.Data,
                Total = orders.DataCount
            };
            return Json(gridModel);
        }

        public ActionResult Cancel()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ListCancel(DataSourceRequest command, OrderModel model)
        {
            var orders = await _orderService.GetAllCancelOrders(new OrderRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                Id = model.Id,
                Address = model.Address,
            });

            var gridModel = new DataSourceResult
            {
                Data = orders.Data,
                Total = orders.DataCount
            };
            return Json(gridModel);
        }

        #region ChangStatus
        // Thay đổi trạng thái
        public ActionResult ChangeStatus(int id)
        {
            var model = new OrderModel();
            model.Id = id;

            var projectDto = _orderService.ChangeStatusById(model.Id, new OrderRequest()
            {
                Id = model.Id
            });

            SuccessNotification("Đổi trạng thái thành công");
            return RedirectToAction("Index");
        }
        #endregion
    }
}