using Phoenix.Shared.OrderDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phoenix.Mobile.Core.Models.OrderDetail
{
    public class OrderDetailHistoryModel : OrderDetailHistoryDto
    {
        public string Comment1 { get; set; }
        public int Rate1 { get; set; }
    }
}
