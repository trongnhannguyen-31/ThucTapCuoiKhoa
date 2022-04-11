using Phoenix.Server.Services.MainServices;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/order")]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]  
        [Route("GetAllAppOrders")]
        public async Task<BaseResponse<OrderAppDto>> GetAllAppOrders(OrderAppRequest request)
        {
            return await _orderService.GetAllAppOrders(request);
        }

        [HttpPost]
        [Route("AddOrder")]
        public Task<CrudResult> AddOrder([FromBody] OrderAppRequest request)

        {
            return _orderService.AddOrder(request);
        }

        [HttpPost]
        [Route("GetLatestOrder")]
        public async Task<BaseResponse<OrderAppDto>> GetLatestOrder([FromBody] OrderAppRequest request)
        {
            return await _orderService.GetLatestOrder(request);
        }
    }
}