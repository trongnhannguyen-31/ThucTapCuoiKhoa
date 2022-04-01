using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.OrderDetail
{
    public class OrderDetailModel
    {
        public int Id { get; set; }

        public int Order_Id { get; set; }

        public int ProductSKU_Id { get; set; }

        public double? Price { get; set; }

        public int? Quantity { get; set; }

        public double? Total { get; set; }
    }
}