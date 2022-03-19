using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.Warehouse
{
    public class WarehouseModel
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int ProductSKU_Id { get; set; }
    }
}