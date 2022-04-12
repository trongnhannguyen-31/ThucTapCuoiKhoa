using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.Order
{
    public class OrderModel
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public int Customer_Id { get; set; }

        public bool CancelRequest { get; set; }
    }
}