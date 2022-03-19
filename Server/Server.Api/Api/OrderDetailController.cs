using Phoenix.Server.Services.MainServices;
using Phoenix.Shared.Common;
using Phoenix.Shared.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/orderDetail")]
    public class OrderDetailController : BaseApiController
    {
        private readonly IOrderDetailService _orderDetailService;
        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpPost]
        [Route("GetAllOrderDetails")]
        public async Task<BaseResponse<OrderDetailDto>> GetAllOrderDetails(OrderDetailRequest request)
        {
            return await _orderDetailService.GetAllOrderDetails(request);
        }
    }
}